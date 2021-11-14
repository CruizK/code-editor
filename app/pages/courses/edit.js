import { Image } from "@chakra-ui/react";
import Main from "@Components/Main/Main";
import SectionHeader from "@Components/SectionHeader/SectionHeader";
import SNoLink from "@Components/SNoLink/SNoLink";
import SNoLinkButton from "@Components/SNoLinkButton/SNoLinkButton";
import dynamic from 'next/dynamic'; 
const CourseForm = dynamic(
    () => import('@Modules/Courses/components/CourseForm/CourseForm').then(mod => mod.default),
    { ssr: false }
);
import { Center, Flex, Grid } from "@chakra-ui/layout";
import { Button } from "@chakra-ui/button";
import { updateCourse } from "@Modules/Courses/Courses";
import { useCookies } from "react-cookie";
import { loggedIn } from "@Modules/Auth/Auth";
import Router from 'next/router';
import { getRole } from "@Utils/jwt";
import Barrier from "@Components/Barrier/Barrier";
import { useState } from "react";

function EditCourse() {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const token = cookies.user;

    const [presetPublishValue, setPreset] = useState(null); // this will be called in CourseForm

    async function handleSubmit(isPublished, token) {
        let success = await updateCourse(isPublished, token);
        if (success) {
            const userRole = (isLoggedIn) ? getRole(cookies.user) : "None";
            let redirect = '/dashboard/' + ((userRole == "Student") ? '' : (userRole.toLowerCase())); 
            Router.push(redirect);
        }
    }

    var draftButton, publishButton;

    if (presetPublishValue != null && presetPublishValue) {
        draftButton =
        <Barrier 
            buttonText={<Button variant="black">Save As Draft</Button>}
            title="Confirmation"
            text="Doing this will hide your course from public view, are you sure you want this?"
            callback={() => handleSubmit(false, token)}
        />;
    }
    if (presetPublishValue != null && !presetPublishValue) {
        publishButton =
        <Barrier 
            buttonText={<Button variant="maroon">Publish</Button>}
            title="Confirmation"
            text="Doing this will display your course to the public, are you sure you want this?"
            callback={() => handleSubmit(true, token)}
        />;
    }

    return(
        <Main>
            <Grid templateRows="5 1fr" gap={6} width="100%">
                <Center><SNoLink href="/"><Image src="/siucode_logo.png" alt="SIU Logo" maxHeight="100px" /></SNoLink></Center>
                <SectionHeader title="EDIT COURSE">
                <SNoLinkButton href="/dashboard/teacher" variant="white">
                    Cancel
                </SNoLinkButton>

                {draftButton ||
                    <Button variant="black" onClick={() => handleSubmit(false, token)}>
                        Save As Draft
                    </Button>
                }
                {publishButton ||
                    <Button variant="maroon" onClick={() => handleSubmit(true, token)}>
                        Publish
                    </Button>
                }
                </SectionHeader>
                <CourseForm getDefaults={true} setPreset={setPreset} />
            </Grid>
        </Main>
    );
  }

export default EditCourse;