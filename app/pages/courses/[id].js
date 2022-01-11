import { Box, Button, Center, Flex, Heading, Image, Spacer } from "@chakra-ui/react";
import Main from "@Components/Main/Main";
import { useRouter } from 'next/router';
import TutorialList from "@Modules/Tutorials/components/TutorialList/TutorialList";
import { getCourseDetails, registerForCourse } from "@Modules/Courses/Courses";
import { loggedIn } from "@Modules/Auth/Auth";
import { useCookies } from "react-cookie";

export async function getServerSideProps(context) {
    const { id } = context.query;
    var courseDetails = {};

    const cookies = context.req.cookies;
    const isLoggedIn = loggedIn(cookies.user);
    let token = cookies.user;

    const course = await getCourseDetails(id, token);
    if (course) {
        courseDetails = course;
    }
  
    return {
        props: {
            ...courseDetails
        }, // will be passed to the page component as props
    }
}

function Course(props) {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const token = cookies.user;
    
    const { id, title, description, tutorials } = props;
    console.log(props);

    async function handleSubmit(event) {
        /**
         * Add handler code here.
         */
        let success = await registerForCourse(id, token);
        if (success) {
            // do something
        }
    }

    return(
        <Main width="100%" margin="0" maxWidth="100%">
            <Flex width="100%" height="8rem" backgroundColor="ce_mainmaroon" alignItems="center">
                <Flex height="50%" w="100%" color="ce_white" fontWeight="bold" fontFamily="button" fontSize="md" alignItems="end">
                    {title}
                </Flex>
                <Spacer />
                <Image src="/defaults/card_icon.png" alt="SIU Logo" height="60%" mr={15} />
            </Flex>
            <Box maxWidth="container.lg" margin="auto">
                <Heading size="sm" fontWeight="bold">Description</Heading>
                {description}
                <Center>
                    <Button variant="maroon" onClick={() => handleSubmit(true, token)} w="xs" maxW="md" pt={15} pb={15} mb={15}>
                        Start from Beginning
                    </Button>
                </Center>
                <Box borderColor="ce_grey" borderWidth="2px" borderRadius="md" pl={15} pr={15}>
                    <Heading size="sm" fontWeight="bold">Tutorial</Heading>
                    <TutorialList courseId={id} tutorials={tutorials} />
                </Box>
            </Box>
        </Main>
    );
} 

export default Course;