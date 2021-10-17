import { Accordion, AccordionButton, AccordionIcon, AccordionItem, AccordionPanel } from "@chakra-ui/accordion";
import { DeleteIcon, EditIcon } from "@chakra-ui/icons";
import { Box, Heading, HStack } from "@chakra-ui/layout";
import { useStyleConfig } from "@chakra-ui/system";
import TutorialList from "@Modules/Tutorials/components/TutorialList/TutorialList";
import { storeThenRouteCourse } from "@Utils/storage";

function CourseItem(props) {
    const { id, title, description } = props;

    return(
        <AccordionItem>
            <Heading as="h2">
                <AccordionButton>
                    <Box flex="1" textAlign="left" fontSize="md">
                        {title}
                    </Box>
                    <HStack spacing={3}>                        
                        <EditIcon color="ce_mainmaroon" onClick={() => storeThenRouteCourse(id, title, description)} />
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
                    return <CourseItem key={index} {...courseDefaults} tutorials={courseData.tutorials} />;
                })}
            </Accordion>
        </Box>
    )
}

export default CourseList;