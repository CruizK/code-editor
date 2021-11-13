import { useBreakpointValue } from '@chakra-ui/media-query';
import { Input } from '@chakra-ui/react';
import { ContentState, convertFromRaw, convertToRaw, EditorState } from 'draft-js';
import { draftToMarkdown as toMarkdown, markdownToDraft} from 'markdown-draft-js';
import { useState } from 'react';
import { Editor } from 'react-draft-wysiwyg';
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";

function MarkdownEditor(props) {
    const {prompt} = props;
    const [editorState, setEditorState] = useState(
        () => {
            if (prompt) {
                let parsedContentState = convertFromRaw(markdownToDraft(prompt));
                return EditorState.createWithContent(parsedContentState);
            } else {
                return EditorState.createEmpty();
            }
        },
    );
    const [markdown, setMarkdown] = useState(prompt || '');

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