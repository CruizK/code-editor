import { ChevronLeftIcon, ChevronRightIcon, HamburgerIcon, SmallCloseIcon } from "@chakra-ui/icons";
import { Collapse, Flex, Icon, Popover, PopoverArrow, PopoverBody, PopoverCloseButton, PopoverContent, PopoverHeader, PopoverTrigger, Spacer } from "@chakra-ui/react";
import dynamic from 'next/dynamic'; 
import { useRef } from "react";
const MarkdownRenderer = dynamic(
  () => import("@Modules/Tutorials/components/MarkdownRenderer/MarkdownRenderer"),
  { ssr: false }
);

function TutorialSideBar(props) {
    const { prompt, show, tutorials } = props;
    console.log(tutorials);
  
    const handleToggle = () => { props.setShow(!show); };

    const initRef = useRef();

    return(
        <Flex flex={(show) ? "1" : "0"} direction="column">
          <Flex h="50px" pr={3} bg="ce_mainmaroon" color="ce_white" align={"center"}>
            <Popover closeOnBlur={false} initialFocusRef={initRef} offset={[0, 0]}>
              {({ isOpen, onClose }) => (
                <>
                  <PopoverTrigger>
                    <Icon cursor="pointer"s
                      backgroundColor={(isOpen) ? 'ce_black' : 'ce_mainmaroon'}
                      as={(isOpen) ? SmallCloseIcon : HamburgerIcon } 
                      w={(isOpen) ? 10: 8} 
                      h={(isOpen) ? '100%' : 8} 
                    />
                  </PopoverTrigger>
                  <PopoverContent>
                    <PopoverHeader>Back to Course</PopoverHeader>
                    <PopoverBody>Are you sure you want to have that milkshake?</PopoverBody>
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
          <Collapse in={show}>
            <MarkdownRenderer>
              {prompt}
            </MarkdownRenderer>
          </Collapse>
        </Flex>
    )
}

export default TutorialSideBar;