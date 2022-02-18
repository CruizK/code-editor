import { Box, Button, Center, Flex, Heading, Image, Spacer } from "@chakra-ui/react";
import Main from "@Components/Main/Main";
import Router, { useRouter } from 'next/router';
import TutorialList from "@Modules/Tutorials/components/TutorialList/TutorialList";
import { checkIfInCourse, getCourseDetails, registerForCourse } from "@Modules/Courses/Courses";
import { loggedIn } from "@Modules/Auth/Auth";
import { useCookies } from "react-cookie";
import { getTutorialsFromCourse, getUserTutorialsDetailsFromCourse } from "@Modules/Tutorials/Tutorials";

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

    const isRegistered = await checkIfInCourse(id, token);

    const tutorials = await getTutorialsFromCourse(id, token);
    const tutorialDetails = await getUserTutorialsDetailsFromCourse(id, token);
    tutorials.forEach(function(tute) {
        const thisCourseIndex = tutorialDetails.findIndex(tuteDetails => tute.id == tuteDetails.id); // it's possible a tutorial added after someone registers for a course doesnt have a tutorialDetails
        if (isRegistered && thisCourseIndex == -1)
        console.log(`User is registered for course ${id}, but not for every tutorial under it`);

        let inProgress = (tutorialDetails && tutorialDetails[thisCourseIndex]) ? tutorialDetails[thisCourseIndex].inProgress : false;
        tute['inProgress'] = inProgress;

        let isCompleted = (tutorialDetails && tutorialDetails[thisCourseIndex]) ? tutorialDetails[thisCourseIndex].isCompleted : false;
        tute['isCompleted'] = isCompleted;

        if (inProgress && isCompleted)
        console.error(new Error(`Tutorial ${tute.id} is both complete and in progress.`));
        return tute
    });
  
    return {
        props: {
            ...courseDetails,
            tutorials: tutorials,
            isRegistered: isRegistered,
        }, // will be passed to the page component as props
    }
}

function Course(props) {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const token = cookies.user;
    
    const { id, title, description, tutorials, isRegistered } = props;

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
                <Spacer />
                <Image src="/defaults/card_icon.png" alt="SIU Logo" height="60%" mr={15} />
            </Flex>
            <Box maxWidth="container.lg" margin="auto">
                <Heading size="sm" fontWeight="bold">Description</Heading>
                {description}
                <Center>
                    {isRegistered && 
                    <Button variant="black" onClick={() => {/** TODO */}} w="xs" maxW="md" pt={15} pb={15} mb={15} mr={15} isDisabled="true">
                        Continue From Last Tutorial
                    </Button>
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