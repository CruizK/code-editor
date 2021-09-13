import { Box } from "@chakra-ui/layout";

/**
 * Contains the shared header for each page. Only render user icon if logged in.
 */
 function Header(props) {
    return(
        <Box height="50px" bgColor="ce_darkgrey">
            {props.children}
        </Box>
    )
}

export default Header;