import { Button } from "@chakra-ui/button";
import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Center, Grid } from "@chakra-ui/layout";
import { passwordRegEx } from "@Modules/Auth/Auth";
import instance from "@Utils/instance";
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

        let isValid = true;
        let form = event.target;
        
        [
            "email",
            "password",
        ].forEach(key => {
            isValid = (form[key].validity.valid) ? isValid : false;
        });

        if (isValid) {        
            instance.post("/Auth/Login", {
                email: form["email"].value,
                password: form["password"].value,
            })
            .then((response) => {
                if (response.statusText == "OK") {
                    let token = response.data;
                    let hours = 1;
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