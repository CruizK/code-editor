import { getCodeTemplates, getLanguageFromId } from "@Utils/templates";
import { Select } from "@chakra-ui/react";
import { useEffect, useState } from "react";

function TemplateLoader(props) {
    const { id, callback, ...rest } = props;
    const [text, setText] = useState(null);
    const [templates, setTemplates] = useState([]);
    const [language, setLanguage] = useState(null);

    useEffect(function() {
        let name = getLanguageFromId(props.languageId);
        setLanguage(name);
    }, [props.languageId])

    useEffect(async function() {
        let success = await getCodeTemplates(language);
        console.log(success);
        if (success) {
            setTemplates(success);
        }
    }, [language]);

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