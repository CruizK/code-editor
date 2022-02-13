import { Box } from "@chakra-ui/layout";
import { AlertDialog, AlertDialogBody, AlertDialogContent, AlertDialogFooter, AlertDialogHeader, AlertDialogOverlay, Button, Select } from "@chakra-ui/react";
import { useRef } from "react";

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
    const { isOpen, onOpen, onClose } = useDisclosure();

    const cancelRef = useRef(); // accessibility helper

    return (
        <>
            <Box onClick={onOpen}>{buttonText}</Box>
        
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
                            {text || 'Are you sure?'}
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button variant="maroon" onClick={callback} ml={3}>
                            Confirm
                            </Button>
                            <Button variant="ghost" ref={cancelRef} onClick={onClose}>
                            Cancel
                            </Button>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialogOverlay>
            </AlertDialog>
        </>
    )
}

export default Barrier;