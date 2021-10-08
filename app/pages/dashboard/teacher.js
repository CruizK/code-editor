import { Center, Grid } from "@chakra-ui/layout";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import CourseList from "@Modules/Courses/components/CourseList/CourseList";

function Teacher(props) {

    return(
        <Main>
            <Grid templateRows="5 1fr" gap={6} width="100%">
                <Center><SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink></Center>
                <SectionHeader title="MY CREATED CONTENT">
                    <SNoLinkButton href="/tutorials/new" variant="black">
                        New Tutorial +
                    </SNoLinkButton>
                    <SNoLinkButton href="/courses/new" variant="maroon">
                        New Course +
                    </SNoLinkButton>
                </SectionHeader>
                <CourseList />
            </Grid>
        </Main>
    );
  }

export default Teacher;