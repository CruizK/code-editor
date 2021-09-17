import { Button } from "@chakra-ui/button";
import { FormControl, FormLabel } from "@chakra-ui/form-control";
import { Input } from "@chakra-ui/input";
import { Center, Grid } from "@chakra-ui/layout";

function RegistrationForm() {

    return(
        <Center>
            <Grid templateRows="5 1fr" gap={6} w="56">
                <FormControl id="email" isRequired>
                    <Input placeholder="Email" />
                </FormControl>
                <FormControl id="password" isRequired>
                    <Input placeholder="Password" />
                </FormControl>
                <Button variant="white">Sign Up</Button>
            </Grid>
        </Center>
    );
}

export default RegistrationForm;