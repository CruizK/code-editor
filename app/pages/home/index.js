import Carousel from "@Components/Carousel/Carousel";
import Main from "@Components/Main/Main";

function Home() {
  const carouselItems = [];
  const randomInt = Math.max(Math.random() * 100, 2);
  for (let index = 0; index < randomInt; index++) {
    carouselItems.push(index);
  }

  return(
    <Main>
      <Carousel items={carouselItems} />
    </Main>
  );
}

export default Home;