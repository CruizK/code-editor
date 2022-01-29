import { Box, Center, Divider, Flex, Grid, Heading } from "@chakra-ui/layout";
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
      <Heading as="h2">Redirecting...</Heading>
    );
  }

  return(
    <Box w="100%" h="calc(100vh - 50px)">
      <Flex width="100%" height="100%">
        <Box flex="3" bgImage="/siu.png" bgBlendMode="multiply" backgroundColor={paletteToRGB("ce_mainmaroon", 0.75)} />
        <Box w="100%" maxW="700px" minW="400px" mx="auto" flex="2" bgColor="ce_backgroundlighttan">
          <Grid templateRows="5 1fr" gap={6} w="50%" minW="300px" maxW="400px" mx="auto" my="10vh">
            <SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink>
            <SNoLinkButton size="md" href="/auth/register" variant="maroon">SIGN UP</SNoLinkButton>
            <Center>
              <Divider w="75%" borderColor="ce_black" />
            </Center>
            <SNoLinkButton href="/auth/login" variant="black">SIGN IN</SNoLinkButton>
          </Grid>
        </Box>
      </Flex>
    </Box>
  );
}

// Just forcing rebuild actually

export default Index;