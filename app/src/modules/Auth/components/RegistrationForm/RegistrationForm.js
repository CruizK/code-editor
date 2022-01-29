import { Button } from "@chakra-ui/button";
import { Checkbox } from "@chakra-ui/checkbox";
import { FormControl } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Grid } from "@chakra-ui/layout";
import { maxAgeInHours, passwordRegEx, register } from "@Modules/Auth/Auth";
import { useState } from "react";
import { useCookies } from "react-cookie";

/**
 * Handles displaying form UI and sending formdata to the server.
 */
function RegistrationForm() {
    const [email, setEmail] = useState("placeholder");
    const [cookies, setCookie] = useCookies(["user"]);

    async function handleSubmit(event) {
        let token = await register(event);
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
                    <Input placeholder="Email" type="email" onChange={(e) => setEmail(e.target.value)} />
                </FormControl>
                <FormControl id="name" isRequired>
                    <Input placeholder="Name" />
                </FormControl>
                <FormControl id="password" isRequired>
                    <Input placeholder="Password" type="password" pattern={passwordRegEx(email)} />
                </FormControl>
                <Checkbox id="admin" size="sm">Request admin access</Checkbox>
                <Button variant="white" type="submit">Sign Up</Button>
            </Grid>
        </form>
    );
}

export default RegistrationForm;