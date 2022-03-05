import instance from "@Utils/instance";
import { getID } from "@Utils/jwt";
import { tutorialStatus } from "@Utils/static";

/**
 * A function that gets tutorial details from the server using a tutorial ID.
 * @param {integer} id Tutorial id
 * @returns {Object|boolean} Tutorial objects if successful, 'false' if unsuccessful
 */
async function getUserTutorialDetailsFromId(id, token) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    try {
        let response = await instance.get("/Tutorials/UserTutorialDetails/" + id, {
            headers: {...headers},
        });
        
        return response.data;
    } catch (error) {
        console.log(error);
    }
    return false;
}

/**
 * A function that gets an array of tutorials from the server using a course ID.
 * @param {integer} id Course id
 * @returns {Array<Object>|boolean} Array of tutorial objects if successful, 'false' if unsuccessful
 */
async function getTutorialsFromCourse(id, token) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    try {       
        let response = await instance.get("/Tutorials/CourseTutorials/" + id, {
            headers: {...headers},
        });

        return response.data;
    } catch (error) {
        //TODO: Error handling.
        //console.log(error.response);
    }
    return false;
}

/**
 * A function that gets an array of tutorial details from the server using a course ID.
 * Will probably run into a race condition eventually.
 * @param {integer} id Course id
 * @returns {Array<Object>|boolean} Array of tutorial detail objects if successful, 'false' if unsuccessful
 */
async function getUserTutorialsDetailsFromCourse(id, token) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    try {       
        let response = await instance.get("/Tutorials/GetUserTutorialsDetails/" + id, {
            headers: {...headers},
        });

        return response.data;
    } catch (error) {
        //TODO: Error handling.
        console.log(error.response);
    }
    return false;
}

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
            let response = await instance.post("/Tutorials/CreateTutorial", {
                courseId: form["course_id"].value,
                title: form["tutorial_title"].value,
                author: author,
                description: form["description"].value,
                isPublished: isPublished,
                languageId: form["language"].value,
                difficultyId: form["difficulty"].value,
                prompt: form["md"].value,
                template: form["template"].value,
            }, {
                headers: {...headers},
            });

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
            let response = await instance.put("/Tutorials/UpdateTutorial/" + form["tutorial_id"].value, {
                courseId: form["course_id"].value,
                title: form["tutorial_title"].value,
                author: author,
                description: form["description"].value,
                isPublished: isPublished,
                languageId: form["language"].value,
                difficultyId: form["difficulty"].value,
                prompt: form["md"].value,
                template: form["template"].value,
            }, {
                headers: {...headers},
            });

            return true;
        } catch (error) {
            console.log(error);
        }
    }

    return false;
}

/**
 * A function that sends data to the server for updating a user's progress in a tutorial.
 * Validation should be done server-side.
 * @param {integer} id Course id
 * @param {string} token JWT token.
 * @param {boolean} tutorialStatus Progress through the tutorial
 * @param {boolean} userCode The current code in the editor
 * @returns {boolean} Whether or not the update succeeded.
 */
async function updateUserTutorial(id, token, tutorialStatus, userCode) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    try {
        let response = await instance.put("/Tutorials/UpdateUserTutorial/" + id, {
            tutorialStatus,
            userCode
        }, {
            headers: {...headers},
        });

        return true;
    } catch (error) {
        console.log(error);
    }

    return false;
}

/**
 * Sends code to run to the server, calling updateUserTutorial to set inProgress before trying to run the code.
 * If the code then, afterwards, compiles and runs correctly, isComplete is set instead.
 * Validation should be done server-side.
 * @param {integer} id Tutorial id
 * @param {string} token JWT token.
 * @param {string} language Coding language.
 * @param {string} code Code to compile.
 * @returns {boolean} Whether or not the code compiled succeeded.
 */
async function compileAndRunCode(id, token, language, code) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    let updateSuccess = await updateUserTutorial(id, token, tutorialStatus.InProgress, code);

    if (!updateSuccess) {
        return false;
    }

    try {
        
        let response = await instance.post("/CodeCompiler/Compile/", {
            language: language,
            code: code,
        }, {
            headers: {...headers},
        });

        return response;
    } catch (error) {
        console.log(error);
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
            let response = await instance.delete("/Tutorials/DeleteTutorial/" + id, {
                headers: {...headers},
            });

            return true;
        } catch (error) {
            
        }
    }

    return false;
}

export { getUserTutorialDetailsFromId, getTutorialsFromCourse, getUserTutorialsDetailsFromCourse, createTutorial, updateTutorial, updateUserTutorial, compileAndRunCode, deleteTutorial }