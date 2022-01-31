const TagStyle = {
    baseStyle: {
        label: {
            color: "ce_white",
        },
    },
    variants: {
        solid: (props) => {
            //console.log(props);
            return ({
            container: {
                bg: props.type + "." + props.lower,
                paddingLeft: "4",
                paddingRight: "4",
            },
        })
    }
    },
    sizes: {
        sm: {
            container: {
                borderRadius: "13px",
            },
        },
        md: {
            container: {
                borderRadius: "13px",
            },
        },
        lg: {
            container: {
                borderRadius: "13px",
            },
        },       
    },
    defaultProps: {
        variant: "solid",
    },
}

export default TagStyle;