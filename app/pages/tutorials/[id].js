import { Flex, Container, Button, Spinner, useToast } from "@chakra-ui/react";
import { loggedIn } from "@Modules/Auth/Auth";
import instance from "@Utils/instance";
import Editor from "@monaco-editor/react";
import { useEffect, useRef, useState } from "react";
import Router from "next/router";
import TutorialSideBar from "@Modules/Tutorials/components/TutorialSideBar/TutorialSideBar";
import { compileAndRunCode, getTutorialsFromCourse, getUserTutorialDetailsFromId, getUserTutorialsDetailsFromCourse, updateUserTutorial } from "@Modules/Tutorials/Tutorials";
import { useCookies } from "react-cookie";
import { checkIfInCourse, getCourseDetails } from "@Modules/Courses/Courses";
import TutorialCodeOutput from "@Modules/Tutorials/TutorialCodeOutput/TutorialCodeOutput";
import { dbLanguageToMonacoLanguage, programmingLanguages, ShouldLanguageCompile, tutorialStatus } from "@Utils/static";
import { getRole } from "@Utils/jwt";

export async function getServerSideProps(context) {
  const { id } = context.query;
  let nextTutorialId = false; // assume there isn't a next tutorial

  const cookies = context.req.cookies;
  const isLoggedIn = loggedIn(cookies.user);
  let token = cookies.user;

  var values = await getUserTutorialDetailsFromId(id, token) || {};
  var courseTutorials = null;

  const isRegistered = await checkIfInCourse(values.courseId, token);
  const courseDetails = await getCourseDetails(values.courseId, token);
  courseTutorials = courseDetails.courseTutorials;

  if (isRegistered) {
    const tutorialDetails = courseDetails.userTutorialList;

    const thisCourseIndex = tutorialDetails.findIndex(tute => tute.tutorialId == values.id); // it's possible a tutorial added after someone registers for a course doesnt have a tutorialDetails
    if (isRegistered && thisCourseIndex == -1)
      console.log(`User is registered for course ${values.courseId}, but not for tutorial ${id}: '${values.title}'.`);

    const detailsForThisTutorial = tutorialDetails[thisCourseIndex]; // undefined if thisCourseIndex isnt in tutorialDetails

    // We want to grab the next tutorial id while we're already grabbing UserTutorials
    const detailsForNextTutorial = tutorialDetails[thisCourseIndex + 1];
    if (detailsForNextTutorial)
      nextTutorialId = detailsForNextTutorial.tutorialId;

    let status = (detailsForThisTutorial) ? detailsForThisTutorial.status : tutorialStatus.NotStarted;
    values['status'] = status;

    let userCode = (detailsForThisTutorial) ? detailsForThisTutorial.userCode : null;
    values['userCode'] = userCode;
  }

  console.log(values);
  const language = programmingLanguages.filter(x => x.dbIndex == values.languageId)[0].value

  return {
    props: {
      values: values,
      courseTutorials: courseTutorials,
      nextTutorialId: nextTutorialId,
      language
    }, // will be passed to the page component as props
  }
}

