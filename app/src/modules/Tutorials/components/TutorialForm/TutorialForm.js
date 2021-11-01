import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Flex, Grid } from "@chakra-ui/layout";
import { Select } from "@chakra-ui/react";
import { Textarea } from "@chakra-ui/textarea";
import { difficultylevels, programmingLanguages } from "@Utils/static";

/**
 * Handles displaying form UI
 * Formdata is sent through the tutorials route, using document.getElementById to grab the form DOM object
 */
function TutorialForm(props) {
    let dCID, dT, dD, dID, dDiff, dLan;
    if (props.defaultValues) {
        let v = props.defaultValues;
        dCID = v["courseId"],
        dID = v["id"];
        dT = v["title"];
        dD = v["description"]
        dDiff = v["difficultyId"],
        dLan = v["languageId"];
    }

    const courseOptions = props.courses || [];

    const difficultyOptions = difficultylevels;
    const languageOptions = programmingLanguages;

    return(
        <Flex alignItems="end" flexDir="column">
            <form id="tutorial_form">
                <Grid templateRows="5 1fr" gap={6} w="container.md" className="pog">
                    {dID &&
                    <Input id="tutorial_id" type="hidden" defaultValue={dID} /> 
                    }
                    <FormLabel display="flex" alignItems="center">Course
                        <Select id="course_id" ml={15} defaultValue={dCID}>
                            {courseOptions.map((option, index) => {
                                const {title, id} = option;
                                return(
                                    <option id={index} value={id}>{title}</option>
                                );
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
                        <Select id="language" ml={15} defaultValue={dLan}>
                            {languageOptions.map((option, index) => {
                                const {dbIndex, value} = option;
                                return <option id={index} value={dbIndex}>{value}</option>
                            })}
                        </Select>
                    </FormLabel>
                    <FormLabel display="flex" alignItems="center">Difficulty
                        <Select id="difficulty" ml={15} defaultValue={dDiff}>
                            {difficultyOptions.map((option, index) => {
                                const {dbIndex, value} = option;
                                return <option id={index} value={dbIndex}>{value}</option>
                            })}
                        </Select>
                    </FormLabel>
                </Grid>
            </form>
        </Flex>
    );
}

export default TutorialForm;