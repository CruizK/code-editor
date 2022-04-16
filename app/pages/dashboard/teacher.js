import { Center, Grid } from "@chakra-ui/layout";
import { Tooltip } from '@chakra-ui/react'
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import { loggedIn, redirectPayload } from "@Modules/Auth/Auth";
import CourseList from "@Modules/Courses/components/CourseList/CourseList";
import instance from "@Utils/instance";

export async function getServerSideProps(context) {
    var data = [];

    const cookies = context.req.cookies;
    const isLoggedIn = loggedIn(cookies.user);

    if (!isLoggedIn) return redirectPayload;

    const headers = {};
    let response;
    
    if (isLoggedIn) {
        let token = cookies.user;
        headers["Authorization"] = "Bearer " + token;
    }
    
    
    
    try {
        response = await instance.get("/Courses/GetUserCreatedCourses", {
            headers: {...headers},
        });
    } catch(e) {
        console.error(e)
    }
    
    data = response ? response.data : []

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
                    {courses.length > 0 ? <SNoLinkButton href="/tutorials/new" variant="black">New Tutorial +</SNoLinkButton> 
                    : <Tooltip label="Create a Course first!" shouldWrapChildren borderRadius="md">
                        <SNoLinkButton href="/tutorials/new" variant="black" isDisabled="true">New Tutorial +</SNoLinkButton>
                    </Tooltip> }
                        
                    <SNoLinkButton href="/courses/new" variant="maroon">
                        New Course +
                    </SNoLinkButton>
                </SectionHeader>
                { courses.length > 0 ? <CourseList courses={courses} /> : null }
            </Grid>
        </Main>
    );
  }

export default Teacher;
