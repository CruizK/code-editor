import { ChevronLeftIcon, ChevronRightIcon, HamburgerIcon } from "@chakra-ui/icons";
import { Collapse, Flex, Spacer } from "@chakra-ui/react";
import dynamic from 'next/dynamic'; 
const MarkdownRenderer = dynamic(
  () => import("@Modules/Tutorials/components/MarkdownRenderer/MarkdownRenderer"),
  { ssr: false }
);

function TutorialSideBar(props) {
    const { prompt, show } = props;
  
    const handleToggle = () => { props.setShow(!show); };

    return(
        <Flex flex={(show) ? "1" : "0"} direction="column">
          <Flex h="50px" px={3} bg="ce_mainmaroon" color="ce_white" align={"center"}>
            <HamburgerIcon w={8} h={8} onClick={handleToggle} cursor="pointer" />
            {show && <>
              <Spacer />
              <ChevronLeftIcon w={8} h={8} onClick={handleToggle} cursor="pointer" />
            </>}
            {!show &&
              <ChevronRightIcon w={8} h={8} onClick={handleToggle} cursor="pointer" />
            }
          </Flex>
          <Collapse in={show}>
            <MarkdownRenderer>
              {prompt}
            </MarkdownRenderer>
          </Collapse>
        </Flex>
    )
}

export default TutorialSideBar;