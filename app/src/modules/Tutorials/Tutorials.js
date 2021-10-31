import instance from "@Utils/instance";
import { getID } from "@Utils/jwt";

/**
 * A function that sends form data to the server for tutorial creation.
 * Validation is done through attributes on the form's html, but is currently not defined.
 * @param {boolean} isPublished 
 * @param {string} token JWT token.
 */
async function createTutorial(isPublished, token) {
    let isValid = true;
    let author; //TODO: Make backend not need this explicitly set.
    let form = document.getElementById("tutorial_form");

    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
        author = getID(token);
    }

    [
        "tutorial_title",
        "description",
    ].forEach(key => {
        isValid = (form[key].validity.valid) ? isValid : false;
    });

    console.log(isValid);

    if (isValid) {
        try {
            let response = await instance.post("/Tutorials/CreateTutorials", {
                courseId: form["course_id"].value,
                title: form["tutorial_title"].value,
                author: author,
                description: form["description"].value,
                isPublished: isPublished,
                languageId: form["language"].value,
                difficultyId: form["difficulty"].value,
            }, {
                headers: {...headers},
            });

            if (response.statusText == "OK")
            return true;
        } catch (error) {
            console.log(error);
        }
    }

    return false;
}

export { createTutorial }