import { Center, Divider, Flex, Grid, Heading } from "@chakra-ui/layout";
import Main from "@Components/Main/Main";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import LoginForm from "@Modules/Auth/components/LoginForm/LoginForm";

function Login() {
    return(
        <Main>
            <Flex minHeight="350px">        
                <Center flex="2" color="white" bgColor="ce_white">
                    <Grid templateRows="5 1fr" gap={6} w="56">
                        <SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink>
                        <Center><Heading as="h1">Sign In</Heading></Center>
                        <LoginForm />
                        <Center>
                            <Divider w="75%" borderColor="black" />
                        </Center>
                        <SNoLinkButton href="/auth/login" variant="maroon">Sign in with Google</SNoLinkButton>
                        <SNoLinkButton href="/auth/register" variant="black">Sign in with GitHub</SNoLinkButton>
                    </Grid>
                </Center>
            </Flex>
        </Main>
    );
  }

export default Login;