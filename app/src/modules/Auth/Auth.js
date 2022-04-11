import instance from "@Utils/instance";

/**
 * Max age of JWT tokens in hours.
 */
const maxAgeInHours = 1;

/**
 * Checks if user cookie is set
 */
function loggedIn(userCookie) {
    if (userCookie) return true;
    return false;
}

const redirectURL = '/auth/login';

const redirectPayload = {
    redirect: {
        permanent: false,
        destination: redirectURL
    }
}

/**
 * At least eight characters, but no more than twenty.
 * At least one lowercase letter.
 * At least one uppercase letter.
 * At least one number.
 * At least one special character: !, @, #, $, %, ^, &, *
 */
function validatePassword(password) {
    let length = password.length >= 8 && password.length <= 20;
    let oneLowerCase = /[a-z]+/g
    let oneUpperCase = /[A-Z]+/g
    let oneNumber = /[0-9]+/g
    let oneSpecial = /[!@#\$%\^&\*]+/g

    if (!length) return 'Password must be between 8-20 characters';
    if (!oneLowerCase.test(password)) return 'Password must contain one lowercase letter';
    if (!oneUpperCase.test(password)) return 'Password must contain one uppercase letter';
    if (!oneNumber.test(password)) return 'Password must contain at least 1 number';
    if (!oneSpecial.test(password)) return 'Password must contain at least 1 special character: !, @, #, $, %, ^, &, *';
    return undefined;
};

/**
 * A function that sends form data to the server for login.
 * Validation is done through attributes on the form's html
 * @param event submit event from a form.
 * @returns JWT token
 */
function login(event) {
    event.preventDefault();

    let form = event.target;
   
    return instance.post("/Auth/Login", {
        email: form["email"].value,
        password: form["password"].value,
    });
}

/**
 * A function that sends form data to the server for registration.
 * Validation is done through attributes on the form's html
 * @param event submit event from a form.
 * @param needsAccessCode
 * @return The response from the server.
 */
function register(event, needsAccessCode) {
    event.preventDefault();

    let form = event.target;
 
    let data = {
        name: form["name"].value,
        email: form["email"].value,
        password: form["password"].value,
        role: form["role"].value, 
    };

    if(needsAccessCode) data.accesscode = form["accesscode"].value;

    return instance.post("/Auth/Register", data);
}

function verify(verifyToken) {

    const data = {
        verifyToken
    };

    return instance.post("/Auth/VerifyAccount", data);
}

/**
 * A function that sends form data to the server for user-settings updating
 * Validation is done through attributes on the form's html
 * Backend should validate that the user is allowed to update via comparing password digests.
 * @param event Submit event from a form 
 * @param {string} token JWT token.
 * @returns The response from the server.
 */
function updateUser(event, token) {
    event.preventDefault();

    let form = event.target;

    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    let data = {
        name: form["name"].value,
        email: form["email"].value,
        oldPassword: form["oldPassword"].value,
        newPassword: form["newPassword"].value,
    };
    
    return instance.put("/Auth/UpdateUser", data, {
        headers: {...headers},
    });
}

export { maxAgeInHours, loggedIn, validatePassword, login, register, updateUser, verify, redirectURL, redirectPayload };
