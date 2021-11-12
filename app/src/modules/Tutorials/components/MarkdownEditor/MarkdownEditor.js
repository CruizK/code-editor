import { useBreakpointValue } from '@chakra-ui/media-query';
import { convertToRaw, EditorState } from 'draft-js';
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
    )

    const handleEditorChange = (state) => {
        setEditorState(state);
    };

    const maxWidth = useBreakpointValue({ base: "350px", lg: "768px"});

    return(
        <Editor
            editorState={editorState}
            onEditorStateChange={handleEditorChange}
            wrapperClassName="demo-wrapper"
            editorClassName="demo-editor"
            wrapperStyle={{ maxWidth: maxWidth }}
        />
    )
}

export default MarkdownEditor;