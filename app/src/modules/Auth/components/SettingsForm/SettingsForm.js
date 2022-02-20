import { Button } from "@chakra-ui/button";
import { FormControl, FormLabel, FormErrorMessage } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Grid, Flex } from "@chakra-ui/layout";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import { maxAgeInHours, updateUser } from "@Modules/Auth/Auth";
import { useState } from "react";
import { useCookies } from "react-cookie";

function SettingsForm() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [oldPassword, setOldPassword] = useState('');
    const [password, setPassword] = useState('');
    const [passwordError, setPasswordError] = useState(undefined);

    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const oldToken = cookies.user;

    async function handleSubmit(event) {
        event.preventDefault();
        try {
            setPasswordError(undefined);
            let res = await updateUser(event, oldToken);
            const token = res.data;

            if (token) {
                setCookie("user", token, { 
                    path: "/",
                    maxAge: maxAgeInHours * 60 * 60, //seconds
                    sameSite: true,
                })
            }
        } catch(e) {
            if(e.response) {
               const status = e.response.data.statusCode;

               if(status == 400) {
                   setPasswordError("Password Incorrect");
               }
            }
        }
    }

    return(
        <form onSubmit={handleSubmit} w="100%">
            <Grid templateRows="5 1fr" gap={6} w="100%">
                <FormControl id="name">
                    <FormLabel display="flex" alignItems="center">Username</FormLabel>
                    <Input placeholder="Username" onChange={(e) => setUsername(e.target.value)} />
                </FormControl>
                <br />
                <FormControl id="email">
                    <FormLabel display="flex" alignItems="center">Email</FormLabel>
                    <Input placeholder="Email" onChange={(e) => setEmail(e.target.value)} />
                </FormControl>
                <br />
                <FormControl isRequired>
                    <FormLabel display="flex" alignItems="center">Password</FormLabel>
                    To change your password, enter your old password then enter your new password.
                    {passwordError &&
                        <FormErrorMessage>{passwordError}</FormErrorMessage>
                    }
                    <br /><br />
                    Old Password<Input id="oldPassword" placeholder="Password" type="password" onChange={e => setOldPassword(e.target.value)} mb={2}/>
                    New Password<Input id="newPassword" placeholder="Password" type="password" onChange={e => setPassword(e.target.value)}/>
                </FormControl>
                <Flex h="50px" justify={"end"} align="center">
                    <SNoLinkButton href="/" variant="white" w="xs" maxW="80px" mr={2}>Cancel</SNoLinkButton>
                    <Button variant="maroon" type="submit" w="xs" maxW="120px">SAVE</Button>
                </Flex>
            </Grid>
        </form>
    );
}

export default SettingsForm;