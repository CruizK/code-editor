import { Search2Icon } from "@chakra-ui/icons";
import { Box, Center, Flex, HStack, Input, Select, Spacer, Text, VStack } from "@chakra-ui/react";
import Carousel from "@Components/Carousel/Carousel";
import Main from "@Components/Main/Main";
import SNoLink from "@Components/SNoLink/SNoLink";
import { loggedIn } from "@Modules/Auth/Auth";
import CourseBox from "@Modules/Courses/components/CourseBox/CourseBox";
import { getCoursesFromSearch } from "@Modules/Courses/Courses";
import { difficultylevels, programmingLanguages } from "@Utils/static";
import withAuthorization from "@Utils/withAuthorization";
import { useEffect, useState } from "react";
import { useCookies } from "react-cookie";

function Search() {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const token = cookies.user;

    const [searchString, setSearchString] = useState('');
    const [difficultyId, setDifficulty] = useState(0);    
    const [languageId, setLanguage] = useState(0);

    const [searchParameters, setSearch] = useState({
        searchString: searchString,
        difficultyId: difficultyId,
        languageId: languageId,
    });

    const [courses, setCourses] = useState([]);

    const [currentCourse, setCurrentCourse] = useState(courses[0]);

    /**
     * We don't want to immediately call the API, hence updating a buffer object
     */
    useEffect(() => {
        var newSearchParameters = searchParameters;

        if (searchString != searchParameters.searchString)
            newSearchParameters.searchString = searchString;
        
        if (difficultyId != searchParameters.difficultyId)
            newSearchParameters.difficultyId = difficultyId;

        if (languageId != searchParameters.languageId)
            newSearchParameters.languageId = languageId;

        setSearch(newSearchParameters);
    }, [searchString, difficultyId, languageId]);

    async function handleSearch() {
        let success = await getCoursesFromSearch(searchParameters, token);
        if (success) {
            setCourses(success);
        }
    }

    function handleCarouselClick(courseId) {
        const tempCourse = courses.find((tc) => {
            return tc.id == courseId;
        });

        setCurrentCourse(tempCourse);
    }

    return (
        <Main>
            <VStack>
                <SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink>
                <HStack spacing={3} id="search">
                    <Input w="lg" id="searchBar" placeholder="Search" onChange={(e) => setSearchString(e.target.value)} />
                    <Search2Icon onClick={handleSearch}
                        color="ce_white" boxSize="2em" borderRadius="md" backgroundColor="ce_mainmaroon" padding={2} 
                    />
                </HStack>
                <Spacer />
                <HStack w="lg">
                    <Spacer />
                    <Select flexBasis="132px" onChange={(e) => setDifficulty(e.target.value)}>
                        <option id={"Any"} value={0}>Difficulty</option>
                        {difficultylevels.map((option, index) => {
                            const { dbIndex, value } = option;
                            return <option id={index} value={dbIndex}>{value}</option>
                        })}
                    </Select>
                    <Spacer />
                    <Select flexBasis="132px" onChange={(e) => setLanguage(e.target.value)}>
                        <option id={"Any"} value={0}>Language</option>
                        {programmingLanguages.map((option, index) => {
                            const { dbIndex, value } = option;
                            return <option id={index} value={dbIndex}>{value}</option>
                        })}
                    </Select>
                    <Spacer />
                </HStack>
            </VStack>
            <Flex w="100%" minHeight="654px" alignItems="left" mt={5}>
                <VStack flexBasis="25%">
                    <Carousel clickOverride={handleCarouselClick} direction={'vertical'} items={courses} />
                </VStack>
                <Spacer flexBasis="5%" />
                <CourseBox course={currentCourse} searchParameters={searchParameters} flexBasis="70%" h="100%" />
            </Flex>
        </Main>
    )
}

export default withAuthorization(Search);