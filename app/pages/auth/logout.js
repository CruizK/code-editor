import { Heading } from "@chakra-ui/layout";
import Main from "@Components/Main/Main";
import { loggedIn } from "@Modules/Auth/Auth";
import Router from "next/router";
import { useCookies } from "react-cookie";

function Logout() {
    let isLoaded = false;
    const [cookies, setCookie, removeCookie] = useCookies([]);
    const isLoggedIn = loggedIn(cookies.user);
  
    if (isLoggedIn) { 
      removeCookie("user", {
        path: "/",
      });
      isLoaded = true;
    }

    if (isLoaded) {
        Router.push('/');
    }

    return(
        <Main>
          <Heading as="h2">Logging out...</Heading>
        </Main>
    )
}

export default Logout;