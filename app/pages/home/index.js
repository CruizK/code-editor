import Carousel from "@Components/Carousel/Carousel";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import { loggedIn } from "@Modules/Auth/Auth";
import instance from "@Utils/instance";

export async function getServerSideProps(context) {
  var data = [];

  const cookies = context.req.cookies;
  const isLoggedIn = loggedIn(cookies.user);
  const headers = {};

  if (isLoggedIn) {
    let token = cookies.user;
    headers["Authorization"] = "Bearer " + token;
  }

  // eventually, we'll need to handle all the course getters separately, but for now we don't have to care about the differentiation
  // ideally we'd shove the request logic into modules/Home/Home.js

  let response = await instance.get("/Courses/GetAllPublishedCourses", {
    headers: {...headers},
  });

  if (response.statusText == "OK")
  data = response.data;

  return {
    props: {
      courses: data,
    }, // will be passed to the page component as props
  }
}  

function Home(props) {
  const { courses } = props;
  const carouselItems = courses || []; // default to empty array

  return(
    <Main>
      <SectionHeader title="ALL COURSES" />
      <Carousel items={carouselItems} />
    </Main>
  );
}

export default Home;