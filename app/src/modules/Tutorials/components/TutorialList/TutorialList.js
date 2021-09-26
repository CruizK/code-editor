import { DeleteIcon, EditIcon } from "@chakra-ui/icons";
import { Box, Flex, Grid, GridItem, HStack, Divider, Center } from "@chakra-ui/layout";
import { Tag } from "@chakra-ui/tag";

function TutorialItem(props) {
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

    return(
        <Grid templateColumns="repeat(5, 1fr)" gap={6} pl={5} mt={15} mb={15}>
            <GridItem>
                {props.title}
            </GridItem>
            <GridItem colStart={4}>
                <HStack spacing={3}>
                    {tags.map((tagData) => {
                        let name = tagData.name;
                        let color = tagData.type + "." + name.toLowerCase();
                        return <Tag key={name} color="ce_white" backgroundColor={color}>{name}</Tag>;
                    })}
                </HStack>
            </GridItem>
            <GridItem colStart={6}>
                <HStack spacing={3}>                        
                    <EditIcon />
                    <DeleteIcon />
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
    return(
        <>
            {props.tutorials.map((tutorialData) => {
                return <TutorialItem key={tutorialData.title} {...tutorialData} />
            })}            
        </>
    )
}

export default TutorialList;