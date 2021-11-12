import { useBreakpointValue } from '@chakra-ui/media-query';
import { Input } from '@chakra-ui/react';
import { convertToRaw, EditorState } from 'draft-js';
import toMarkdown from 'draftjs-to-markdown';
import dynamic from 'next/dynamic'; 
import { useState } from 'react';
const Editor = dynamic(
    () => import('react-draft-wysiwyg').then(mod => mod.Editor),
    { ssr: false }
);
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";

function MarkdownEditor() {
    const [editorState, setEditorState] = useState(
        () => EditorState.createEmpty(),
    );
    const [markdown, setMarkdown] = useState('');

    const handleEditorChange = (state) => {
        setEditorState(state);
        let currentContent = convertToRaw(state.getCurrentContent());
        let newMarkdown = toMarkdown(currentContent);
        setMarkdown(newMarkdown);
    };

    const maxWidth = useBreakpointValue({ base: "350px", lg: "768px"});

    return(
        <>
        <Editor
            editorState={editorState}
            onEditorStateChange={handleEditorChange}
            wrapperClassName="demo-wrapper"
            editorClassName="demo-editor"
            wrapperStyle={{ maxWidth: maxWidth }}
        />
        <Input id="md" type="hidden" value={markdown} />
        </>
    )
}

export default MarkdownEditor;