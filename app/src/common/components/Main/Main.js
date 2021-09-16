import Header from "@Components/Header/Header"

/**
 * Wrapper for main so that nextjs lets us render the header without complaining.
 */
function Main(props) {
    return(
        <main>
            <Header />
            {props.children}
        </main>
    )
}

export default Main;