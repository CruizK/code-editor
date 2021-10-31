import { Button } from "@chakra-ui/button";
import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Center, Grid } from "@chakra-ui/layout";
import FormToolTip from "@Components/FormTooltip/FormToolTip";
import { login, maxAgeInHours, passwordRegEx, passwordTooltipLines } from "@Modules/Auth/Auth";
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
        <Center>
            <form onSubmit={handleSubmit}>
                <Grid templateRows="5 1fr" gap={6} w="56">
                    <FormControl id="email" isRequired>
                        <Input placeholder="Email" onChange={(e) => setEmail(e.target.value)} />
                    </FormControl>
                    <FormControl id="password" isRequired>
                        <FormLabel display="flex" alignItems="center">
                            <Input placeholder="Password" type="password" pattern={passwordRegEx(email)} />
                            <FormToolTip lines={passwordTooltipLines}/>
                        </FormLabel>
                    </FormControl>
                    <Button variant="white" type="submit">Sign In</Button>
                </Grid>
            </form>
        </Center>
    );
}

export default LoginForm;