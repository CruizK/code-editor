import { Button } from "@chakra-ui/button";
import SNoLink from "@Components/SNoLink/SNoLink";

function SNoLinkButton(props) {
    const { variant, children, size, href, ...rest } = props

    return(
        <SNoLink href={href}>
            <Button variant={variant} size={size} {...rest}>
                {children}
            </Button>
        </SNoLink>
    )
}

export default SNoLinkButton;