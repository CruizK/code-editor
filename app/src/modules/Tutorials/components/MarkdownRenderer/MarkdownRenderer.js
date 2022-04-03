import { useBreakpointValue } from '@chakra-ui/media-query';
import { Box } from '@chakra-ui/react';
import ReactMarkdown from 'react-markdown';
import remarkGfm from 'remark-gfm';

/**
 * A renderer for github-style markdown. Pass markdown as a direct plain-text child to ReactMarkdown. 
 * @param {Object} props 
 * @returns A 'rendered' div wrapping a ReactMarkDown instance
 */
function MarkdownRenderer(props) {
    const maxWidth = useBreakpointValue({ base: "350px", lg: "768px"});

    return(
        <Box id="rendered" whiteSpace="pre-wrap" maxWidth={maxWidth}>
            <ReactMarkdown 
                className="demo-wrapper markdown-renderer" 
                remarkPlugins={[remarkGfm]}
                components={{
                    input: ({disabled, checked, ...props}) => {
                        //console.log(props);
                        props.node.properties.checked = undefined;
                        props.node.properties.disabled = undefined;
                        return <input disabled={(props.type !== 'checkbox') ? true : undefined} {...props} />
                    }
                }}
            >
                {props.children}
            </ReactMarkdown>
        </Box>
    )
}

export default MarkdownRenderer;