import SNoLink from "@Components/SnoLink/SnoLink";

function SNoLinkButton(props) {
    let sizeProps = {};

    if(props.type == "large") {
        sizeProps.height = "50px";
        sizeProps.fontSize = "xl";
    } else {
        sizeProps.height = "35px";
    }

    return(
        <SNoLink href={props.href} as="button" backgroundColor={props.backgroundColor} borderRadius="md" {...sizeProps}>{props.children}</SNoLink>
    )
}

export default SNoLinkButton;