import { useBreakpointValue } from '@chakra-ui/media-query';
import { Input } from '@chakra-ui/react';
import { DefaultDraftBlockRenderMap } from 'draft-js';
import { ContentState, convertFromRaw, convertToRaw, EditorState } from 'draft-js';
import RichTextEditorUtil from 'draft-js/lib/RichTextEditorUtil';
import Immutable from 'immutable';
import { draftToMarkdown as toMarkdown, markdownToDraft } from 'markdown-draft-js';
import { useState } from 'react';
import { Editor } from 'react-draft-wysiwyg';
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";
import blockRenderer from './BlockRenderFunc';
import { CheckBoxButton, insertCheckbox } from './insertCheckbox';
import { customKeyBindingFn } from './keybinds';

function MarkdownEditor(props) {
    const { prompt } = props;
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
        let newMarkdown = toMarkdown(currentContent, {
            styleItems: {
                code: {
                    open: function (entity, block) {
                        return '```'
                    },

                    close: function (entity, block) {
                        return '```'
                    }
                }
            }
        });
        console.log(newMarkdown);
        setMarkdown(newMarkdown);
        if (props.callback) props.callback(newMarkdown);
    };

    function handleKeyCommand(command) {
        let newEditorState = null;
        switch (command) {
            case 'insert-checkbox':
                newEditorState = insertCheckbox(editorState);
                break;
            default:
                newEditorState = RichTextEditorUtil.handleKeyCommand(editorState, command);
        }
        console.log(command, newEditorState);
        setEditorState(newEditorState);
    }

    const maxWidth = useBreakpointValue({ base: "350px", lg: "550px" });

    return (
        <>
            <Editor
                editorState={editorState}
                handleKeyCommand={handleKeyCommand}
                keyBindingFn={customKeyBindingFn}
                onEditorStateChange={handleEditorChange}
                toolbar={
                    {
                        options: ['inline', 'blockType', 'list', 'textAlign'],
                        blockType: {
                            inDropdown: true,
                            options: ['Normal', 'H1', 'H2', 'Code']
                        },
                        inline: {
                            inDropdown: false,
                            options: ['bold', 'italic', 'underline', 'strikethrough', 'monospace', 'superscript', 'subscript'],
                        }
                    }
                }
                toolbarCustomButtons={[
                    <CheckBoxButton editorState={editorState} handler={handleEditorChange} />
                ]}
                wrapperClassName="demo-wrapper"
                editorClassName="demo-editor"
                wrapperStyle={{ 
                    maxWidth: maxWidth, 
                    boxShadow: "0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)" 
                }}
                editorStyle={{ 
                    minHeight: "450px", 
                    maxHeight: "450px", 
                    paddingLeft: "10px", 
                    paddingRight: "10px",
                    fontSize: "16px",
                }}
            />
            <Input id="md" type="hidden" value={markdown} />
        </>
    )
}

export default MarkdownEditor;