import { Accordion, AccordionButton, AccordionIcon, AccordionItem, AccordionPanel } from "@chakra-ui/accordion";
import { DeleteIcon, EditIcon } from "@chakra-ui/icons";
import { Box, Heading, HStack } from "@chakra-ui/layout";
import { useStyleConfig } from "@chakra-ui/system";
import { loggedIn } from "@Modules/Auth/Auth";
import Router from 'next/router';
import { deleteCourse } from "@Modules/Courses/Courses";
import TutorialList from "@Modules/Tutorials/components/TutorialList/TutorialList";
import { useCookies } from "react-cookie";
import Barrier from "@Components/Barrier/Barrier";

function CourseItem(props) {
    const { id, title, description, isPublished } = props.data;

    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const token = cookies.user;

    async function handleDeletion(id, token) {
        let success = await deleteCourse(id, token);
        if (success) {
            Router.reload();
        }
    }

    return(
        <AccordionItem>
            {({ isExpanded }) => (
            <>
            <Heading as="h2">
                <AccordionButton>
                    <Box flex="1" textAlign="left" fontSize="sm">
                        {title}
                    </Box>
                    <HStack spacing={3}>                        
                        <EditIcon color="ce_mainmaroon" onClick={() => {
                            let redirect = '/courses/edit/' + id; 
                            Router.push(redirect);
                        }} />
                        <Barrier 
                            buttonText={<DeleteIcon />}
                            title="Confirmation"
                            text="Are you sure you want to delete this course and its associated tutorials?"
                            callback={() => handleDeletion(id, token)}
                        />
                        <AccordionIcon />
                    </HStack>
                </AccordionButton>
            </Heading>
            <AccordionPanel pb={4}>
                <TutorialList courseId={id} getTutorials={isExpanded} editable={true} />
            </AccordionPanel>
            </>
            )}
        </AccordionItem>
    )
}

/**
 * Handles displaying an accordion list of courses.
 */
function CourseList(props) {
    const styles = useStyleConfig("AccordionBox", {});

    const { courses } = props;

    return(
        <Box __css={styles}>
            <Accordion allowMultiple>
                {courses.map((courseData, index) => {
                    let courseDefaults = {
                        id: courseData.id,
                        title: courseData.title,
                        description: courseData.description,
                        isPublished: courseData.isPublished,
                    }
                    return <CourseItem key={index} data={courseDefaults} />;
                })}
            </Accordion>
        </Box>
    )
}

export default CourseList;