import { Center } from "@chakra-ui/layout";
import Carousel from "@Components/Carousel/Carousel";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import { loggedIn, redirectPayload } from "@Modules/Auth/Auth";
import { getUserCourses } from "@Modules/Courses/Courses";

export async function getServerSideProps(context) {
  const cookies = context.req.cookies;
  const isLoggedIn = loggedIn(cookies.user);

  if (!isLoggedIn) return redirectPayload;

  let token = cookies.user;

  let myCourses = await getUserCourses(token);

  return {
    props: {
        myCourses: myCourses || [],
    }, // will be passed to the page component as props
  }
}  

function Mine(props) {
  const { myCourses } = props;

  return(
    <Main>
      <Center><SNoLink href="/"><img maxWidth="90%" src="/siucode_logo.png" /></SNoLink></Center>
      <SectionHeader title="Continue Learning" />
      <Carousel items={myCourses} />
    </Main>
  );
}

export default Mine;