import { Button } from "@chakra-ui/button";
import { FormControl, FormErrorMessage } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Flex, Grid, Text } from "@chakra-ui/layout";
import { useDisclosure } from "@chakra-ui/react";
import { Select } from "@chakra-ui/select";
import { register, validatePassword } from "@Modules/Auth/Auth";
import { useState } from "react";
import EmailSentModal from "../EmailSentModal/EmailSentModal";

/**
 * Handles displaying form UI and sending formdata to the server.
 */
function RegistrationForm() {
    const [email, setEmail] = useState("placeholder");
    const [password, setPassword] = useState('');
    const [role, setRole] = useState('Student');
    const [emailError, setEmailError] = useState(undefined);
    const [passwordErrors, setPaswordErrors] = useState(undefined);
    const { isOpen, onClose, onOpen } = useDisclosure();


    const showAccessCode = role != "Student";

    async function handleSubmit(event) {
        event.preventDefault();
        const errors = validatePassword(password);
        setPaswordErrors(errors);
        if (errors) {
            return;
        }

        try {
            setEmailError(undefined);
            let res = await register(event, showAccessCode);
            if (res.data == "OK")
                onOpen()

        } catch (e) {
            console.dir(e);
            if (e.response && e.response.status != 500) {
                const msg = e.response.data.message;
                console.log(msg.includes('email'));
                if (msg.includes('email') || msg.includes('user')) {
                    setEmailError(msg);
                }
            }
        }
    }

    const selectWidth = '200px';

    return (
        <>
            <form onSubmit={handleSubmit} w="100%">
                <Grid templateRows="5 1fr" gap={6} w="100%">
                    <FormControl id="email" isRequired isInvalid={emailError}>
                        <Input placeholder="Email" type="email" onChange={(e) => setEmail(e.target.value)} />
                        {emailError ? (
                            <FormErrorMessage>
                                {emailError}
                            </FormErrorMessage>
                        ) : null}
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
                    {showAccessCode ?
                        <FormControl id="accesscode">
                            <Input placeholder="Access Code" />
                        </FormControl>
                        : null}
                    <Button variant="white" type="submit">Sign Up</Button>
                </Grid>
            </form>
            <EmailSentModal email={email} isOpen={isOpen} onClose={onClose}/>
        </>
    );
}

export default RegistrationForm;