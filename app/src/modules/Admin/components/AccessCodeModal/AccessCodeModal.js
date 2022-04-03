import { CopyIcon } from "@chakra-ui/icons";
import { Button, Center, Code, Flex, FormLabel, HStack, Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay, Radio, RadioGroup, Spacer, Stack, Text, useClipboard, useDisclosure } from "@chakra-ui/react"
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import { generateAccessCode } from "@Modules/Admin/Admin";
import { useState } from "react";
import { useCookies } from "react-cookie";

function AccessCodeModal() {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const token = cookies.user;

    const { isOpen, onOpen, onClose } = useDisclosure();
    
    const [userRole, setUserRole] = useState('2');

    const [accesscode, setAccesscode] = useState(null);
    const { hasCopied, onCopy } = useClipboard(accesscode);

    async function generate() {
        const code = await generateAccessCode(userRole, token);

        if (code)
            setAccesscode(code);
    }

    return(
        <>
            <Button variant="maroon" onClick={onOpen}>Open Modal</Button>

            <Modal isOpen={isOpen} onClose={() => { onClose(); setAccesscode(null) }}>
                <ModalOverlay />
                <ModalContent borderRadius="3xl" px="25px" maxW="40%">
                    <ModalHeader></ModalHeader>
                    <ModalCloseButton />
                    
                    <ModalBody pt="12%" pb={(!accesscode) ? "12%" : "0%"}>
                        <RadioGroup onChange={setUserRole} value={userRole} py="20px">
                            <Stack direction='row'>
                                <FormLabel>User Role</FormLabel>
                                <Radio value='2'>Teacher</Radio>
                                <Radio value='1'>Admin</Radio>
                            </Stack>
                        </RadioGroup>
                        <Button onClick={generate} 
                            size="md" py="40px" variant='maroon'
                        >Generate Access Code</Button>
                    </ModalBody>

                    <ModalFooter>
                        {accesscode &&
                            <Flex direction="column" w="100%" alignItems="end">
                                <Center mb={4} w="100%">
                                    <Code w="100%">
                                        {accesscode}
                                    </Code>
                                </Center>
                                <HStack spacing={2} w="50%">
                                    <SNoLinkButton href={`mailto:someone@somewhere.com?subject=Access Code from siuCode&body=${accesscode}`} variant="white">
                                        Email Code
                                    </SNoLinkButton>
                                    <Button variant="black" onClick={onCopy}>
                                        {hasCopied ? 
                                            'Copied' : 
                                            <>
                                                <Text mr={3}>Copy</Text><CopyIcon />
                                            </>
                                        }
                                    </Button>
                                </HStack>
                            </Flex>
                        }
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    )
}

export default AccessCodeModal