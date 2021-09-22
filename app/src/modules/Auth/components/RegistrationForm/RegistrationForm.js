import { Button } from "@chakra-ui/button";
import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Center, Grid } from "@chakra-ui/layout";
import { passwordRegEx, register } from "@Modules/Auth/Auth";

function RegistrationForm() {
    return(
        <Center>
            <form onSubmit={register}>
                <Grid templateRows="5 1fr" gap={6} w="56">
                    <FormControl id="email" isRequired>
                        <Input placeholder="Email" type="email" />
                    </FormControl>
                    <FormControl id="name" isRequired>
                        <Input placeholder="Name" />
                    </FormControl>
                    <FormControl id="password" isRequired>
                        <Input placeholder="Password" type="password" pattern={passwordRegEx} />
                    </FormControl>
                    <Button variant="white" type="submit">Sign Up</Button>
                </Grid>
            </form>
        </Center>
    );
}

export default RegistrationForm;