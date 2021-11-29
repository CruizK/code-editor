import { Grid } from "@chakra-ui/layout";
import Main from "@Components/Main/Main";
import { loggedIn } from "@Modules/Auth/Auth";
import instance from "@Utils/instance";
import Editor from "@monaco-editor/react";
import { GridItem } from "@chakra-ui/react";
import { useState } from "react";
import dynamic from 'next/dynamic'; 
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
  const { values } = props;
  const { prompt } = values;

  const [editorText, setText] = useState("<button onClick=\"document.getElementById('demo').innerHTML = \n\t'Change me!'\"\n>\n\tClick Me!\n</button>\n");

  return(
    <Main width="100%" margin="0" maxWidth="100%">
      <Grid templateColumns="repeat(3, 33%)" width="100%" height="450px">
        <GridItem>
          <MarkdownRenderer>
            {prompt}
          </MarkdownRenderer>
        </GridItem>
        <GridItem>
          <Editor
            height="100%"
            width="100%"
            defaultLanguage="javascript"
            defaultValue={editorText}
            onChange={(value, event) => { setText(value) }}
          />
        </GridItem>
        <GridItem width="100%">
          <iframe srcDoc={
            "<html><body>" + editorText + "<br /><div id=\"demo\">Hi!</div></body></html>"
          } />
        </GridItem>
      </Grid>
    </Main>
  );
} 

export default Tutorial;