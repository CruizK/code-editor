import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Flex, Grid } from "@chakra-ui/layout";
import { Textarea } from "@chakra-ui/textarea";

/**
 * Handles displaying form UI
 * Formdata is sent through the courses route, using document.getElementById to grab the form DOM object
 */
function CourseForm() {

    return(
        <Flex alignItems="end" flexDir="column">
            <form id="course_form">
                <Grid templateRows="5 1fr" gap={6} w="container.md" className="pog">
                    <FormControl id="course_title" isRequired>
                        <FormLabel display="flex" alignItems="center">Title
                            <Input placeholder="..." ml={15}/>
                        </FormLabel>
                    </FormControl>
                    <FormControl id="description" isRequired>
                        <FormLabel display="flex" alignItems="center">Description
                            <Textarea placeholder="..." ml={15}/>
                        </FormLabel>
                    </FormControl>
                </Grid>
            </form>
        </Flex>
    );
}

export default CourseForm;