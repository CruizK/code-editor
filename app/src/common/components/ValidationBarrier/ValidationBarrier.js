import { Box } from "@chakra-ui/layout";
import { AlertDialog, AlertDialogBody, AlertDialogContent, AlertDialogFooter, AlertDialogHeader, AlertDialogOverlay, Button, Select } from "@chakra-ui/react";
import { useRef } from "react";

const { useDisclosure } = require("@chakra-ui/hooks")

/**
 * A component that puts an alert when the condition prop is true 
 * @param {{
 *      buttonText: JSX.Element,
 *      title: String,
 *      text?: String,
 *      callback: Function
 *  }} props
 */
function ValidationBarrier(props) {
    const { buttonText, title, text, callback, conditional } = props;
    const { isOpen, onOpen, onClose } = useDisclosure();

    const cancelRef = useRef(); // accessibility helper

    return (
        <>
            <Box onClick={() => (conditional()) ? onOpen() : callback()}>{buttonText}</Box>
        
            <AlertDialog
                isOpen={isOpen}
                leastDestructiveRef={cancelRef}
                onClose={onClose}
            >
                <AlertDialogOverlay>
                    <AlertDialogContent>
                        <AlertDialogHeader>
                            {title}
                        </AlertDialogHeader>

                        <AlertDialogBody>
                            {text || 'Validation Error'}
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button variant="ghost" ref={cancelRef} onClick={onClose}>
                            Cancel
                            </Button>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialogOverlay>
            </AlertDialog>
        </>
    );
}

export default ValidationBarrier;