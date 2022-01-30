import { ChevronLeftIcon, HamburgerIcon } from "@chakra-ui/icons";
import { Flex, Spacer } from "@chakra-ui/react";
import dynamic from 'next/dynamic'; 
const MarkdownRenderer = dynamic(
  () => import("@Modules/Tutorials/components/MarkdownRenderer/MarkdownRenderer"),
  { ssr: false }
);

function TutorialSideBar(props) {
    const { prompt } = props;

    return(
        <>
        <Flex h="50px" px={3} bg="ce_mainmaroon" color="ce_white" align={"center"}>
            <HamburgerIcon w={8} h={8}/>
            <Spacer />
            <ChevronLeftIcon w={8} h={8}/>
        </Flex>
        <Flex flex="1" p={3} bg="ce_backgroundlighttan" >
            <MarkdownRenderer>
                {prompt}
            </MarkdownRenderer>
        </Flex>
        </>
    )
}

export default TutorialSideBar;