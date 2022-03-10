import { Box, Button, Center, Flex, Heading, Image, Spacer } from "@chakra-ui/react";
import Main from "@Components/Main/Main";
import Router, { useRouter } from 'next/router';
import TutorialList from "@Modules/Tutorials/components/TutorialList/TutorialList";
import { checkIfInCourse, getCourseDetails, registerForCourse } from "@Modules/Courses/Courses";
import { loggedIn } from "@Modules/Auth/Auth";
import { useCookies } from "react-cookie";
import { getLastTutorial, getTutorialsFromCourse, getUserTutorialsDetailsFromCourse } from "@Modules/Tutorials/Tutorials";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";

export async function getServerSideProps(context) {
    const { id } = context.query;
    var courseDetails = {};
    var lastTutorial;

    const cookies = context.req.cookies;
    const isLoggedIn = loggedIn(cookies.user);
    let token = cookies.user;

    const course = await getCourseDetails(id, token);
    if (course) {
        courseDetails = course.courseDetails;
    }

    const isRegistered = await checkIfInCourse(id, token);

    const tutorials = course.courseTutorials;

    const userTutorialList = course.userTutorialList;
    tutorials.forEach(tutorial => {
        const userTutorial = userTutorialList.find((userTute) => userTute.tutorialId == tutorial.id);
        if (userTutorial) {
            tutorial['status'] = userTutorial.status;
        } else {
            tutorial['status'] = 1; // not started
        }
    });

    const lastTutorialResponse = await getLastTutorial(id, token); // last in progress tutorial

    if (lastTutorialResponse) {
        lastTutorial = lastTutorialResponse.data;
    }

    return {
        props: {
            ...courseDetails,
            tutorials: tutorials,
            lastTutorialId: (lastTutorial) ? lastTutorial.id : null,
            isRegistered: isRegistered,
        }, // will be passed to the page component as props
    }
}

function Course(props) {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const token = cookies.user;
    
    const { id, title, description, tutorials, isRegistered, lastTutorialId } = props;

    /**
     * 
     * @param {integer} to Tutorial id
     * @param {integer} from Course id
     */
    async function start(event, to, from) {
        let success = true;
        if (!isRegistered) {
            success = await registerForCourse(from, token);
        }
        if (success) {
            let redirect = '/tutorials/' + to; 
            Router.push(redirect);
        }
    }

    return(
        <Main width="100%" margin="0" maxWidth="100%">
            <Flex width="100%" height="8rem" backgroundColor="ce_mainmaroon" alignItems="center">
                <Flex height="50%" w="100%" color="ce_white" fontWeight="bold" fontFamily="button" fontSize="md" alignItems="end">
                    {title}
                </Flex>
            </Flex>
            <Box maxWidth="container.lg" margin="auto">
                <Heading size="sm" fontWeight="bold">Description</Heading>
                {description}
                <Center>
                    {isRegistered && 
                    <SNoLinkButton 
                        href={(lastTutorialId) ? "/tutorials/" + lastTutorialId : undefined}
                        variant="black"  w="xs" maxW="md" pt={15} pb={15} mb={15} mr={15}
                    >
                        Continue From Last Tutorial
                    </SNoLinkButton>
                    }
                    <Button variant="maroon" onClick={(event) => start(event, tutorials[0].id, id)} w="xs" maxW="md" pt={15} pb={15} mb={15}>
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