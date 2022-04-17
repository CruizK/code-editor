import { DeleteIcon, EditIcon, ViewIcon } from "@chakra-ui/icons";
import { Grid, GridItem, HStack, Divider, Center } from "@chakra-ui/layout";
import { Tag, TagLabel } from "@chakra-ui/tag";
import { deleteTutorial } from "@Modules/Tutorials/Tutorials";
import Router from "next/router";

function EditableTutorialItem(props) {
    const { token } = props;
    const { id, title, courseId } = props.data;
    const tags = [];
    if (props.data.difficulty) {
        var difficultyObject = props.data.difficulty;
        tags.push({
            name: difficultyObject.difficulty,
            type: 'difficulties',
        });
    }
    if (props.data.language) {
        var languageObject = props.data.language;
        tags.push({
            name: languageObject.language,
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
        <Grid templateColumns="repeat(5, 1fr)" gap={3} pl={5} mt={3}>
            <GridItem colStart={1} colEnd={4}>
                {title}
            </GridItem>
            <GridItem colStart={4}>
                <HStack spacing={3}>
                    {tags.map((tagData) => {
                        let name = tagData.name;
                        let lower = name.toLowerCase();
                        if (lower == 'c#') lower = 'csharp';
                        return <Tag key={name} type={tagData.type} lower={lower}>
                            <TagLabel>{name}</TagLabel>
                        </Tag>;
                    })}
                </HStack>
            </GridItem>
            <GridItem colStart={6}> 
                <HStack spacing={3} align="center">            
                    <ViewIcon cursor="pointer" onClick={() => {
                        let redirect = '/tutorials/' + id; 
                        Router.push(redirect);
                    }} />            
                    <EditIcon cursor="pointer" color="ce_mainmaroon" onClick={() => {
                        let redirect = '/tutorials/edit/' + id; 
                        Router.push(redirect);
                    }} />
                    <DeleteIcon cursor="pointer" onClick={() => handleDeletion(id, token)} />
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

export default EditableTutorialItem