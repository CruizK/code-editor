import { Center, Grid } from "@chakra-ui/layout";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";

function Dashboard(props) {

    return(
        <Main>
            <Grid templateRows="5 1fr" gap={6} width="100%">
                <Center><SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink></Center>
                <SectionHeader title="Continue Learning">
                    Stuff
                </SectionHeader>
            </Grid>
        </Main>
    );
  }

export default Dashboard;