import { Modifier, EditorState, Entity } from 'draft-js';

function insertCheckbox(editorState) {
    // Define the checkbox entity
    const entityKey = Entity.create('CHECKBOX', 'IMMUTABLE', { checked: false });
    
    // Collapse selection
    var selectionState = editorState.getSelection();
    selectionState = selectionState.merge({
        anchorOffset: selectionState.getFocusOffset(),
    });

    // Insert the checkbox text
    let newContent = Modifier.insertText(
        editorState.getCurrentContent(),
        selectionState,
        '- [ ]\n'
    );

    // Set Checkbox Entity on this element
    newContent = Modifier.applyEntity(
        newContent,
        newContent.getSelectionAfter(),
        entityKey
    );

    // Push new EditorState
    const newEditorState = EditorState.push(
        editorState,
        newContent,
        'insert-checkbox'
    );

    return EditorState.forceSelection(
        newEditorState,
        newContent.getSelectionAfter()
    );
}

function CheckBoxButton(props) {
    const { handler, editorState } = props;

    return(
        <div onClick={() => handler(insertCheckbox(editorState))}>
            ✔️
        </div>
    )
}

export { CheckBoxButton, insertCheckbox }