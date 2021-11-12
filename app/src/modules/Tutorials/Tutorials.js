import instance from "@Utils/instance";
import { getID } from "@Utils/jwt";

/**
 * A function that sends form data to the server for tutorial creation.
 * Validation is done through attributes on the form's html, but is currently not defined.
 * @param {boolean} isPublished 
 * @param {string} token JWT token.
 */
async function createTutorial(isPublished, token, prompt) {
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
                prompt: form["md"].value,
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

/**
 * A function that sends form data to the server for tutorial updating.
 * Validation is done through attributes on the form's html, but is currently not defined.
 * @param {boolean} isPublished 
 * @param {string} token JWT token.
 */
 async function updateTutorial(isPublished, token) {
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

    if (isValid) {
        try {
            let response = await instance.put("/Tutorials/UpdateTutorials/" + form["tutorial_id"].value, {
                courseId: form["course_id"].value,
                title: form["tutorial_title"].value,
                author: author,
                description: form["description"].value,
                isPublished: isPublished,
                languageId: form["language"].value,
                difficultyId: form["difficulty"].value,
                prompt: form["md"].value,
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

/**
 * A function that deletes a tutorial.
 * @param {integer} id 
 * @param {string} token JWT token.
 * @returns {boolean} Whether or not the deletion succeeded.
 */
 async function deleteTutorial(id, token) {
    let isValid = true;

    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    if (isValid) {
        try { 
            let response = await instance.delete("/Tutorials/DeleteTutorials/" + id, {
                headers: {...headers},
            });

            if (response.statusText == "OK")
            return true;
        } catch (error) {
            
        }
    }

    return false;
}

export { createTutorial, updateTutorial, deleteTutorial }