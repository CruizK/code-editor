import { useBreakpointValue } from '@chakra-ui/media-query';
import { Input } from '@chakra-ui/react';
import { ContentState, convertFromRaw, convertToRaw, EditorState } from 'draft-js';
import { draftToMarkdown as toMarkdown, markdownToDraft} from 'markdown-draft-js';
import { useState } from 'react';
import { Editor } from 'react-draft-wysiwyg';
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";
import blockRenderer from './BlockRenderFunc';

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
        if (props.callback) props.callback(newMarkdown);
    };

    const maxWidth = useBreakpointValue({ base: "350px", lg: "550px"});

    return(
        <>
        <Editor
            editorState={editorState}
            onEditorStateChange={handleEditorChange}
            toolbar={
                {
                    options: ['inline', 'blockType', 'list', 'textAlign', 'link'],
                    inline: {
                        inDropdown: false,
                        options: ['bold', 'italic', 'underline', 'strikethrough', 'monospace', 'superscript', 'subscript'],

                    }
                }
            }
            customBlockRenderFunc={blockRenderer}
            wrapperClassName="demo-wrapper"
            editorClassName="demo-editor"
            wrapperStyle={{ maxWidth: maxWidth, boxShadow: "0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)" }}
            editorStyle={{ minHeight: "450px", maxHeight: "450px", paddingLeft: "10px", paddingRight: "10px"}}
        />
        <Input id="md" type="hidden" value={markdown} />
        </>
    )
}

export default MarkdownEditor;