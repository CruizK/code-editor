import { Flex, Container, Button } from "@chakra-ui/react";
import { loggedIn } from "@Modules/Auth/Auth";
import instance from "@Utils/instance";
import Editor from "@monaco-editor/react";
import { useEffect, useRef, useState } from "react";
import TutorialSideBar from "@Modules/Tutorials/components/TutorialSideBar/TutorialSideBar";

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

  const [showSidebar, setShow] = useState(true);

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
          <TutorialSideBar prompt={prompt} show={showSidebar} setShow={setShow} />
          <Flex flex="2" maxW="50%" direction={"column"}>
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
            <Flex h="50px" bg="ce_blue" justify={"end"}>
              <Button w="10%" h="100%" variant="blue">RUN</Button>
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
            <Button w="10%" maxW="150px" mr={2} variant="yellow">Exit</Button>
        </Flex>
      </Flex>
    </Container>
  );
} 

export default Tutorial;