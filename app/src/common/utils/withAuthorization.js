import { loggedIn, redirectURL } from "@Modules/Auth/Auth";
import { useCookies } from "react-cookie";
import React, { useState, useEffect } from 'react';
import { useRouter } from "next/router";

function withAuthorization(Page) {
  return () => {
    const router = useRouter();
    const [verifyLoggedIn, setVerifyLoggedIn] = useState(false);
    const [cookies] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);

    useEffect(() => {
        if (!isLoggedIn) router.replace(redirectURL)
        else setVerifyLoggedIn(true);
    }, [])
    
    if (!verifyLoggedIn) return <></>
    else return <Page />
  }
}

export default withAuthorization;