import { Box, Center, Divider, Flex, Grid } from "@chakra-ui/layout";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import paletteToRGB from '@Utils/color';

function Index() {
  return(
    <main>
      <Flex minHeight="350px">
        <Box flex="3" bgImage="/eng.jpg" flexShrink="3" bgBlendMode="multiply" backgroundColor={paletteToRGB("ce_mainmaroon", 0.6)} />
        <Center flex="2" color="ce_white" bgColor="ce_backgroundlighttan">
          <Grid templateRows="5 1fr" gap={6} w="56">
            <SNoLink href="/"><img src="/siucode_logo.png" /></SNoLink>
            <SNoLinkButton type="large" href="/auth/register" backgroundColor="ce_hovermaroon" >SIGN UP</SNoLinkButton>
            <Center>
              <Divider w="75%" borderColor="ce_black" />
            </Center>
            <SNoLinkButton href="/auth/login" backgroundColor="ce_black">SIGN IN</SNoLinkButton>
          </Grid>
        </Center>
      </Flex>
    </main>
  );
}

export default Index;