import { Grid, GridItem, HStack, Divider, Center } from "@chakra-ui/layout";
import { Tag, TagLabel } from "@chakra-ui/tag";
import Router from "next/router";
import { Button } from "@chakra-ui/react";
import { checkIfInCourse, registerForCourse } from "@Modules/Courses/Courses";
import { tutorialStatus } from "@Utils/static";

function TutorialItem(props) {
    const { token } = props;
    const { id, title, courseId, status } = props.data;
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

    /**
     * 
     * @param {integer} to Tutorial id
     * @param {integer} from Course id
     */
     async function start(event, to, from) {
        let isRegistered = await checkIfInCourse(from, token);

        let success = true;
        if (!isRegistered) {
            success = await registerForCourse(from, token);
        }
        if (success) {
            let redirect = '/tutorials/' + to; 
            Router.push(redirect);
        }
    }

    /**
     * 
     * @param {integer} to Tutorial id
     * @param {integer} from Course id
     */
    async function enter(event, to, from) {
        let success = true;
        if (success) {
            let redirect = '/tutorials/' + to; 
            Router.push(redirect);
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
                        console.log(tagData);
                        return <Tag key={name} type={tagData.type} lower={lower}>
                            <TagLabel>{name}</TagLabel>
                        </Tag>;
                    })}
                </HStack>
            </GridItem>
            <GridItem colStart={6}>
                <HStack spacing={3}>
                    {[tutorialStatus.InProgress, tutorialStatus.Restarted].includes(status) &&
                    <Button variant="white" onClick={(e) => enter(e, id, courseId)}>
                        Continue
                    </Button>
                    }
                    {[tutorialStatus.Completed].includes(status) &&
                    <Button variant="white" onClick={(e) => enter(e, id, courseId)}>
                        Restart
                    </Button>
                    }
                    {[tutorialStatus.NotStarted].includes(status) &&
                    <Button variant="white" onClick={(e) => start(e, id, courseId)}>
                        Start
                    </Button>
                    }
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

export default TutorialItem