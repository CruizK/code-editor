import { ChakraProvider } from "@chakra-ui/react"
import Header from "@Components/Header/Header";
import theme from '@Utils/theme'

function MyApp({ Component, pageProps }) {
  return (
    <ChakraProvider theme={theme}>
      <Header />
      <Component {...pageProps} />
    </ChakraProvider>
  )
}

export default MyApp