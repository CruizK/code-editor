import { Button, FormLabel, Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay, Radio, RadioGroup, Stack, useDisclosure } from "@chakra-ui/react"
import { useState } from "react";

function AccessCodeModal() {
    const { isOpen, onOpen, onClose } = useDisclosure();

    const [userRole, setUserRole] = useState('Teacher');

    function generate() {

    }

    return(
        <>
            <Button variant="maroon" onClick={onOpen}>Open Modal</Button>

            <Modal isOpen={isOpen} onClose={onClose}>
                <ModalOverlay />
                <ModalContent borderRadius="3xl" px="25px" maxW="40%">
                    <ModalHeader></ModalHeader>
                    <ModalCloseButton />
                    
                    <ModalBody py="12%">
                        <RadioGroup onChange={setUserRole} value={userRole} py="20px">
                            <Stack direction='row'>
                                <FormLabel>User Role</FormLabel>
                                <Radio value='Teacher'>Teacher</Radio>
                                <Radio value='Admin'>Admin</Radio>
                            </Stack>
                        </RadioGroup>
                        <Button size="md" py="40px" variant='maroon'>Secondary Action</Button>
                    </ModalBody>

                    <ModalFooter></ModalFooter>
                </ModalContent>
            </Modal>
        </>
    )
}

export default AccessCodeModal