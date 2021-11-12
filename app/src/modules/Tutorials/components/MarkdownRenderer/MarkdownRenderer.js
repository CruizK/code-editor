import toMarkdown from 'draftjs-to-markdown';
import { useEffect, useState } from 'react';
import ReactMarkdown from 'react-markdown';

function MarkdownRenderer(props) {

    const [md, setMD] = useState("No md passed");
    useEffect(() => {
        if (typeof window !== 'undefined')
        setMD(toMarkdown(props.raw));
    }, [props.raw])

    return(
        <ReactMarkdown>
            {md}
        </ReactMarkdown>
    )
}

export default MarkdownRenderer;