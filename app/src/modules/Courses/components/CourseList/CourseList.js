import { Accordion, AccordionButton, AccordionIcon, AccordionItem, AccordionPanel } from "@chakra-ui/accordion";
import { DeleteIcon, EditIcon } from "@chakra-ui/icons";
import { Box, Heading, HStack } from "@chakra-ui/layout";
import { useStyleConfig } from "@chakra-ui/system";
import TutorialList from "@Modules/Tutorials/components/TutorialList/TutorialList";

function CourseItem(props) {
    return(
        <AccordionItem>
            <Heading as="h2">
                <AccordionButton>
                    <Box flex="1" textAlign="left" fontSize="md">
                        {props.title}
                    </Box>
                    <HStack spacing={3}>                        
                        <EditIcon color="ce_mainmaroon" />
                        <DeleteIcon />
                        <AccordionIcon />
                    </HStack>
                </AccordionButton>
            </Heading>
            <AccordionPanel pb={4}>
                <TutorialList tutorials={props.tutorials}/>
            </AccordionPanel>
        </AccordionItem>
    )
}

/**
 * Handles displaying an accordion list of courses.
 */
function CourseList(props) {
    const styles = useStyleConfig("AccordionBox", {});

    const courses = props.courses;

    return(
        <Box __css={styles}>
            <Accordion>
                {courses.map((courseData, index) => {
                    return <CourseItem key={index} title={courseData.title} tutorials={courseData.tutorials} />;
                })}
            </Accordion>
        </Box>
    )
}

export default CourseList;