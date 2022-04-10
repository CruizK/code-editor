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
    console.log(props.children);
    return(
        <Box id="rendered" whiteSpace="pre-wrap" maxWidth={['350px', '350px', '350px', '350px', '350px', '768px']}>
            <ReactMarkdown 
                className="demo-wrapper markdown-renderer" 
                remarkPlugins={[remarkGfm]}
                components={{
                    code: ({inline, ...props}) => {
                        let className = props.className || 'language-generic';
                        if (inline)
                            return <code {...props} />;

                        return <pre class={"code " + className}><code {...props} /></pre>;
                    },
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