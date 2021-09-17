import { Button } from "@chakra-ui/button";
import SNoLink from "@Components/SNoLink/SNoLink";

function SNoLinkButton(props) {
    const { variant, children, type, href, ...rest } = props

    let sizeProps = {};
    if(type == "large") {
        sizeProps.height = "50px";
        sizeProps.fontSize = "md";
    } else {
        sizeProps.height = "35px";
    }

    return(
        <SNoLink href={href}>
            <Button variant={variant} {...sizeProps} {...rest}>
                {children}
            </Button>
        </SNoLink>
    )
}

export default SNoLinkButton;