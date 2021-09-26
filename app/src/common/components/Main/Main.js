import { Container } from "@chakra-ui/layout";
import Header from "@Components/Header/Header"

/**
 * Wrapper for main so that nextjs lets us render the header without complaining.
 */
function Main(props) {
    return(
        <main>
            <Container maxW="container.lg" centerContent minHeight="450px" bgColor="ce_white">
                <Header />
                {props.children}
            </Container>
        </main>
    )
}

export default Main;