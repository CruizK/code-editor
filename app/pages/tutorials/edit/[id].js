import { Image } from "@chakra-ui/react";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import { Center, Grid } from "@chakra-ui/layout";
import { Button } from "@chakra-ui/button";
import { useCookies } from "react-cookie";
import { loggedIn, redirectPayload } from "@Modules/Auth/Auth";
import Router from 'next/router';
import { getRole } from "@Utils/jwt";
import dynamic from 'next/dynamic'; 
const TutorialForm = dynamic(
  () => import('@Modules/Tutorials/components/TutorialForm/TutorialForm').then(mod => mod.default),
  { ssr: false }
);
import instance from "@Utils/instance";
import { createTutorial, getUserTutorialDetailsFromId, updateTutorial } from "@Modules/Tutorials/Tutorials";
import ValidationBarrier from "@Components/ValidationBarrier/ValidationBarrier";
import { getLanguageFromId } from "@Utils/templates";
import { ShouldLanguageCompile } from "@Utils/static";

export async function getServerSideProps(context) {
  const { id } = context.query;

  var courses = [];

  const cookies = context.req.cookies;
  const isLoggedIn = loggedIn(cookies.user);

  if (!isLoggedIn) return redirectPayload;

  let token = cookies.user;

  const headers = {};

  if (isLoggedIn) {
    headers["Authorization"] = "Bearer " + token;
  }
  
  let courseResponse;

  try {
    courseResponse = await instance.get("/Courses/GetUserCreatedCourses", {
      headers: {...headers},
    });
    
    if (courseResponse.statusText == "OK")
    courses = courseResponse.data.map((courseData) => {
      // we only need the titles for each course
      return {
        id: courseData.id,
        title: courseData.title + ' (' + courseData.id + ')',
      };
    });
  } catch (error) {
    console.log(error);
  }

  let defaultValues = await getUserTutorialDetailsFromId(id, token) || {};

  return {
    props: {
      courses: courses,
      defaultValues: defaultValues,
    }, // will be passed to the page component as props
  }
}

function EditTutorial(props) {
  const [cookies, setCookie, removeCookie] = useCookies(["user"]);
  const isLoggedIn = loggedIn(cookies.user);
  const token = cookies.user;

  async function handleSubmit(isPublished, token) {
    let success = await updateTutorial(isPublished, token);
    if (success) {
      const userRole = (isLoggedIn) ? getRole(cookies.user) : "None";
      let redirect = '/dashboard/' + ((userRole == "Student") ? '' : (userRole.toLowerCase())); 
      Router.push(redirect);
    }
  }

  /**
   * Currently short-circuits, re-enable if need be
   * @returns false
   */
  function hasNoSolution() {
    return false;
    const languageNode = document.getElementById('language');
    const solutionNode = document.getElementById('solution');
    
    const language = getLanguageFromId(languageNode.value);
    const isEmpty = ShouldLanguageCompile(language) && solutionNode.value === '';
    return isEmpty;
  }

  return(
      <Main>
        <Grid templateRows="5 1fr" gap={6} width="100%">
          <Center><SNoLink href="/"><Image src="/siucode_logo.png" alt="SIU Logo" maxHeight="100px" /></SNoLink></Center>
          <SectionHeader title="CREATE TUTORIAL">
          <SNoLinkButton href="/dashboard/teacher" variant="white">
            Cancel
          </SNoLinkButton>
          <Button variant="black" onClick={() => handleSubmit(false, token)}>
            Save As Draft
          </Button>
          <ValidationBarrier 
            buttonText={
              <Button variant="maroon">
                Publish
              </Button>
            }
            title="Alert"
            text="You must have a solution to publish a tutorial."
            conditional={hasNoSolution}
            callback={() => handleSubmit(true, token)}
          />;
          </SectionHeader>
          <TutorialForm courses={props.courses} defaultValues={props.defaultValues} getDefaults={true} />
        </Grid>
      </Main>
  );
}

export default EditTutorial;