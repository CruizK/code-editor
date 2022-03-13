import { cssVar } from "@chakra-ui/theme-tools";

const $popperBg = cssVar("popper-bg");

const PopoverStyle = {
    baseStyle: {
        popper: {
            borderColor: 'ce_black',
            fontFamily: 'body',
            color: 'ce_lightgrey',
        },
        header: {
            color: 'ce_white',
        },
        content: {
            borderRadius: '0',
            backgroundColor: 'ce_black',
            _focus: {
                boxShadow: 'none',
            }
        }
    },
}

export default PopoverStyle;