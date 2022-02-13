import { useRef, useEffect, useState } from "react";
import { Button, Flex, Spinner, Text } from "@chakra-ui/react";
import { ShouldLanguageCompile } from "@Utils/static";


function TutorialCodeOutput({ language, editorText, compiledText }) {
  const iframeRef = useRef();



  // For explanation of iframe messaging: https://joyofcode.xyz/avoid-flashing-iframe
  useEffect(() => {
    if (iframeRef && iframeRef.current) {
      const html = { type: 'html', value: editorText };
      iframeRef.current.contentWindow.postMessage(html, '*')
    }
  }, [editorText])

  if (!ShouldLanguageCompile(language)) {
    return (
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
    )
  }
  else {
    return <Flex flex="1" width="100%" backgroundColor={"ce_black"}>
      <Text color="ce_white" whiteSpace={"pre-wrap"} p={3}>
        {compiledText}
      </Text>
    </Flex>
  }
}

export default TutorialCodeOutput;