import { ChevronLeftIcon, ChevronRightIcon, HamburgerIcon, SmallCloseIcon } from "@chakra-ui/icons";
import { Box, Collapse, Flex, Icon, List, ListItem, Popover, PopoverBody, PopoverContent, PopoverHeader, PopoverTrigger, Spacer } from "@chakra-ui/react";
import SNoLink from "@Components/SNoLink/SNoLink";
import dynamic from 'next/dynamic'; 
import { useRef } from "react";
const MarkdownRenderer = dynamic(
  () => import("@Modules/Tutorials/components/MarkdownRenderer/MarkdownRenderer"),
  { ssr: false }
);

function TutorialSideBar(props) {
    const { courseId, prompt, show, tutorials } = props;
  
    const handleToggle = () => { props.setShow(!show); };

    const initRef = useRef();

    return(
        <Flex flex={(show) ? "1" : "0"} direction="column">
          <Flex h="50px" pr={3} bg="ce_mainmaroon" color="ce_white" align={"center"}>
            <Popover closeOnBlur={false} initialFocusRef={initRef} offset={[0, 0]}>
              {({ isOpen, onClose }) => (
                <>
                  <PopoverTrigger>
                    <Icon cursor="pointer"
                      backgroundColor={(isOpen) ? 'ce_black' : 'ce_mainmaroon'}
                      pl={(isOpen) ? 0 : 2}
                      as={(isOpen) ? SmallCloseIcon : HamburgerIcon } 
                      w={(isOpen) ? 10: 8} 
                      h={(isOpen) ? '100%' : 8} 
                    />
                  </PopoverTrigger>
                  <PopoverContent>
                    <PopoverHeader><SNoLink href={`/courses/${courseId}`}>Back to Course</SNoLink></PopoverHeader>
                    <PopoverBody>
                      <List>
                      {tutorials.map((tutorialData, index) => {
                        //console.log(tutorialData);
                        const { id, title } = tutorialData;
                        return <ListItem key={index}><SNoLink href={`/tutorials/${id}`}>{title}</SNoLink></ListItem>;
                      })}
                      </List>
                    </PopoverBody>
                  </PopoverContent>
                </>
              )}
            </Popover>
            {show && <>
              <Spacer />
              <ChevronLeftIcon w={8} h={8} onClick={handleToggle} cursor="pointer" />
            </>}
            {!show &&
              <ChevronRightIcon w={8} h={8} onClick={handleToggle} cursor="pointer" />
            }
          </Flex>
          <Box overflowY="scroll">
            <Collapse in={show}>
              <MarkdownRenderer>
                {prompt}
              </MarkdownRenderer>
            </Collapse>
          </Box>
        </Flex>
    )
}

export default TutorialSideBar;