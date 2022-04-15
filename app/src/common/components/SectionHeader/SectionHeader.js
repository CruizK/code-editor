import { Flex, HStack, Spacer } from "@chakra-ui/layout";
import { Heading } from "@chakra-ui/react";

/**
 * The header for each section of content on a page.
 */
function SectionHeader(props) {
    const {title, children, justifyContent = 'start', ...rest} = props;

    return(
        <Flex>
            <Heading size="sm" pb="5px" color="ce_middlegrey">
                {title}
            </Heading>
            <Spacer />
            <HStack spacing={2} w="40%" justifyContent={justifyContent}>
                {children}
            </HStack>
        </Flex>
    )
}

export default SectionHeader;