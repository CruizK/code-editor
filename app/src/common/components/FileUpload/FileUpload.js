import { ArrowUpIcon } from "@chakra-ui/icons";
import { Button, Input } from "@chakra-ui/react";
import { useState } from "react";

/**
 * Renders a button that calls on a hidden file select input.
 * Internal file input has id 'fileElem'
 * @param {*} props 
 * @returns 
 */
function FileUpload(props) {
    const { id, callback, ...rest } = props;
    const [text, setText] = useState(null);

    function pseudoClick(event) {
        //console.log(event);
        event.target.previousElementSibling.click(); // click input
    }

    async function handleChange(event) {
        //console.log(event);
        let file = event.target.files[0];

        let fileText = await file.text();
        //console.log(fileText);
        setText(fileText);
        if (callback) callback(fileText);
    }

    return(
        <>
            <Input type="file" id="fileElem" display="none" onChange={handleChange} />
            <Button id={id} w="20%" maxW="150px" mr={2} variant="black" {...rest} onClick={pseudoClick}>Upload <ArrowUpIcon ml={5} /></Button>
        </>
    )
}

export default FileUpload;