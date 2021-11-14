import { useBreakpointValue } from '@chakra-ui/media-query';
import { Box } from '@chakra-ui/react';
import toMarkdown from 'draftjs-to-markdown';
import { useEffect, useState } from 'react';
import ReactMarkdown from 'react-markdown';

function MarkdownRenderer(props) {
    const maxWidth = useBreakpointValue({ base: "350px", lg: "768px"});

    return(
        <Box id="rendered" whiteSpace="pre-wrap" maxWidth={maxWidth}>
            <ReactMarkdown className="Huh?">
                {props.children}
            </ReactMarkdown>
        </Box>
    )
}

export default MarkdownRenderer;