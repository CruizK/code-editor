import { Center, Container, Grid, Heading } from "@chakra-ui/layout";
import { loggedIn } from "@Modules/Auth/Auth";
import SettingsForm from "@Modules/Auth/components/SettingsForm/SettingsForm";
import withAuthorization from "@Utils/withAuthorization";
import { useCookies } from "react-cookie";

function Settings() {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);

    return(
        <Container w="450px">     
            <Center>
                <Grid templateRows="5 1fr" gap={6} width="100%" borderRadius="md" border="1px solid" borderColor="ce_middlegrey" mt={5} padding={10}>
                    <Heading as="h2" fontWeight="bold">ACCOUNT</Heading>
                    <SettingsForm />

                </Grid>
            </Center>
        </Container>
    );
}

export default withAuthorization(Settings);