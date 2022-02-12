import { AlertDialog, AlertDialogBody, AlertDialogContent, AlertDialogFooter, AlertDialogHeader, AlertDialogOverlay, Button, Select } from "@chakra-ui/react";
import { programmingLanguages } from "@Utils/static";
import { useRef, useState } from "react";

/**
 * A container for the language selection logic. We could just wrap the Select with a Barrier, theoretically, but that gets messy quickly by complicating Barrier.js' logic. 
 * @param {{
 *      languageId: Number,
 *      template: String,
 *  }} props
 * @returns 
 */
function LanguageSelector(props) {
    const { id, callback, ...rest } = props;
    const [languageId, setLanguageId] = useState(props.languageId);
    const [tempLangId, setTempLangId] = useState(null); // buffer

    const languageOptions = programmingLanguages;

    const [isOpen, setIsOpen] = useState(false);
    const onClose = () => setIsOpen(false);
    const cancelRef = useRef(); // accessibility helper

    /**
     * We don't want to immediately change languageId UNLESS template is empty, so we'll temporarily store it and ask for confirmation.
     * @param {*} event 
     */
    function handleChange(event) {
        let newId = event.target.value;

        if (props.template && props.template != '') {
            setIsOpen(true); 
            setTempLangId(newId);
        } else {
            setLanguageId(newId);
            if (callback) callback(newId); 
        }
    }

    /**
     * If they click Yes, then we're fine to change languageId.
     */
    function handleCommit() {
        onClose(); 
        setLanguageId(tempLangId);
        if (callback) callback(tempLangId); 

        // clear buffer
        setTempLangId(null);
    }

    return(
        <>        
            <Select w="30%" maxW="150px" id="language" value={languageId} onChange={handleChange}>
            {languageOptions.map((option, index) => {
                const { dbIndex, value } = option;
                return <option id={index} value={dbIndex}>{value}</option>
            })}
            </Select>
            <AlertDialog
                isOpen={isOpen}
                leastDestructiveRef={cancelRef}
                onClose={onClose}
            >
                <AlertDialogOverlay>
                    <AlertDialogContent>
                        <AlertDialogHeader>
                            Confirmation
                        </AlertDialogHeader>

                        <AlertDialogBody>
                            Changing the language will erase any code changes you have made. Are you sure you want to change the language?
                        </AlertDialogBody>

                        <AlertDialogFooter>
                            <Button variant="maroon" onClick={handleCommit} ml={3}>
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

export default LanguageSelector;