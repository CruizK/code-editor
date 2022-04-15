import { Center } from "@chakra-ui/layout";
import AccessCodeBox from "@Modules/Admin/components/AccessCodeBox/AccessCodeBox";
import Carousel from "@Components/Carousel/Carousel";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import { loggedIn, redirectPayload } from "@Modules/Auth/Auth";
import { getAllPublishedCoursesSortByModifyDate, getMostPopularCourses } from "@Modules/Courses/Courses";
import { getRole } from "@Utils/jwt";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";

export async function getServerSideProps(context) {
  const cookies = context.req.cookies;
  const isLoggedIn = loggedIn(cookies.user);

  if (!isLoggedIn) return redirectPayload;

  let token = cookies.user;
  const userRole = (isLoggedIn) ? getRole(cookies.user) : "None";

  let mostPopular = await getMostPopularCourses(token);

  let recentlyUpdated = await getAllPublishedCoursesSortByModifyDate(token);

  return {
    props: {
      userRole: userRole,
      mostPopular: mostPopular || [],
      recentlyUpdated: recentlyUpdated || [],
    }, // will be passed to the page component as props
  }
}  

function Home(props) {
  const { userRole, mostPopular, recentlyUpdated } = props;

  return(
    <>
      {(userRole == "Admin") &&
        <AccessCodeBox />
      }
      <Main>
        <Center><SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink></Center>
        <SectionHeader title="Most Popular" justifyContent="end">
          <SNoLinkButton href="/search" variant="maroon" maxW="180px" mr={2} mb="20px">
            Search Courses
          </SNoLinkButton>
        </SectionHeader>
        <Carousel items={mostPopular} />

        <SectionHeader title="Recently Updated" />
        <Carousel items={recentlyUpdated} />
      </Main>
    </>
  );
}

export default Home;