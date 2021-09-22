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
            color: "ce_black",
            backgroundColor: "ce_white",
            _hover: {
                color: "ce_mainmaroon",
            }
        },
        maroon: {
            color: "ce_white",
            backgroundColor: "ce_mainmaroon",
            _hover: {
                backgroundColor: "ce_hovermaroon",
            }
        },
        black: {
            color: "ce_white",
            backgroundColor: "ce_black",
            _hover: {
                backgroundColor: "ce_darkgrey",
            }
        },
    },
    // The default variant value
    defaultProps: {
        variant: "maroon",
        size: "sm",
    },
    sizes: {
        md: {
            h: 12,
            minW: 10,
            fontSize: "md",
            px: 4,
        },
    }
}

export default ButtonStyle;