import { getCodeTemplates, getLanguageFromId } from "@Utils/templates";
import { Select } from "@chakra-ui/react";
import { useEffect, useRef, useState } from "react";

function TemplateLoader(props) {
    const { id, callback, ...rest } = props;
    const [text, setText] = useState('');
    const [templates, setTemplates] = useState([]);

    const isInitialMount = useRef(true); // see https://reactjs.org/docs/hooks-faq.html#can-i-run-an-effect-only-on-updates

    useEffect(async function() {
        let language = getLanguageFromId(props.languageId);

        let success = await getCodeTemplates(language);
        console.log('language changed', success);
        if (success) {
            setTemplates(success);
        }

        // only on update
        if (isInitialMount.current) {
            isInitialMount.current = false;
        } else {
            setText(''); if (callback) callback('');
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
            value={text}
        >
            <option key="placeholder" value=''>Edit a Template</option>
        {
            templates.map((tempData, tempIndex) => {
                return <option key={tempIndex} value={tempData.code}>{tempData.template}</option>
            })
        }
        </Select>
    )
}

export default TemplateLoader;