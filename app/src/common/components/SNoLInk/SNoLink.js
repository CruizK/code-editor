import { Link } from '@chakra-ui/layout';
import NextLink from 'next/link';
/**
 * (S)tyled (Ne)NextJS (Link). NextJS routing wrapper for Chakra UI Link. Enables NextJS pre-fetching and Chakra UI style props.
 */
function SNoLink(props) {
    const { children, href, ...rest } = props;

    return(
        <NextLink href={href} passHref>
            <Link {...rest} textDecoration={"none"}>{children}</Link>
        </NextLink>
    )
}

export default SNoLink;