import { Button } from "@chakra-ui/button";
import { Box } from "@chakra-ui/layout";
import { Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay } from "@chakra-ui/modal";

const { useDisclosure } = require("@chakra-ui/hooks")

/**
 * A component that puts a callback function behind a confirmation screen
 * @param {{
 *      buttonText: JSX.Element,
 *      title: String,
 *      text?: String,
 *      callback: Function
 *  }} props
 */
function Barrier(props) {
    const { buttonText, title, text, callback } = props;
    const { isOpen, onOpen, onClose } = useDisclosure()
    return (
        <>
            <Box onClick={onOpen}>{buttonText}</Box>
    
            <Modal isOpen={isOpen} onClose={onClose}>
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>{title}</ModalHeader>
                    <ModalCloseButton />
                    <ModalBody>
                    { text || 'Are you sure?'}
                    </ModalBody>
        
                    <ModalFooter>
                    <Button variant="maroon" mr={3} onClick={callback}>Yes</Button>
                    <Button variant="ghost" onClick={onClose}>
                        Close
                    </Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    )
}

export default Barrier;