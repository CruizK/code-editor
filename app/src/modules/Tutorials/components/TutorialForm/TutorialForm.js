import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Flex, Grid, Box } from "@chakra-ui/layout";
import { Select, Spacer, Text } from "@chakra-ui/react";
import { Textarea } from "@chakra-ui/textarea";
import { difficultylevels } from "@Utils/static";
import dynamic from 'next/dynamic';
import { useEffect, useState } from "react";
const MarkdownEditor = dynamic(
    () => import('../MarkdownEditor/MarkdownEditor').then(mod => mod.default),
    { ssr: false }
);
import Editor from "@monaco-editor/react";
import FileUpload from "@Components/FileUpload/FileUpload";
import TemplateLoader from "../TemplateLoadder/TemplateLoader";
import { getLanguageFromId } from "@Utils/templates";
import LanguageSelector from "../LanguageSelector/LanguageSelector";

/**
 * Handles displaying form UI
 * Formdata is sent through the tutorials route, using document.getElementById to grab the form DOM object
 */
function TutorialForm(props) {
    const dvs = (props.defaultValues) ? props.defaultValues : {};
    const courseOptions = props.courses || [];

    const difficultyOptions = difficultylevels;

    const spacing = 5;
    const selectWidth = '300px';

    const [template, setTemplate] = useState(dvs["template"] || ``);
    const [prompt, setPrompt] = useState(dvs["prompt"] || '');

    const [languageId, setLanguageId] = useState(dvs["languageId"] || '');
    const [monacoLanguage, setMonacoLanguage] = useState('html');

    useEffect(() => {
        let uppercase = getLanguageFromId(languageId);
        let lowercase = uppercase.toLowerCase();

        if (lowercase == 'html' || lowercase == 'javascript' || lowercase == 'css') {
            setMonacoLanguage('html');
        } else
            setMonacoLanguage(uppercase.toLowerCase());
    }, [languageId]);

    return (
        <form id="tutorial_form" style={{ width: '100%' }}>
            <Flex w="90%" align={"flex-end"} ml="auto" flexDir={"column"} fontFamily="input">
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
                        <Input placeholder="..." defaultValue={dvs["title"]} required/>
                    </FormControl>

                </Flex>
                <Flex w="100%" mt={spacing}>
                    <Box w="20%" fontWeight={"bold"} fontSize={"md"}>Description</Box>
                    <FormControl w="80%" id="description">
                        <Textarea placeholder="..." defaultValue={dvs["description"]} required />
                    </FormControl>

                </Flex>
                <Flex w="100%" mt={spacing}>
                    <Box w="20%" fontWeight={"bold"} fontSize={"md"}>Language</Box>
                    <LanguageSelector languageId={languageId} callback={(newValue) => { setLanguageId(newValue) }} template={template} />
                </Flex>
                <Flex w="100%" mt={spacing}>
                    <Box w="20%" fontWeight={"bold"} fontSize={"md"}>Difficulty</Box>
                    <Select w="30%" maxW={"150px"} id="difficulty" defaultValue={dvs["difficultyId"]}>
                        {difficultyOptions.map((option, index) => {
                            const { dbIndex, value } = option;
                            return <option id={index} value={dbIndex}>{value}</option>
                        })}
                    </Select>
                </Flex>
                <Flex w="100%" mt={spacing} visibility="collapse">
                    <Box w="20%" fontWeight={"bold"} fontSize={"md"}>Solution</Box>
                    <FormControl w="80%" id="solution">
                        <Textarea placeholder="..." defaultValue={dvs["solution"]} required />
                    </FormControl>

                </Flex>
                <Flex w="100%" mt={spacing} direction="column">
                    <Box w="100%" fontWeight={"bold"} fontSize={"md"} pb="5px">Tutorial Base Code</Box>
                    <p>Choose whether you want to upload an existing code file or if you want to edit boilerplate code provided by us.</p>
                    <Spacer pb={5}/>
                    <Box id="actions">
                        <FileUpload id="fileSelect" py={0} callback={setTemplate} />
                        <TemplateLoader languageId={languageId} callback={setTemplate} />
                    </Box>
                </Flex>
            </Flex>
            <Grid pb={"10%"} pt={"20px"} pl={"10%"} id="panes" w="100%" maxW="container.lg" height="fit-content" templateColumns="repeat(2, 50%)" mx={2} fontFamily="input">
                <Box py={2}>
                    <Text fontSize={"md"} >Tutorial Instructions</Text>
                    <Text>Use checkmarks to give students tasks to complete.</Text>
                </Box>
                <Box py={2}>
                    <Text fontSize={"md"} >Template Code</Text>
                    <Text>Provide students with a basic code setup to work with.</Text>
                </Box>
                <Box>
                    <MarkdownEditor prompt={dvs["prompt"]} callback={setPrompt}/>
                </Box>
                <Box h="525px" boxShadow={"0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)"}>
                    <Editor
                        height="100%"
                        width="100%"
                        theme="vs-dark"
                        defaultLanguage="html"
                        language={monacoLanguage}
                        options={{
                            padding: {
                                top: "10px"
                            },
                            scrollBeyondLastLine: false,
                            overviewRulerBorder: false,
                            overviewRulerLanes: 0,
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