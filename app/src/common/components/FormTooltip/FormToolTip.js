import { QuestionIcon } from "@chakra-ui/icons";
import { Box } from "@chakra-ui/layout";
import { Tooltip } from "@chakra-ui/tooltip";

function FormToolTip(props) {
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