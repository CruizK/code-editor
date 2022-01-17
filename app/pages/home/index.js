import { Center } from "@chakra-ui/layout";
import Carousel from "@Components/Carousel/Carousel";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import { loggedIn } from "@Modules/Auth/Auth";
import { getMostPopularCourses } from "@Modules/Courses/Courses";
import instance from "@Utils/instance";

export async function getServerSideProps(context) {
  var data = [];

  const cookies = context.req.cookies;
  const isLoggedIn = loggedIn(cookies.user);
  let token = cookies.user;

  let popularCourses = await getMostPopularCourses(token);

  if (popularCourses)
  mostPopular = popularCourses;

  return {
    props: {
      mostPopular: [], //mostPopular, TODO: Change once API route is fixed.
    }, // will be passed to the page component as props
  }
}  

function Home(props) {
  const { mostPopular } = props;

  return(
    <Main>
      <Center><SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink></Center>
      <SectionHeader title="Most Popular" />
      <Carousel items={mostPopular} />
    </Main>
  );
}

export default Home;