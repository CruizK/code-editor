import instance from "@Utils/instance"; 
import { getID } from "@Utils/jwt";

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
        let now = new Date();    
        instance.post("/Courses/CreateCourse", {
            title: form["course_title"].value,
            author: getID(token), //TODO: Make backend generate this.
            description: form["description"].value,
            createDate: now.toISOString(), //TODO: Make backend generate this.
            modifyDate: now.toISOString(), //TODO: Make backend generate this.
            isPublished: isPublished,
        }, {
            headers: {...headers},
        })
        .then((response) => {
            if (response.statusText == "OK") {
                // DO SOMETHING
            }
        });
    }
}

export { createCourse }