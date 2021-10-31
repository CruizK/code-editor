import { QuestionIcon } from "@chakra-ui/icons";
import { Box } from "@chakra-ui/layout";
import { Tooltip } from "@chakra-ui/tooltip";

/**
 * Pass hovertext as an array of strings, with the assumption being that each string is rendered on its own line.
 * @param {{lines:string[]}} props 
 * @returns A form element which displays relevant hovertext
 */
function FormToolTip(props) {
    // chakra Tooltip's don't handle whitespace="pre" or any similar setting
    // hence the messy workaround
    let label = 
    <ul>
        {props.lines.map((line) => {
            return(<li>
                {line}
            </li>)
        })}
    </ul>;

    return(
        <Tooltip label={label} aria-label={label} placement="right" borderRadius="md">        
            <QuestionIcon pl={1}/>
        </Tooltip>
    );
}

export default FormToolTip;