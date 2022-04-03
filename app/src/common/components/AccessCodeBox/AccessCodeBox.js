import { Box, Button, Center, Flex, Heading, Text } from "@chakra-ui/react";

function AccessCodeBox() {
    return(
        <Center
            bgColor="ce_backgroundlighttan" color="ce_white" 
            w="100%" height="60px" px="5%" py="5px"
        >
            <Flex direction="column" w="88%">
                <Heading fontWeight="bold" size="xs" color="ce_mainmaroon" textTransform="uppercase">
                    Generate an access code for new Teacher/ADMIN user accounts. 
                </Heading>
                <Text color="ce_middlegrey">
                    By generating and sending this code to the user, you are authorizing access to creating and editing content through this site.
                </Text>
            </Flex>
            <Center w="12%">
                <Button variant="maroon" />
            </Center>
        </Center>
    )
}

export default AccessCodeBox;