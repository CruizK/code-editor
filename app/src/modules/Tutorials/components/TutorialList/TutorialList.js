import { useCookies } from "react-cookie";
import { loggedIn } from "@Modules/Auth/Auth";
import { useEffect, useState } from "react";
import { getTutorialsFromCourse } from "@Modules/Tutorials/Tutorials";
import EditableTutorialItem from "@Modules/Tutorials/TutorialItem/EditableTutorialItem";
import TutorialItem from "@Modules/Tutorials/TutorialItem/Tutorialitem";

/**
 * Handles displaying an accordion list of courses.
 */
function TutorialList(props) {
    const [tutorials, setTutorials] = useState(props.tutorials || []);
    const { courseId, getTutorials, editable } = props;
    const headers = {};

    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const token = cookies.user;

    /**
     * When tutorials are provided by TutorialList
     */
    useEffect(async function() {
        if (getTutorials) {
            let success = await getTutorialsFromCourse(courseId, token);
            if (success) {
                setTutorials(success);
            }
        }
    }, [getTutorials]);

    /**
     * When tutorials are provided from a page
     */
    useEffect(function() {
        setTutorials(props.tutorials || []);
    }, [props.tutorials]);

    return(
        <>
            {tutorials.map((tutorialData, index) => {
                if (editable) return <EditableTutorialItem key={index} data={tutorialData} token={token} />
                return <TutorialItem key={index} data={tutorialData} token={token} />
            })}            
        </>
    )
}

export default TutorialList;