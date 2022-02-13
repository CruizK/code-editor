const SelectStyle = {
    baseStyle: {
        field: {
            fontFamily: "input",
            textAlign: "center",
            borderColor: "ce_black",
            fontWeight: "bold",
        },
    },
    defaultProps: {
        size: "sm",
    },
    variants: {
        outline: {
            field: {
                borderRadius: "md",
            },
        },
        maroon: {
            icon: {
                color: "ce_white",
            },
            field: {
                "> option, > optgroup": {
                    bg: "ce_mainmaroon",
                },
                color: "ce_white",
                backgroundColor: "ce_mainmaroon",
                borderRadius: "md",
            },

        }
    },
}

export default SelectStyle;