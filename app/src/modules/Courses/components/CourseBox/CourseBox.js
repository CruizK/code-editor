import { Box, Button, Flex, HStack, Text, useStyleConfig } from "@chakra-ui/react";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import { loggedIn } from "@Modules/Auth/Auth";
import TutorialList from "@Modules/Tutorials/components/TutorialList/TutorialList";
import { getTutorialsFromCourseSearch } from "@Modules/Tutorials/Tutorials";
import { useEffect, useState } from "react";
import { useCookies } from "react-cookie";

function CourseBox(props) {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const token = cookies.user;

    const styles = useStyleConfig("CourseBox", {});

    const { course = {}, searchParameters, ...rest } = props;
    const { id, title, description } = course;

    const [tutorials, setTutorials] = useState([]);

    // this grabs the tutorials for a coursebox
    useEffect(async function() {
        let success = await getTutorialsFromCourseSearch(id, searchParameters, token);
        if (success) {
            setTutorials(success);
        }
    }, [id]);

    if (typeof course.id == 'undefined') {
        return (
            <Box _css={styles} {...rest}>               
                <Text mt={2} width="100%" ml="-5%">
                    You don't currently have a course selected - try searching for one.
                </Text>
            </Box>
        );
    }

    return (
        <Box __css={styles} {...rest}>
            <SectionHeader title={
                <Text maxW="100%" 
                    color="ce_black" fontWeight="bold" fontFamily="button" fontSize="md"
                >
                    {title.toUpperCase()}
                </Text>
            }>
                <SNoLinkButton 
                    href={"/courses/" + id} variant="maroon" ml="20%" maxW="80%"
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
                <TutorialList courseId={id} tutorials={tutorials} />
            </Box>
        </Box>
    )
}

export default CourseBox;