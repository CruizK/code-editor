import { Flex, HStack, Spacer } from "@chakra-ui/layout";
import { Heading } from "@chakra-ui/react";

/**
 * The header for each section of content on a page.
 */
function SectionHeader(props) {
    const {title, children, ...rest} = props;

    return(
        <Flex>
            <Heading size="sm" color="ce_middlegrey">
                {title}
            </Heading>
            <Spacer />
            <HStack spacing={2} w="40%">
                {children}
            </HStack>
        </Flex>
    )
}

export default SectionHeader;