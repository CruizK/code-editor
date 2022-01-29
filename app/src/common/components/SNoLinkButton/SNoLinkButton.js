import { Button } from "@chakra-ui/button";
import NextLink from 'next/link';

function SNoLinkButton(props) {
    const { variant, children, size, href, ...rest } = props

    return(
        <NextLink href={href} passHref>
            <Button variant={variant} size={size} {...rest}>
                {children}
            </Button>
        </NextLink>
    )
}

export default SNoLinkButton;