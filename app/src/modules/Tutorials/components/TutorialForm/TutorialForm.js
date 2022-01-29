import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Flex, Box } from "@chakra-ui/layout";
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

    const spacing = 5;
    const selectWidth = '200px';

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
                <Flex direction={"column"} mt={spacing + 5} align="start" w="100%" mb="10%">
                    <Box  fontSize={"md"} py={2}>Tutorial Instructions</Box>
                    <MarkdownEditor prompt={dvs["prompt"]} />
                </Flex>
            </Flex>
        </form>
    );
}

export default TutorialForm;