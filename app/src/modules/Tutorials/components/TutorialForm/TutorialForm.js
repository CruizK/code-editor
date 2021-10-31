import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Flex, Grid } from "@chakra-ui/layout";
import { Select } from "@chakra-ui/react";
import { Textarea } from "@chakra-ui/textarea";

/**
 * Handles displaying form UI
 * Formdata is sent through the tutorials route, using document.getElementById to grab the form DOM object
 */
function TutorialForm(props) {
    let dT, dD, dID;
    if (props.defaultValues) {
        let v = props.defaultValues;
        dID = v["id"];
        dT = v["title"];
        dD = v["description"];
    }

    const courseOptions = [
        'Option 1', 
        'Option 2', 
        'Option 3',
    ]; //TODO: Replace with api call

    const languageOptions = [
        'Python', 
        'Java', 
        'Lisp',
    ]; //TODO: Replace with api call

    const difficultyOptions = [
        'Easy',
        'Medium',
        'Hard',
    ]; //TODO: Replace with api call

    return(
        <Flex alignItems="end" flexDir="column">
            <form id="tutorial_form">
                <Grid templateRows="5 1fr" gap={6} w="container.md" className="pog">
                    {dID &&
                    <Input id="tutorial_id" type="hidden" defaultValue={dID} /> 
                    }
                    <FormLabel display="flex" alignItems="center">Course
                        <Select ml={15}>
                            {courseOptions.map((courseTitle, index) => {
                                return <option id={index} value={courseTitle}>{courseTitle}</option>
                            })}
                        </Select>
                    </FormLabel>
                    <FormControl id="tutorial_title" isRequired>
                        <FormLabel display="flex" alignItems="center">Title
                            <Input placeholder="..." ml={15} defaultValue={dT} />
                        </FormLabel>
                    </FormControl>
                    <FormControl id="description" isRequired>
                        <FormLabel display="flex" alignItems="center">Description
                            <Textarea placeholder="..." ml={15} defaultValue={dD}/>
                        </FormLabel>
                    </FormControl>
                    <FormLabel display="flex" alignItems="center">Language
                        <Select ml={15}>
                            {languageOptions.map((languageTitle, index) => {
                                return <option id={index} value={languageTitle}>{languageTitle}</option>
                            })}
                        </Select>
                    </FormLabel>
                    <FormLabel display="flex" alignItems="center">Difficulty
                        <Select ml={15}>
                            {difficultyOptions.map((difficultyTitle, index) => {
                                return <option id={index} value={difficultyTitle}>{difficultyTitle}</option>
                            })}
                        </Select>
                    </FormLabel>
                </Grid>
            </form>
        </Flex>
    );
}

export default TutorialForm;