import { useState, useEffect } from "react";
import Router from "next/router";

/**
 * Stores course data in sessionStorage, then reroutes the user.
 * @param {string} title The title of an existing course.
 * @param {string} description The description of an existing course.
 */
function storeThenRouteCourse(id, title, description) {
    sessionStorage.setItem('courseDefaults', JSON.stringify({
        id: id,
        title: title,
        description: description,
    }));
    let redirect = '/courses/edit'
    Router.push(redirect);
}

/**
 * 
 * @returns The course a user is currently editing. If a user switches tabs, this will be empty. 
 */
const useCourseSession = () => {
    const [value, setValue] = useState({})

    useEffect(function() {
        let tempValue = sessionStorage.getItem('courseDefaults');
        if (tempValue) {
            setValue(JSON.parse(tempValue));
        }
    }, [])

    return value;
}

export { useCourseSession, storeThenRouteCourse };