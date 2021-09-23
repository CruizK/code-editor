import { Button } from "@chakra-ui/button";
import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Center, Grid } from "@chakra-ui/layout";
import { passwordRegEx, validKeys } from "@Modules/Auth/Auth";
import { useState } from "react";

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
    let formData = {};
    
    validKeys.register.forEach(key => {
        isValid = (form[key].validity.valid) ? isValid : false;
        formData[key] = form[key].value;
    });

    if (isValid) {        
        const res = await fetch(process.env.NEXT_PUBLIC_API + '/Auth/Register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(formData),
        });
        return res;  
    }

}

function RegistrationForm() {
    const [email, setEmail] = useState("placeholder");

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
                    <Button variant="white" type="submit">Sign Up</Button>
                </Grid>
            </form>
        </Center>
    );
}

export default RegistrationForm;