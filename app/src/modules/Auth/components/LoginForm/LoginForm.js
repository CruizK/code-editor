import { Button } from "@chakra-ui/button";
import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Center, Grid } from "@chakra-ui/layout";
import { passwordRegEx, validKeys } from "@Modules/Auth/Auth";
import { useState } from "react";
import { useCookies } from "react-cookie";

function LoginForm() {
    const [email, setEmail] = useState("placeholder");
    const [cookie, setCookie] = useCookies(["user"]);

    /**
     * A function that sends form data to the server for login.
     * Validation is done through attributes on the form's html
     * @param event submit event from a form.
     * @return The response from the server.
     */
    async function login(event) {
        event.preventDefault();
        console.log("pog");

        let isValid = true;
        
        let form = event.target;
        let formData = {};
        
        validKeys.login.forEach(key => {
            isValid = (form[key].validity.valid) ? isValid : false;
            formData[key] = form[key].value;
        });

        if (isValid) {        
            await fetch(process.env.NEXT_PUBLIC_API + '/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData),
            }).then(async (response) => {
                if (response.ok) {
                    let data = await response.text();
                    let token = data.slice(1, data.length - 1);
                    let hours = 0.001;
                    setCookie("user", token, { 
                        path: "/",
                        maxAge: hours * 60 * 60, //seconds
                        sameSite: true,
                    })
                }
            });
        }
    }

    return(
        <Center>
            <form onSubmit={login}>
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