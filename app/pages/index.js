import { Box, Center, Divider, Flex, Grid, Heading } from "@chakra-ui/layout";
import Main from "@Components/Main/Main";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import { loggedIn } from "@Modules/Auth/Auth";
import paletteToRGB from '@Utils/color';
import { getRole } from "@Utils/jwt";
import Router from "next/router";
import { useCookies } from "react-cookie";

function Index() {
  const [cookies, setCookie, removeCookie] = useCookies(["user"]);
  const isLoggedIn = loggedIn(cookies.user);
  const userRole = (isLoggedIn) ? getRole(cookies.user) : "None";

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
      <Flex width="100%" minHeight="450px">
        <Box flex="3" bgImage="/siu.png" flexShrink="3" bgBlendMode="multiply" backgroundColor={paletteToRGB("ce_mainmaroon", 0.75)} />
        <Center flex="2" bgColor="ce_backgroundlighttan">
          <Grid templateRows="5 1fr" gap={6} w="56">
            <SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink>
            <SNoLinkButton size="md" href="/auth/register" variant="maroon">SIGN UP</SNoLinkButton>
            <Center>
              <Divider w="75%" borderColor="ce_black" />
            </Center>
            <SNoLinkButton href="/auth/login" variant="black">SIGN IN</SNoLinkButton>
          </Grid>
        </Center>
      </Flex>
    </Main>
  );
}

// Just forcing rebuild actually

export default Index;