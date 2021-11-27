import { Image } from "@chakra-ui/react";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import { Center, Grid } from "@chakra-ui/layout";
import { Button } from "@chakra-ui/button";
import { useCookies } from "react-cookie";
import { loggedIn } from "@Modules/Auth/Auth";
import Router from 'next/router';
import { getRole } from "@Utils/jwt";
import dynamic from 'next/dynamic'; 
const TutorialForm = dynamic(
  () => import('@Modules/Tutorials/components/TutorialForm/TutorialForm').then(mod => mod.default),
  { ssr: false }
);
import instance from "@Utils/instance";
import { createTutorial } from "@Modules/Tutorials/Tutorials";

export async function getServerSideProps(context) {
  var data = [];

  const cookies = context.req.cookies;
  const isLoggedIn = loggedIn(cookies.user);
  const headers = {};

  if (isLoggedIn) {
    let token = cookies.user;
    headers["Authorization"] = "Bearer " + token;
  }
  
  let response = await instance.get("/Courses/GetUserCreatedCourses", {
    headers: {...headers},
  });
  
  if (response.statusText == "OK")
  data = response.data.map((courseData) => {
    // we only need the titles for each course
    return {
      id: courseData.id,
      title: courseData.title + ' (' + courseData.id + ')',
    };
  });

  return {
    props: {
      courses: data,
    }, // will be passed to the page component as props
  }
}

function NewTutorial(props) {
  const [cookies, setCookie, removeCookie] = useCookies(["user"]);
  const isLoggedIn = loggedIn(cookies.user);
  const token = cookies.user;

  async function handleSubmit(isPublished, token) {
    let success = await createTutorial(isPublished, token);
    if (success) {
      const userRole = (isLoggedIn) ? getRole(cookies.user) : "None";
      let redirect = '/dashboard/' + ((userRole == "Student") ? '' : (userRole.toLowerCase())); 
      Router.push(redirect);
    }
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
          <Button variant="maroon" onClick={() => handleSubmit(true, token)}>
            Publish
          </Button>
          </SectionHeader>
          <TutorialForm courses={props.courses} />
        </Grid>
      </Main>
  );
}

export default NewTutorial;