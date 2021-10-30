import { Box, Container } from "@chakra-ui/layout";
import { useBreakpointValue } from "@chakra-ui/media-query";
import { useMultiStyleConfig } from "@chakra-ui/system";
import Header from "@Components/Header/Header"

/**
 * Wrapper for main so that nextjs lets us render the header without complaining.
 */
function Main(props) {
    const size = useBreakpointValue({ base: "xs", lg: "lg"});
    const styles = useMultiStyleConfig("Main", {
        size: size,
    });
    return(
        <main>
            <Box __css={styles.outer}>                
                <Header />
                <Box __css={styles.content}>
                    {props.children}
                </Box>
            </Box>
        </main>
    )
}

export default Main;