import { Button } from "@chakra-ui/button";
import { Checkbox } from "@chakra-ui/checkbox";
import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Center, Grid } from "@chakra-ui/layout";
import { passwordRegEx, register } from "@Modules/Auth/Auth";
import Router from 'next/router';
import { useState } from "react";

/**
 * Handles displaying form UI and sending formdata to the server.
 */
function RegistrationForm() {
    const [email, setEmail] = useState("placeholder");

    async function handleSubmit(event) {
        let success = await register(event);
        if (success) {
            let redirect = '/auth/login'; 
            Router.push(redirect);
        }
    }

    return(
        <Center>
            <form onSubmit={handleSubmit}>
                <Grid templateRows="5 1fr" gap={6} w="56">
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
        </Center>
    );
}

export default RegistrationForm;