import { getDefaultKeyBinding } from "draft-js";
import { hasCommandModifier } from "draft-js/lib/KeyBindingUtil";
import keycode from 'keycode';

function customKeyBindingFn(e) {
    if (hasCommandModifier(e) && e.shiftKey && keycode(e) === 'c') {
       return 'insert-checkbox';
    }
    return getDefaultKeyBinding(e);
}

export { customKeyBindingFn }