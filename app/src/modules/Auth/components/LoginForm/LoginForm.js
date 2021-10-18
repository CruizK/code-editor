import { Button } from "@chakra-ui/button";
import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Center, Grid } from "@chakra-ui/layout";
import { login, passwordRegEx } from "@Modules/Auth/Auth";
import { useState } from "react";
import { useCookies } from "react-cookie";

function LoginForm() {
    const [email, setEmail] = useState("placeholder");
    const [cookies, setCookie] = useCookies(["user"]);

    async function handleSubmit(event) {
        let token = await login(event);
        let hours = 1;
        if (token) {
            setCookie("user", token, { 
                path: "/",
                maxAge: hours * 60 * 60, //seconds
                sameSite: true,
            })
        }
    }

    return(
        <Center>
            <form onSubmit={handleSubmit}>
                <Grid templateRows="5 1fr" gap={6} w="56">
                    <FormControl id="email" isRequired>
                        <Input placeholder="Email" onChange={(e) => setEmail(e.target.value)} />
                    </FormControl>
                    <FormControl id="password" isRequired pattern={passwordRegEx(email)}>
                        <Input placeholder="Password" />
                    </FormControl>
                    <Button variant="white" type="submit">Sign In</Button>
                </Grid>
            </form>
        </Center>
    );
}

export default LoginForm;