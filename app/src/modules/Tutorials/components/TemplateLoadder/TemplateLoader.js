import { getCodeTemplates, getLanguageFromId } from "@Utils/templates";
import { Select } from "@chakra-ui/react";
import { useEffect, useRef, useState } from "react";

function TemplateLoader(props) {
    const { id, callback, ...rest } = props;
    const [text, setText] = useState(null);
    const [templates, setTemplates] = useState([]);

    useEffect(async function() {
        let language = getLanguageFromId(props.languageId);

        let success = await getCodeTemplates(language);
        console.log('language changed', success);
        if (success) {
            setTemplates(success);
        }
    }, [props.languageId]);

    function handleChange(event) {
        //console.log(event);
        let newText = event.target.value;

        setText(newText);
        if (callback) callback(newText);
    }
    
    return(
        <Select onChange={handleChange}
            display="inline-block"
            w="20%" maxW="170px" mr={2}
            variant="maroon"
            placeholder='Edit A Template'
        >
        {
            templates.map((tempData, tempIndex) => {
                return <option key={tempIndex} value={tempData.code}>{tempData.template}</option>
            })
        }
        </Select>
    )
}

export default TemplateLoader;