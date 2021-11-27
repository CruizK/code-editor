import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Flex, Grid } from "@chakra-ui/layout";
import { Select } from "@chakra-ui/react";
import { Textarea } from "@chakra-ui/textarea";
import { difficultylevels, programmingLanguages } from "@Utils/static";
import dynamic from 'next/dynamic'; 
const MarkdownEditor = dynamic(
    () => import('../MarkdownEditor/MarkdownEditor').then(mod => mod.default),
    { ssr: false }
);

/**
 * Handles displaying form UI
 * Formdata is sent through the tutorials route, using document.getElementById to grab the form DOM object
 */
function TutorialForm(props) {
    const dvs = (props.defaultValues) ? props.defaultValues : {};
    const courseOptions = props.courses || [];

    const difficultyOptions = difficultylevels;
    const languageOptions = programmingLanguages;

    return(
        <Flex alignItems="end" flexDir="column">
            <form id="tutorial_form">
                <Grid templateRows="5 1fr" gap={6} w="container.md" className="pog">
                    {dvs["id"] &&
                    <Input id="tutorial_id" type="hidden" defaultValue={dvs["id"]} /> 
                    }
                    <FormLabel display="flex" alignItems="center">Course
                        <Select id="course_id" ml={15} defaultValue={dvs["courseId"]}>
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
                            <Input placeholder="..." ml={15} defaultValue={dvs["title"]} />
                        </FormLabel>
                    </FormControl>
                    <FormControl id="description" isRequired>
                        <FormLabel display="flex" alignItems="center">Description
                            <Textarea placeholder="..." ml={15} defaultValue={dvs["description"]}/>
                        </FormLabel>
                    </FormControl>
                    <FormLabel display="flex" alignItems="center">Language
                        <Select id="language" ml={15} defaultValue={dvs["languageId"]}>
                            {languageOptions.map((option, index) => {
                                const {dbIndex, value} = option;
                                return <option id={index} value={dbIndex}>{value}</option>
                            })}
                        </Select>
                    </FormLabel>
                    <FormLabel display="flex" alignItems="center">Difficulty
                        <Select id="difficulty" ml={15} defaultValue={dvs["difficultyId"]}>
                            {difficultyOptions.map((option, index) => {
                                const {dbIndex, value} = option;
                                return <option id={index} value={dbIndex}>{value}</option>
                            })}
                        </Select>
                    </FormLabel>
                    <MarkdownEditor prompt={dvs["prompt"]} />
                </Grid>
            </form>
        </Flex>
    );
}

export default TutorialForm;