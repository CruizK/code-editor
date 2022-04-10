import { addTagsToTutorials } from "@Modules/Tutorials/Tutorials";
import instance from "@Utils/instance"; 
import { getID } from "@Utils/jwt";
import { defaultSearchParams } from "@Utils/static";
import Identicon from "identicon.js";

/**
 * No *, &, !, @, _, or -
 * Does not start with a number.
 */
function courseRegEx() {
    return "^(?![0-9])[^\*&!@_-]+$";
}

/**
 * For use in FormTooltip
 */
 const courseTitleTooltipLines = [
    'No *, &, !, @, _, or -',
    'Does not start with a number.',
];

/**
 * Converts a course identifier (ID) into an SVG.
 * We could theoretically use an actual crypto/hashing algorithm for this, but we don't need this to be secure. Just long and unique.
 * @param {String} identifier
 * @param {*} options Identicon options
 * @returns {String} base64 encoded SVG 
 */
function courseSvg(identifier, options) {
    // not intended for security, just to get a digest
    var hval = 'siuCode';

    identifier.split('').forEach((charac) => {
        hval ^= charac.charCodeAt(0);
        hval += (hval << 1) + (hval << 4) + (hval << 7) + (hval << 8) + (hval << 24);
    });
    var hash = ("0000000" + (hval >>> 0).toString(16));
    // gotta be greater than 15 characters
    hash = hash.padEnd(17, identifier);

    // create a base64 encoded SVG
    var icon = new Identicon(hash, options).toString();
    return icon;
}

/**
 * A function that gets course data from the server using a course ID.
 * @param {integer} id Course id
 * @returns {Array<Object>|boolean} Course object if successful, 'false' if unsuccessful
 */
async function getCourseDetails(id, token) {
    const headers = {};

    if (token) {
        headers["Authorization"] = "Bearer " + token;
    }

    let courseResponse;

    try {
        courseResponse = await instance.get("/Courses/GetCourseDetails/" + id, {
            headers: {...headers},
        });

        var course = courseResponse.data;
        addTagsToTutorials(course.courseTutorials);
        
        return course;
    } catch (error) {
        console.log(error);
    }
    return false;
}

/**
 * A function that gets all courses a user is registered for.
 * @returns {Array<Object>|boolean} Course objects if successful, 'false' if unsuccessful
 */
 async function getUserCourses(token) {
    const headers = {};

    if (token) {
        headers["Authorization"] = "Bearer " + token;
    }

    let courseResponse;

    try {
        courseResponse = await instance.get("/Courses/GetUserCourses", {
            headers: {...headers},
        });
        
        if (courseResponse.statusText == "OK")
        return courseResponse.data;
    } catch (error) {
        console.log(error);
    }
    return false;
}

/**
 * A function that gets the most popular courses.
 * @returns {Array<Object>|boolean} Course objects if successful, 'false' if unsuccessful
 */
 async function getMostPopularCourses(token) {
    const headers = {};

    if (token) {
        headers["Authorization"] = "Bearer " + token;
    }

    let courseResponse;

    try {
        courseResponse = await instance.get("/Courses/GetMostPopularCourses", {
            headers: {...headers},
        });
        
        if (courseResponse.statusText == "OK")
        return courseResponse.data;
    } catch (error) {
        console.log(error);
    }
    return false;
}

/**
 * A function that gets the most popular courses.
 * @returns {Array<Object>|boolean} Course objects if successful, 'false' if unsuccessful
 */
 async function getAllPublishedCoursesSortByModifyDate(token) {
    const headers = {};

    if (token) {
        headers["Authorization"] = "Bearer " + token;
    }

    let courseResponse;

    try {
        courseResponse = await instance.get("/Courses/GetAllPublishedCoursesSortByModifyDate", {
            headers: {...headers},
        });
        
        if (courseResponse.statusText == "OK")
        return courseResponse.data;
    } catch (error) {
        console.log(error);
    }
    return false;
}

/**
 * A function that gets an array of courses from the server using search parameters.
 * @param {Object} searchParams An object containing any of the following keys: 'searchString, languageId, difficultyId'. Only searchString is mandatory
 * @returns {Array<Object>|boolean} Array of course objects if successful, 'false' if unsuccessful
 */
async function getCoursesFromSearch(searchParams, token) {
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
        let response = await instance.get("/Courses/SearchCourses" + queryString, {
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
 * A function that sends form data to the server for course creation.
 * Validation is done through attributes on the form's html
 * @param {boolean} isPublished 
 * @param {string} token JWT token.
 */
async function createCourse(isPublished, token) {
    let isValid = true;
    let form = document.getElementById("course_form");

    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    [
        "course_title",
        "description",
    ].forEach(key => {
        isValid = (form[key].validity.valid) ? isValid : false;
    });

    if (isValid) {
        try {
            let response = await instance.post("/Courses/CreateCourse", {
                title: form["course_title"].value,
                description: form["description"].value,
                isPublished: isPublished,
            }, {
                headers: {...headers},
            });

            return true;
        } catch (error) {
            
        }
    }

    return false;
}

/**
 * A function that sends form data to the server for course updating.
 * Validation is done through attributes on the form's html
 * Backend should validate that the user is allowed to update the course.
 * @param {boolean} isPublished 
 * @param {string} token JWT token.
 */
async function updateCourse(isPublished, token) {
    let isValid = true;
    let form = document.getElementById("course_form");

    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    [
        "course_id",
        "course_title",
        "description",
    ].forEach(key => {
        isValid = (form[key].validity.valid) ? isValid : false;
    });

    if (isValid) {
        try {
            let response = await instance.put("/Courses/UpdateCourse/" + form["course_id"].value, {
                title: form["course_title"].value,
                description: form["description"].value,
                isPublished: isPublished,
            }, {
                headers: {...headers},
            });

            return true;
        } catch (error) {
            
        }
    }
    
    return false;
}

/**
 * A function that deletes a course.
 * @param {integer} id 
 * @param {string} token JWT token.
 * @returns {boolean} Whether or not the deletion succeeded.
 */
async function deleteCourse(id, token) {
    let isValid = true;

    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    if (isValid) {
        try { 
            let response = await instance.delete("/Courses/DeleteCourse/" + id, {
                headers: {...headers},
            });

            if (response.statusText == "OK")
            return true;
        } catch (error) {
            
        }
    }

    return false;
}

/**
 * A function that registers for a course.
 * @param {integer} id 
 * @param {string} token JWT token.
 * @returns {boolean} Whether or not the registration succeeded
 */
async function registerForCourse(id, token) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    try { 
        let response = await instance.post("/Courses/RegisterUser", {
            courseId: id,
        }, {
            headers: {...headers},
        });

        if (response.statusText == "OK")
        return true;
    } catch (error) {
        
    }

    return false;
}

async function unregisterFromCourse(id, token) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    try { 
        let response = await instance.delete("/Courses/UnregisterUser", {
            headers: {...headers},
        });

        if (response.statusText == "OK")
        return true;
    } catch (error) {
        
    }

    return false;
}

async function checkIfInCourse(id, token) {
    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    var courses = await getUserCourses(token);
    if (!courses) return false;

    let inCourse = courses.some((course, index, ar) => {
        return course.id == id
    });
    
    return inCourse;
}

export { getCourseDetails, getUserCourses, getMostPopularCourses, getAllPublishedCoursesSortByModifyDate, getCoursesFromSearch, createCourse, updateCourse, deleteCourse, courseRegEx, courseTitleTooltipLines, courseSvg, registerForCourse, checkIfInCourse }