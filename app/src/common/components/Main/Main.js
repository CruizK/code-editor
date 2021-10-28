import { Box, Container } from "@chakra-ui/layout";
import Header from "@Components/Header/Header"

/**
 * Wrapper for main so that nextjs lets us render the header without complaining.
 */
function Main(props) {
    return(
        <main>
            <Box minW={{base: "container.xs", lg: "container.lg" }} bgColor="ce_white" w="100%">                
                <Header />
                <Container maxWidth={{base: "container.xs", lg: "container.lg" }} w="100%" centerContent minHeight="450px">
                    {props.children}
                </Container>
            </Box>
        </main>
    )
}

export default Main;