const ButtonStyle = {
    // The styles all Button's have in common
    baseStyle: {
        fontWeight: "bold",
        fontFamily: "button",
        borderRadius: "md",
        width: "100%"
    },
    variants: {
        white: {
            backgroundColor: "ce_white",
        },
        maroon: {
            backgroundColor: "ce_mainmaroon",
        },
        black: {
            backgroundColor: "ce_black",
        },
    },
    // The default variant value
    defaultProps: {
        variant: "maroon",
        size: "sm",
    },
}

export default ButtonStyle;