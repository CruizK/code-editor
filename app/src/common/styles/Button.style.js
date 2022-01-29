const ButtonStyle = {
    // The styles all Button's have in common
    baseStyle: {
        fontWeight: "bold",
        fontFamily: "button",
        borderRadius: "md",
        width: "100%",
        paddingTop: "20px",
        paddingBottom: "20px",
        overflow: "hidden",
        textOverflow: "ellipsis",
        border: "1px solid",
        borderColor: "transparent",
        _hover: {
            backgroundColor: "transparent",
            borderColor: "black",
            boxShadow: "1px 2px #888888",
            textDecoration: "none",
        }
    },
    variants: {
        yellow: {
            color: "ce_yellow",
            borderColor: "ce_yellow",
            backgroundColor: "transparent",
            _hover: {
                color: "ce_white",
                backgroundColor: "ce_yellow",
                boxShadow: "none",
                borderColor: "ce_yellow",
            }
        },
        white: {
            color: "ce_black",
            backgroundColor: "ce_white",
            _hover: {
                color: "ce_white",
                backgroundColor: "ce_black",
                borderColor: "ce_white",
                boxShadow: "none"
            }
        },
        maroon: {
            color: "ce_white",
            backgroundColor: "ce_mainmaroon",
            _hover: {
                color: "ce_mainmaroon",
                borderColor: "ce_mainmaroon",
            }
        },
        black: {
            color: "ce_white",
            backgroundColor: "ce_black",
            _hover: {
                color: "ce_black",
                borderColor: "ce_black",
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