import { Button } from "@chakra-ui/button";
import { FormControl } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Grid } from "@chakra-ui/layout";
import { login, maxAgeInHours, passwordRegEx } from "@Modules/Auth/Auth";
import { useState } from "react";
import { useCookies } from "react-cookie";

function LoginForm() {
    const [email, setEmail] = useState("placeholder");
    const [cookies, setCookie] = useCookies(["user"]);

    async function handleSubmit(event) {
        let token = await login(event);
        if (token) {
            setCookie("user", token, { 
                path: "/",
                maxAge: maxAgeInHours * 60 * 60, //seconds
                sameSite: true,
            })
        }
    }

    return(
        <form onSubmit={handleSubmit} w="100%">
            <Grid templateRows="5 1fr" gap={6} w="100%">
                <FormControl id="email" isRequired>
                    <Input placeholder="Email" onChange={(e) => setEmail(e.target.value)} />
                </FormControl>
                <FormControl id="password" isRequired>
                    <Input placeholder="Password" type="password" pattern={passwordRegEx(email)} />
                </FormControl>
                <Button variant="white" type="submit">Sign In</Button>
            </Grid>
        </form>
    );
}

export default LoginForm;