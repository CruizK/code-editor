import { Center, Grid } from "@chakra-ui/layout";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import { loggedIn } from "@Modules/Auth/Auth";
import CourseList from "@Modules/Courses/components/CourseList/CourseList";
import instance from "@Utils/instance";

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
    data = response.data;

    return {
      props: {
          courses: data,
      }, // will be passed to the page component as props
    }
}  

function Teacher(props) {

    const { courses } = props;

    return(
        <Main>
            <Grid templateRows="5 1fr" gap={6} width="100%">
                <Center><SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink></Center>
                <SectionHeader title="MY CREATED CONTENT">
                    <SNoLinkButton href="/tutorials/new" variant="black">
                        New Tutorial +
                    </SNoLinkButton>
                    <SNoLinkButton href="/courses/new" variant="maroon">
                        New Course +
                    </SNoLinkButton>
                </SectionHeader>
                <CourseList courses={courses} />
            </Grid>
        </Main>
    );
  }

export default Teacher;