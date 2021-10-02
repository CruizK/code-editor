import { Button } from "@chakra-ui/button";
import { Checkbox } from "@chakra-ui/checkbox";
import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Center, Grid } from "@chakra-ui/layout";
import { passwordRegEx } from "@Modules/Auth/Auth";
import instance from "@Utils/instance";
import { useState } from "react";

/**
 * Handles displaying form UI and sending formdata to the server.
 */
function RegistrationForm() {
    const [email, setEmail] = useState("placeholder");

    /**
     * A function that sends form data to the server for registration.
     * Validation is done through attributes on the form's html
     * @param event submit event from a form.
     * @return The response from the server.
     */
    async function register(event) {
        event.preventDefault();

        let isValid = true;
        let form = event.target;
        
        [
            "name",
            "email",
            "password",
            "admin",
        ].forEach(key => {
            isValid = (form[key].validity.valid) ? isValid : false;
        });

        if (isValid) {        
            instance.post("/Auth/Register", {
                name: form["name"].value,
                email: form["email"].value,
                password: form["password"].value,
                admin: form["admin"].checked,
            })
            .then((response) => {
                if (response.statusText == "OK") {
                    // DO SOMETHING
                }
            });
        }
    }

    return(
        <Center>
            <form onSubmit={register}>
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