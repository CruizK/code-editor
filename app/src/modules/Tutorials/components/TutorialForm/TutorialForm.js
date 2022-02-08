import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Flex, Grid, Box } from "@chakra-ui/layout";
import { Button, Select, Spacer } from "@chakra-ui/react";
import { Textarea } from "@chakra-ui/textarea";
import { difficultylevels, programmingLanguages } from "@Utils/static";
import dynamic from 'next/dynamic';
import { useState } from "react";
const MarkdownEditor = dynamic(
    () => import('../MarkdownEditor/MarkdownEditor').then(mod => mod.default),
    { ssr: false }
);
import Editor from "@monaco-editor/react";
import FileUpload from "@Components/FileUpload/FileUpload";

/**
 * Handles displaying form UI
 * Formdata is sent through the tutorials route, using document.getElementById to grab the form DOM object
 */
function TutorialForm(props) {
    const dvs = (props.defaultValues) ? props.defaultValues : {};
    const courseOptions = props.courses || [];

    const difficultyOptions = difficultylevels;
    const languageOptions = programmingLanguages;

    const spacing = 5;
    const selectWidth = '150px';

    const [template, setTemplate] = useState(dvs["template"] || ``);
    const [prompt, setPrompt] = useState(dvs["prompt"] || '');

    return (
        <form id="tutorial_form" style={{ width: '100%' }}>
            <Flex w="90%" align={"flex-end"} ml="auto" flexDir={"column"}>
                {dvs["id"] &&
                    <Input id="tutorial_id" type="hidden" defaultValue={dvs["id"]} />
                }
                <Flex w="100%">
                    <Box w="20%" fontWeight={"bold"} fontSize={"md"}>Course</Box>
                    <Select w="30%" maxW={selectWidth} id="course_id" defaultValue={dvs["courseId"]}>
                        {courseOptions.map((option, index) => {
                            const { title, id } = option;
                            return (
                                <option id={index} value={id}>{title}</option>
                            );
                        })}
                    </Select>
                </Flex>
                <Flex w="100%" mt={spacing}>
                    <Box w="20%" fontWeight={"bold"} fontSize={"md"}>Title</Box>
                    <FormControl w="80%" id="tutorial_title">
                        <Input placeholder="..." defaultValue={dvs["title"]} />
                    </FormControl>

                </Flex>
                <Flex w="100%" mt={spacing}>
                    <Box w="20%" fontWeight={"bold"} fontSize={"md"}>Description</Box>
                    <FormControl w="80%" id="description">
                        <Textarea placeholder="..." defaultValue={dvs["description"]} />
                    </FormControl>

                </Flex>
                <Flex w="100%" mt={spacing}>
                    <Box w="20%" fontWeight={"bold"} fontSize={"md"}>Language</Box>
                    <Select w="30%" maxW={selectWidth} id="language" defaultValue={dvs["languageId"]}>
                        {languageOptions.map((option, index) => {
                            const { dbIndex, value } = option;
                            return <option id={index} value={dbIndex}>{value}</option>
                        })}
                    </Select>

                </Flex>
                <Flex w="100%" mt={spacing}>
                    <Box w="20%" fontWeight={"bold"} fontSize={"md"}>Difficulty</Box>
                    <Select w="30%" maxW={selectWidth} id="difficulty" defaultValue={dvs["difficultyId"]}>
                        {difficultyOptions.map((option, index) => {
                            const { dbIndex, value } = option;
                            return <option id={index} value={dbIndex}>{value}</option>
                        })}
                    </Select>
                </Flex>
                <Flex w="100%" mt={spacing} direction="column">
                    <Box w="100%" fontWeight={"bold"} fontSize={"md"}>Tutorial Base Code</Box>
                    <p>Choose whether you want to upload an existing code file or if you want to edit boilerplate code provided for us.</p>
                    <Spacer />
                    <Box id="actions">
                        <FileUpload id="fileSelect" callback={setTemplate} />
                        <Button w="20%" maxW="150px" mr={2} variant="maroon">Edit A Template</Button>
                    </Box>
                </Flex>
            </Flex>
            <Grid id="panes" w="100%" maxW="container.lg" height="fit-content" templateColumns="repeat(2, 50%)" mx={2}>
                <Box fontSize={"md"} py={2}>Tutorial Instructions</Box>
                <Box fontSize={"md"} py={2}>Boilerplate code</Box>
                <Box pb="10%">
                    <MarkdownEditor prompt={dvs["prompt"]} callback={setPrompt} />
                </Box>
                <Box>
                    <Editor
                        height="100%"
                        width="100%"
                        theme="vs-dark"
                        defaultLanguage="html"
                        options={{
                            padding: {
                            top: "10px"
                            },
                            scrollBeyondLastLine: false,
                            wordWrap: "on",
                            minimap: {
                            enabled: false
                            },
                            scrollbar: {
                            vertical: "auto"
                            }
                        }}
                        value={template}
                        onChange={(value, event) => { setTemplate(value); }}
                    />
                    <Input id="template" type="hidden" value={template} />
                </Box>
            </Grid>
        </form>
    );
}

export default TutorialForm;