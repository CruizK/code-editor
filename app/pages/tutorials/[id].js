import { Flex, Container, Button, Spacer } from "@chakra-ui/react";
import { loggedIn } from "@Modules/Auth/Auth";
import instance from "@Utils/instance";
import Editor from "@monaco-editor/react";
import { useEffect, useRef, useState } from "react";
import dynamic from 'next/dynamic'; 
import { ChevronLeftIcon, HamburgerIcon } from "@chakra-ui/icons";
const MarkdownRenderer = dynamic(
  () => import("@Modules/Tutorials/components/MarkdownRenderer/MarkdownRenderer"),
  { ssr: false }
);

export async function getServerSideProps(context) {
  const { id } = context.query;

  var values = {};

  const cookies = context.req.cookies;
  const isLoggedIn = loggedIn(cookies.user);
  const headers = {};

  if (isLoggedIn) {
    let token = cookies.user;
    headers["Authorization"] = "Bearer " + token;
  }

  let tutorialResponse;

  try {
    tutorialResponse = await instance.get("/Tutorials/UserTutorialDetails/" + id, {
      headers: {...headers},
    });
    
    if (tutorialResponse.statusText == "OK")
    values = tutorialResponse.data;
  } catch (error) {
    console.log(error);
  }

  return {
    props: {
      values: values,
    }, // will be passed to the page component as props
  }
}

function Tutorial(props) {
  const { prompt } = props.values;

  const [editorText, setText] = useState(`<button onClick="document.getElementById('demo').innerHTML = \n\t'Change me!'"\n>\n\tClick Me!\n</button>\n<div id="demo"></div>\n`);
  const iframeRef = useRef();

  // For explanation of iframe messaging: https://joyofcode.xyz/avoid-flashing-iframe
  useEffect(() => {
    if (iframeRef && iframeRef.current) {
      const html = { type: 'html', value: editorText };
      iframeRef.current.contentWindow.postMessage(html, '*')
    }
  }, [editorText])

  return(
    <Container maxW="100%" p="0">
      <Flex direction={"column"} height="calc(100vh - 50px)">
        <Flex width="100%" flex="1">
          <Flex flex="1" direction = "column">
            <Flex h="50px" px={3} bg="ce_mainmaroon" color="ce_white" align={"center"}>
              <HamburgerIcon w={8} h={8}/>
              <Spacer />
              <ChevronLeftIcon w={8} h={8}/>
            </Flex>
            <Flex flex="1" p={3} bg="ce_backgroundlighttan" >
              <MarkdownRenderer>
                {prompt}
              </MarkdownRenderer>
            </Flex>
            
          </Flex>
          <Flex flex="2" direction={"column"}>
            <Flex flex="1">
            <Editor
              height="100%"
              width="100%"
              theme="vs-dark"
              defaultLanguage="html"
              options={{
                padding: {
                  top: "10px"
                },
                scrollBeyondLastLine: false,
                wordWrap: "on",
                minimap: {
                  enabled: false
                },
                scrollbar: {
                  vertical: "auto"
                }
              }}
              defaultValue={editorText}
              onChange={(value, event) => { setText(value); setIframeVisible(false) }}
            />
            </Flex>
            <Flex h="50px" bg="ce_blue">

            </Flex>
          </Flex>
          <Flex flex="1" width="100%">
            <iframe 
            ref={iframeRef}
            srcDoc={
              `
              <html style="background-color: #FFF;">
              <script type="module">
                window.addEventListener('message', (event) => {
                  const { type, value } = event.data;

                  if (type === 'html') {
                    document.body.innerHTML = value;
                  }
                })
              </script>
              <body>
               
              </body>
              </html>
              `
            } />
          </Flex>
        </Flex>
        <Flex h="50px" bg="ce_darkgrey" justify={"end"} align="center">
            <Button w="10%" mr={2} variant="yellow">Exit</Button>
        </Flex>
      </Flex>
    </Container>
  );
} 

export default Tutorial;