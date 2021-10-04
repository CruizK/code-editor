import { Box } from "@chakra-ui/layout";
import { Image } from "@chakra-ui/react";

/**
 * Contains the shared header for each page. Only render user icon if logged in.
 */
 function Header(props) {
    return(
        <Box height="50px" bgColor="ce_darkgrey" width="100%">
            <Image src="/siu_logo.png" alt="SIU Logo" maxHeight="50px" />
        </Box>
    )
}

export default Header;