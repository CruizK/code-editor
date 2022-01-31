import { Flex, Container, Button } from "@chakra-ui/react";
import { loggedIn } from "@Modules/Auth/Auth";
import instance from "@Utils/instance";
import Editor from "@monaco-editor/react";
import { useEffect, useRef, useState } from "react";
import Router from "next/router";
import TutorialSideBar from "@Modules/Tutorials/components/TutorialSideBar/TutorialSideBar";
import { compileAndRunCode, getTutorialsFromCourse, getUserTutorialDetailsFromId, getUserTutorialsDetailsFromCourse, updateUserTutorial } from "@Modules/Tutorials/Tutorials";
import { useCookies } from "react-cookie";
import { checkIfInCourse } from "@Modules/Courses/Courses";

export async function getServerSideProps(context) {
  const { id } = context.query;
  let nextTutorialId = false; // assume there isn't a next tutorial

  const cookies = context.req.cookies;
  const isLoggedIn = loggedIn(cookies.user);
  let token = cookies.user;

  var values = await getUserTutorialDetailsFromId(id, token) || {};

  const isRegistered = await checkIfInCourse(values.courseId, token);

  if (isRegistered) {
    const tutorialDetails = await getUserTutorialsDetailsFromCourse(values.courseId, token);
    const thisCourseIndex = tutorialDetails.findIndex(tute => tute.id == values.id);
    const detailsForThisTutorial = tutorialDetails[thisCourseIndex];

    // We want to grab the next tutorial id while we're already grabbing UserTutorials
    const detailsForNextTutorial = tutorialDetails[thisCourseIndex + 1];
    if (detailsForNextTutorial)
    nextTutorialId = detailsForNextTutorial.id;

    let inProgress = (detailsForThisTutorial) ? detailsForThisTutorial.inProgress : false;
    values['inProgress'] = inProgress;

    let isCompleted = (detailsForThisTutorial) ? detailsForThisTutorial.isCompleted : false;
    values['isCompleted'] = isCompleted;
  }

  return {
    props: {
      values: values,
      nextTutorialId: nextTutorialId,
    }, // will be passed to the page component as props
  }
}

function Tutorial(props) {
  const { id, courseId, prompt } = props.values;

  const [cookies, setCookie, removeCookie] = useCookies(["user"]);
  const isLoggedIn = loggedIn(cookies.user);
  const token = cookies.user;

  const [showSidebar, setShow] = useState(true);

  const [editorText, setText] = useState(`<button onClick="document.getElementById('demo').innerHTML = \n\t'Change me!'"\n>\n\tClick Me!\n</button>\n<div id="demo"></div>\n`);
  const iframeRef = useRef();

  // For explanation of iframe messaging: https://joyofcode.xyz/avoid-flashing-iframe
  useEffect(() => {
    if (iframeRef && iframeRef.current) {
      const html = { type: 'html', value: editorText };
      iframeRef.current.contentWindow.postMessage(html, '*')
    }
  }, [editorText])

  /**
   * Saves progress, using tutorial id from query context. 
   */
  async function saveInProgress(event) {
    let success = await updateUserTutorial(id, token, true, false);
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

  /**
   * Sends code to compile to the server, setting inProgress and isComplete as necessary. 
   */
  async function runCode(event) {
    let success = await compileAndRunCode(id, token, 'CSharp', editorText);
    if (success) {
      let redirect = `/courses/${courseId}`;
      Router.push(redirect);
    }
  }

  return(
    <Container maxW="100%" p="0">
      <Flex direction={"column"} height="calc(100vh - 50px)">
        <Flex width="100%" flex="1">
          <TutorialSideBar prompt={prompt} show={showSidebar} setShow={setShow} />
          <Flex flex="2" maxW="50%" direction={"column"}>
            <Flex flex="1">
            <Editor
              height="100%"
              width="100%"
              theme="vs-dark"
              defaultLanguage="html"
              options={{
                padding: {
                  top: "10px"
                },
                scrollBeyondLastLine: false,
                wordWrap: "on",
                minimap: {
                  enabled: false
                },
                scrollbar: {
                  vertical: "auto"
                }
              }}
              defaultValue={editorText}
              onChange={(value, event) => { setText(value); setIframeVisible(false) }}
            />
            </Flex>
            <Flex h="50px" bg="ce_blue" justify={"end"}>
              <Button w="10%" h="100%" variant="blue" onClick={runCode}>RUN</Button>
            </Flex>
          </Flex>
          <Flex flex="1" width="100%">
            <iframe 
            ref={iframeRef}
            srcDoc={
              `
              <html style="background-color: #FFF;">
              <script type="module">
                window.addEventListener('message', (event) => {
                  const { type, value } = event.data;

                  if (type === 'html') {
                    document.body.innerHTML = value;
                  }
                })
              </script>
              <body>
               
              </body>
              </html>
              `
            } />
          </Flex>
        </Flex>
        <Flex h="50px" bg="ce_darkgrey" justify={"end"} align="center">
            <Button w="10%" maxW="150px" mr={2} variant="yellowOutline">Exit</Button>
            {(props.isCompleted) 
            ? <Button w="10%" maxW="150px" mr={2} variant="yellow" onClick={goToNext}>CONTINUE {'>'}</Button>
            : <Button w="10%" maxW="150px" mr={2} variant="yellow" onClick={saveInProgress}>SAVE PROGRESS</Button>
            }
        </Flex>
      </Flex>
    </Container>
  );
} 

export default Tutorial;