const SNoLinkButtonStyle = {
    // The styles all SNoLinkButton's have in common
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
    },
}

export default SNoLinkButtonStyle;