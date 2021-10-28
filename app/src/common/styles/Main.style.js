const MainStyle = {
    parts: ["outer", "content"],
    baseStyle: {
        outer: {
            width: "100%",
        },
        content: {
            width: "100%",
            alignItems: "center",
            margin: "auto",
            minHeight: "450px",
        },
    },
    sizes: {
        xs: {
            outer: {
                minWidth: null,
            },
            content: {
                maxWidth: null,
            },
        },
        lg: {
            outer: {
                minWidth: "container.lg",
            },
            content: {
                maxWidth: "container.lg",
            },
        },
    },
    variants: {
        white: {
            content: {
                backgroundColor: "ce_white",
            },
        },
        lighttan: {
            content: {
                backgroundColor: "ce_backgroundlighttan",
            },
        }
    },
    defaultProps: {
        variant: "white",
        size: "xs",
    },
}

export { MainStyle as default }