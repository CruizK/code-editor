import { Center, Divider, Flex, Grid, Heading } from "@chakra-ui/layout";
import Main from "@Components/Main/Main";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import { loggedIn } from "@Modules/Auth/Auth";
import LoginForm from "@Modules/Auth/components/LoginForm/LoginForm";
import Router from 'next/router';
import { useCookies } from "react-cookie";

function Login() {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
  
    if (isLoggedIn) {
      let redirect = '/home'; 
      Router.push(redirect);
      return(
        <Main>
          <Heading as="h2">Redirecting...</Heading>
        </Main>
      );
    }

    return(
        <Main>     
            <Center>
                <Grid templateRows="5 1fr" gap={6} width="100%">
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
        </Main>
    );
}

export default Login;