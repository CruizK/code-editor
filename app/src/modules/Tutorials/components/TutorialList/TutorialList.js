import { DeleteIcon, EditIcon, ViewIcon } from "@chakra-ui/icons";
import { Box, Flex, Grid, GridItem, HStack, Divider, Center } from "@chakra-ui/layout";
import { Tag, TagLabel } from "@chakra-ui/tag";
import instance from "@Utils/instance";
import { useCookies } from "react-cookie";
import { loggedIn } from "@Modules/Auth/Auth";
import { useEffect, useState } from "react";
import { deleteTutorial } from "@Modules/Tutorials/Tutorials";
import Router from "next/router";

function TutorialItem(props) {
    const { token } = props;
    const { id, title } = props.data;
    const tags = [];
    if (props.Difficulty) {
        tags.push({
            name: props.Difficulty,
            type: 'difficulties',
        });
    }
    if (props.Language) {
        tags.push({
            name: props.Language,
            type: 'languages',
        });
    }

    async function handleDeletion(id, token) {
        let success = await deleteTutorial(id, token);
        if (success) {
            Router.reload();
        }
    }

    return(
        <Grid templateColumns="repeat(5, 1fr)" gap={6} pl={5} mt={15} mb={15}>
            <GridItem>
                {title}
            </GridItem>
            <GridItem colStart={4}>
                <HStack spacing={3}>
                    {tags.map((tagData) => {
                        let name = tagData.name;
                        let lower = name.toLowerCase();
                        return <Tag key={name} type={tagData.type} lower={lower}>
                            <TagLabel>{name}</TagLabel>
                        </Tag>;
                    })}
                </HStack>
            </GridItem>
            <GridItem colStart={6}>
                <HStack spacing={3}>            
                    <ViewIcon onClick={() => {
                        let redirect = '/tutorials/' + id; 
                        Router.push(redirect);
                    }} />            
                    <EditIcon color="ce_mainmaroon" onClick={() => {
                        let redirect = '/tutorials/edit/' + id; 
                        Router.push(redirect);
                    }} />
                    <DeleteIcon onClick={() => handleDeletion(id, token)} />
                </HStack>
            </GridItem>
            <GridItem colSpan={6}>
                <Center>
                    <Divider w="100%" borderColor="ce_grey" />
                </Center>
            </GridItem>
        </Grid>
    )
}

/**
 * Handles displaying an accordion list of courses.
 */
function TutorialList(props) {
    const [tutorials, setTutorials] = useState(props.tutorials || []);
    const { courseId, getTutorials } = props;
    const headers = {};

    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const token = cookies.user;

    if (isLoggedIn) {
        headers["Authorization"] = "Bearer " + token;
    }

    useEffect(async function() {
        if (getTutorials) {
            try {       
                let response = await instance.get("/Tutorials/CourseTutorials/" + courseId, {
                    headers: {...headers},
                });
                if (response.statusText == "OK")
                setTutorials(response.data);
            } catch (error) {
                //TODO: Error handling.
                //console.log(error.response);
            }
        }
    }, [getTutorials]);

    return(
        <>
            {tutorials.map((tutorialData, index) => {
                return <TutorialItem key={index} data={tutorialData} token={token} />
            })}            
        </>
    )
}

export default TutorialList;