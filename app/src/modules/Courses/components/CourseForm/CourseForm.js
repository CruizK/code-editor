import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Flex, Grid } from "@chakra-ui/layout";
import { Textarea } from "@chakra-ui/textarea";
import { courseRegEx, courseTitleTooltipLines } from "@Modules/Courses/Courses";

/**
 * Handles displaying form UI
 * Formdata is sent through the courses route, using document.getElementById to grab the form DOM object
 */
function CourseForm(props) {
    const dvs = (props.defaultValues) ? props.defaultValues : {};

    return(
        <Flex alignItems="end" flexDir="column">
            <form id="course_form">
                <Grid templateRows="5 1fr" gap={6} w="container.md" className="pog">
                    {dvs["id"] &&
                    <Input id="course_id" type="hidden" defaultValue={dvs["id"]} /> 
                    }
                    <FormControl id="course_title" isRequired>
                        <FormLabel display="flex" alignItems="center">Title
                            <Input placeholder="..." ml={15} defaultValue={dvs["title"]} pattern={courseRegEx()}/>
                        </FormLabel>
                    </FormControl>
                    <FormControl id="description" isRequired>
                        <FormLabel display="flex" alignItems="center">Description
                            <Textarea placeholder="..." ml={15} defaultValue={dvs["description"]}/>
                        </FormLabel>
                    </FormControl>
                </Grid>
            </form>
        </Flex>
    );
}

export default CourseForm;