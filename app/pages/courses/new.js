import { Image } from "@chakra-ui/react";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import dynamic from 'next/dynamic'; 
const CourseForm = dynamic(
    () => import('@Modules/Courses/components/CourseForm/CourseForm').then(mod => mod.default),
    { ssr: false }
);
import { Center, Grid } from "@chakra-ui/layout";
import { Button } from "@chakra-ui/button";
import { createCourse } from "@Modules/Courses/Courses";
import { useCookies } from "react-cookie";
import { loggedIn } from "@Modules/Auth/Auth";
import Router from 'next/router';
import { getRole } from "@Utils/jwt";

function NewCourse() {
  const [cookies, setCookie, removeCookie] = useCookies(["user"]);
  const isLoggedIn = loggedIn(cookies.user);
  const token = cookies.user;

  async function handleSubmit(isPublished, token) {
    let success = await createCourse(isPublished, token);
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
          <SectionHeader title="CREATE COURSE">
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
          <CourseForm />
        </Grid>
      </Main>
  );
  }

export default NewCourse;