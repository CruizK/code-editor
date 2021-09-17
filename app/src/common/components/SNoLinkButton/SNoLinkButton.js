import { Button } from "@chakra-ui/button";
import { useStyleConfig } from "@chakra-ui/system";
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

    const styles = useStyleConfig("SNoLinkButtonStyle", { variant });

    return(
        <SNoLink href={href}>
            <Button __css={styles} {...sizeProps} {...rest}>
                {children}
            </Button>
        </SNoLink>
    )
}

export default SNoLinkButton;