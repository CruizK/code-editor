import { Image } from "@chakra-ui/react";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import CourseForm from "@Modules/Courses/components/CourseForm/CourseForm";
import { Center, Grid } from "@chakra-ui/layout";
import { Button } from "@chakra-ui/button";
import { createCourse } from "@Modules/Courses/Courses";
import { useCookies } from "react-cookie";
import { loggedIn } from "@Modules/Auth/Auth";

function NewCourse() {
  const [cookies, setCookie, removeCookie] = useCookies(["user"]);
  const isLoggedIn = loggedIn(cookies.user);
  const token = cookies.user;

  return(
      <Main>
        <Grid templateRows="5 1fr" gap={6} width="100%">
          <Center><SNoLink href="/"><Image src="/siucode_logo.png" alt="SIU Logo" maxHeight="100px" /></SNoLink></Center>
          <SectionHeader title="CREATE COURSE">
          <SNoLinkButton href="/dashboard/teacher" variant="white">
            Cancel
          </SNoLinkButton>
          <Button variant="black" onClick={() => createCourse(false, token)}>
            Save As Draft
          </Button>
          <Button variant="maroon" onClick={() => createCourse(true, token)}>
            Publish
          </Button>
          </SectionHeader>
          <CourseForm />
        </Grid>
      </Main>
  );
  }

export default NewCourse;