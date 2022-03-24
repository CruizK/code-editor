import { Box, Button, Flex, HStack, Text, useStyleConfig } from "@chakra-ui/react";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import TutorialList from "@Modules/Tutorials/components/TutorialList/TutorialList";

function CourseBox(props) {
    const styles = useStyleConfig("CourseBox", {});

    const { course, ...rest } = props;
    const { id, title, description } = course;

    return (
        <Box __css={styles} {...rest}>
            <SectionHeader title={
                <Text isTruncated maxW="325px" 
                    color="ce_black" fontWeight="bold" fontFamily="button" fontSize="lg"
                >
                    {title.toUpperCase()}
                </Text>
            }>
                <SNoLinkButton 
                    href={"/courses/" + id} variant="white" maxW="180px"
                >
                    Go To Course {'>'}
                </SNoLinkButton>
            </SectionHeader><br />
            <SectionHeader title={
                <Text color="ce_black">DESCRIPTION</Text>
            } />
            <Text mt={2}>
                {description}
            </Text>
            <Box borderColor="ce_grey" borderWidth="2px" borderRadius="2xl" mt={15} pl={25} pr={5}>
                <TutorialList courseId={id} getTutorials={true} />
            </Box>
        </Box>
    )
}

export default CourseBox;