function Tutorial(props) {
  const { id, courseId, status, userCode, prompt, template, solution } = props.values;
  const { language, courseTutorials } = props;
  const [cookies, setCookie, removeCookie] = useCookies(["user"]);
  const isLoggedIn = loggedIn(cookies.user);
  const userRole = (isLoggedIn) ? getRole(cookies.user) : "None";
  const token = cookies.user;

  const [showSidebar, setShow] = useState(true);

  const [thisStatus, setThisStatus] = useState(status); // we don't want to overwrite tutorialStatus from static.js
  const [compiling, setCompilationStatus] = useState(false);
  const [compiledText, setCompiledText] = useState('');

  const [editorText, setText] = useState(userCode || template || ``);

  const toast = useToast();

  /**
   * Saves progress, using tutorial id from query context. 
   */
  async function saveInProgress(event) {
    let success = await updateUserTutorial(id, token, tutorialStatus.InProgress, editorText);
    if (success) {
      let redirect = `/courses/${courseId}`;
      Router.push(redirect);
    }
  }

  async function goToNext(event) {
    let redirect = `/courses/${courseId}`; // if no next tutorial exists, just go back to course page

    if (props.nextTutorialId) {
      redirect = `/tutorials/${props.nextTutorialId}`;
    }
    Router.push(redirect);
  }

  async function submitCode(event) {    
    setCompilationStatus(true);
    const res = await compileAndRunCode(id, token, language, editorText);
    setCompilationStatus(false);

    // did the code run?
    // TODO: see if the checks passed. if they did, set status to completed
    if (res) {
      setCompiledText(res.data);
      // not the most robust check
      // maybe we could check difference percentage?
      if (res.data == solution) {  
        let updateResult = await updateUserTutorial(id, token, tutorialStatus.Completed, editorText);
        if (updateResult) {
          setThisStatus(tutorialStatus.Completed);
        }
      } else{
        toast({
          title: 'Incorrect output!',
          status: 'error',
          duration: 3000,
          isClosable: true,
          position: 'top'
        });
      }
    }
  }

  /**
   * Sends code to compile to the server, setting inProgress and isComplete as necessary. 
   */
  async function runCode(event) {
    console.log(editorText);
    setCompilationStatus(true);
    const res = await compileAndRunCode(id, token, language, editorText);
    setCompilationStatus(false);
    if (res) {
      setCompiledText(res.data)
    }
  }

  return (
    <Container maxW="100%" p="0">
      <Flex direction={"column"} height="calc(100vh - 50px)">
        <Flex width="100%" flex="1">
          <TutorialSideBar courseId={courseId} prompt={prompt} tutorials={courseTutorials} show={showSidebar} setShow={setShow} />
          <Flex flex="2" maxW="50%" direction={"column"}>
            <Flex flex="1">
              <Editor
                height="100%"
                width="100%"
                theme="vs-dark"
                language={dbLanguageToMonacoLanguage[language]}
                options={{
                  padding: {
                    top: "10px"
                  },
                  scrollBeyondLastLine: false,
                  overviewRulerLanes: 0,
                  overviewRulerBorder: false,
                  wordWrap: "on",
                  minimap: {
                    enabled: false
                  },
                  scrollbar: {
                    vertical: "hidden"
                  }
                }}
                defaultValue={editorText}
                onChange={(value) => setText(value)}
              />
            </Flex>
            {ShouldLanguageCompile(language) ?
              <Flex h="50px" bg="ce_blue" justify={"end"}>
                <Button disabled={compiling} w="10%" h="100%" variant="blue" onClick={runCode}>{(!compiling) ? "Run" : <Spinner />}</Button>
              </Flex>
              : null}
          </Flex>
          <TutorialCodeOutput language={language} editorText={editorText} compiledText={compiledText}/>
        </Flex>
        <Flex h="50px" bg="ce_darkgrey" justify={"end"} align="center">
          <Button w="10%" maxW="150px" mr={2} variant="yellowOutline">Exit</Button>
          <Button w="10%" maxW="150px" mr={2} variant="yellow"
            onClick={(userRole == 'Student') ? saveInProgress : undefined}
          >SAVE PROGRESS</Button>
          {[tutorialStatus.Completed].includes(thisStatus) &&
            <Button w="10%" maxW="150px" mr={2} variant="yellow" onClick={goToNext}>CONTINUE {'>'}</Button>
          }
          {[tutorialStatus.InProgress, tutorialStatus.NotStarted, tutorialStatus.Restarted].includes(thisStatus) &&
            <Button w="10%" maxW="150px" mr={2} variant="black"
              onClick={(userRole == 'Student') ? submitCode : undefined}
              _hover={{
                color: "ce_white",
                backgroundColor: "ce_yellow",
                borderColor: "ce_yellow",
                boxShadow: "none"
              }}
            >SUBMIT</Button>
          }
        </Flex>
      </Flex>
    </Container>
  );
}

export default Tutorial;