import { Box, Center, Divider, Flex, Grid } from "@chakra-ui/layout";
import Main from "@Components/Main/Main";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import paletteToRGB from '@Utils/color';

function Index() {
  return(
    <Main>
      <Flex width="100%">
        <Box flex="3" bgImage="/eng.jpg" flexShrink="3" bgBlendMode="multiply" backgroundColor={paletteToRGB("ce_mainmaroon", 0.6)} />
        <Center flex="2" bgColor="ce_backgroundlighttan">
          <Grid templateRows="5 1fr" gap={6} w="56">
            <SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink>
            <SNoLinkButton size="md" href="/auth/register" variant="maroon">SIGN UP</SNoLinkButton>
            <Center>
              <Divider w="75%" borderColor="ce_black" />
            </Center>
            <SNoLinkButton href="/auth/login" variant="black">SIGN IN</SNoLinkButton>
          </Grid>
        </Center>
      </Flex>
    </Main>
  );
}

export default Index;