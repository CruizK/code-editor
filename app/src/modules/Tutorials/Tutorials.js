import instance from "@Utils/instance";
import { getID } from "@Utils/jwt";
import { defaultSearchParams, difficultylevels, programmingLanguages, tutorialStatus } from "@Utils/static";

/**
 * A function that gets the lats tutorial a user was working on
 * @param {*} courseId
 * @param {*} token 
 */
async function getLastTutorial(courseId, token) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    try {
        
        let response = await instance.get("/Tutorials/GetUserLastInProgressTutorial/" + courseId, {
            headers: {...headers},
        });

        return response;
    } catch (error) {
        console.log(error);
    }

    return false;
}

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

        const tutorials = response.data;
        addTagsToTutorials(tutorials);

        return tutorials;
    } catch (error) {
        //TODO: Error handling.
        //console.log(error.response);
    }
    return false;
}

/**
 * A function that gets an array of tutorials from the server using a course ID.
 * @param {integer} id Course id
 * @param {Object} searchParams An object containing any of the following keys: 'searchString, languageId, difficultyId'. Only searchString is mandatory
 * @returns {Array<Object>|boolean} Array of tutorial objects if successful, 'false' if unsuccessful
 */
 async function getTutorialsFromCourseSearch(id, searchParams, token) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    var queryParmas = {
        searchString: searchParams.searchString,
        languageId: searchParams.languageId || defaultSearchParams.languageId,
        difficultyId: searchParams.difficultyId || defaultSearchParams.difficultyId,
    }

    var queryPieces = [];
    for (const [key, value] of Object.entries(queryParmas)) {
        queryPieces.push(`${key}=${value}`);
    }

    const queryString = `?${queryPieces.join('&')}`;

    try {       
        let response = await instance.get("/Tutorials/SearchTutorials/" + id + queryString, {
            headers: {...headers},
        });

        const tutorials = response.data;
        addTagsToTutorials(tutorials);

        return tutorials;
    } catch (error) {
        console.error(error);
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
                solution: form["solution"].value,
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
                solution: form["solution"].value,
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

const maxDistCoefficient = 0.05;

/**
 * Levenshtein distance calculator
 * To transform a string, we can either delete, insert, or replace a single character as a single operation
 * We're calculating how many of those operations we need to get to userSolution
 * @param {String} solution 
 * @param {String} userSolution
 */
function levenshtein(solution, userSolution) {
    const columns = userSolution.length;
    const rows = solution.length;
    
    // Multi-dimensional array of (userSolution.length + 1) columns and (solution.length + 1) rows. The plus one is because we want the bottom-right-most value
    var scoreMatrix = Array(columns + 1).fill(0);
    scoreMatrix = scoreMatrix.map(() => {
        return Array(rows + 1).fill(null)
    });

    // first column
    for (let rowIndex = 0; rowIndex <= rows; rowIndex += 1) {
        scoreMatrix[0][rowIndex] = rowIndex;
    }

    // first row
    for (let colIndex = 0; colIndex <= columns; colIndex += 1) {
        scoreMatrix[colIndex][0] = colIndex;
    }

    // we're starting at colIndex = 1 and rowIndex = 1, so we don't need to check for out-of-array errors
    for (let colIndex = 1; colIndex <= columns; colIndex += 1) {
        for (let rowIndex = 1; rowIndex <= rows; rowIndex += 1) {
            const sub = (solution[rowIndex - 1] === userSolution[colIndex - 1]) ? 0 : 1;

            const scoreAt = Math.min(
                scoreMatrix[colIndex][rowIndex - 1] + 1, // deletion
                scoreMatrix[colIndex - 1][rowIndex] + 1, // insertion
                scoreMatrix[colIndex - 1][rowIndex - 1] + sub, // substitution
            );

            scoreMatrix[colIndex][rowIndex] = scoreAt;
        }
    }

    const passDistance = Math.max(1, maxDistCoefficient * rows);
    const distance = scoreMatrix[columns][rows];
    // userSolution can at most 5% different from solution
    // if we want stricter/looser comparisons, just change the 0.05
    const passes = distance <= passDistance; 
    return {
        passDistance: passDistance,
        distance: distance,
        passes: passes
    };
}

/**
 * A function that mutates the tutorials in the given array, adding difficulty / langauge tag data as necessary
 * @param {Array} tutorials An array of tutorials 
 */
function addTagsToTutorials(tutorials) {
    tutorials.forEach(tute => {
        tute.difficulty = {
            difficulty: difficultylevels.find((dl) => {
                return dl.dbIndex == tute.difficultyId;
            }).value
        };
        
        tute.language = {
            language: programmingLanguages.find((pl) => {
                return pl.dbIndex == tute.languageId;
            }).value
        };
    });
}

export { getLastTutorial, getUserTutorialDetailsFromId, getTutorialsFromCourse, getTutorialsFromCourseSearch, getUserTutorialsDetailsFromCourse, createTutorial, updateTutorial, updateUserTutorial, compileAndRunCode, deleteTutorial, levenshtein, addTagsToTutorials }