import { Button } from "@chakra-ui/button";
import { FormControl, FormErrorMessage } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Grid, Flex, Text } from "@chakra-ui/layout";
import { Select } from "@chakra-ui/select";
import { maxAgeInHours, register, validatePassword } from "@Modules/Auth/Auth";
import { useState } from "react";
import { useCookies } from "react-cookie";

/**
 * Handles displaying form UI and sending formdata to the server.
 */
function RegistrationForm() {
    const [email, setEmail] = useState("placeholder");
    const [password, setPassword] = useState('');
    const [cookies, setCookie] = useCookies(["user"]);
    const [role, setRole] = useState('Student');
    const [passwordErrors, setPaswordErrors] = useState(undefined);

    async function handleSubmit(event) {
        const errors = validatePassword(password);
        event.preventDefault();
        if (errors) {
            setPaswordErrors(errors);
            console.log(errors);
            return;
        }

        let token = await register(event);
        if (token) {
            setCookie("user", token, {
                path: "/",
                maxAge: maxAgeInHours * 60 * 60, //seconds
                sameSite: true,
            })
        }
    }

    const selectWidth = '200px';

    return (
        <form onSubmit={handleSubmit} w="100%">
            <Grid templateRows="5 1fr" gap={6} w="100%">
                <FormControl id="email" isRequired>
                    <Input placeholder="Email" type="email" onChange={(e) => setEmail(e.target.value)} />
                </FormControl>
                <FormControl id="name" isRequired>
                    <Input placeholder="Name" />
                </FormControl>
                <FormControl id="password" isRequired isInvalid={passwordErrors != undefined}>
                    <Input placeholder="Password" type="password" onChange={e => setPassword(e.target.value)} />
                    {passwordErrors ? (
                        <FormErrorMessage>
                            {passwordErrors}
                        </FormErrorMessage>
                    ) : null}
                </FormControl>
                <Flex justify={"space-between"} align="center" my={1}>
                    <Text fontWeight={"bold"} fontSize={"19px"}>Requesting Account Type of: </Text>
                    <Select w="30%" maxW={selectWidth} id="role" defaultValue={"Student"} onChange={(e) => setRole(e.target.value)}>
                        <option id={"Student"} value={"Student"}>Student</option>
                        <option id={"Teacher"} value={"Teacher"}>Teacher</option>
                        <option id={"Admin"} value={"Admin"}>Admin</option>
                    </Select>
                </Flex>
                {role != "Student" ?
                    <FormControl id="accesscode" isRequired>
                        <Input placeholder="Access Code" />
                    </FormControl> : null}
                <Button variant="white" type="submit">Sign Up</Button>
            </Grid>
        </form>
    );
}

export default RegistrationForm;