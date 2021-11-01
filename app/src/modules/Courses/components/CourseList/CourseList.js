import { Accordion, AccordionButton, AccordionIcon, AccordionItem, AccordionPanel } from "@chakra-ui/accordion";
import { DeleteIcon, EditIcon } from "@chakra-ui/icons";
import { Box, Heading, HStack } from "@chakra-ui/layout";
import { useStyleConfig } from "@chakra-ui/system";
import { loggedIn } from "@Modules/Auth/Auth";
import Router from 'next/router';
import { deleteCourse } from "@Modules/Courses/Courses";
import TutorialList from "@Modules/Tutorials/components/TutorialList/TutorialList";
import { storeThenRouteCourse } from "@Utils/storage";
import { useCookies } from "react-cookie";

function CourseItem(props) {
    const { id, title, description } = props;

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
                    <Box flex="1" textAlign="left" fontSize="md">
                        {title}
                    </Box>
                    <HStack spacing={3}>                        
                        <EditIcon color="ce_mainmaroon" onClick={() => storeThenRouteCourse(id, title, description)} />
                        <DeleteIcon onClick={() => handleDeletion(id, token)} />
                        <AccordionIcon />
                    </HStack>
                </AccordionButton>
            </Heading>
            <AccordionPanel pb={4}>
                <TutorialList courseId={id} getTutorials={isExpanded} />
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
            <Accordion>
                {courses.map((courseData, index) => {
                    let courseDefaults = {
                        id: courseData.id,
                        title: courseData.title,
                        description: courseData.description,
                    }
                    return <CourseItem key={index} {...courseDefaults} />;
                })}
            </Accordion>
        </Box>
    )
}

export default CourseList;