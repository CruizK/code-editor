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
async function login(event) {
    event.preventDefault();

    let isValid = true;
    let form = event.target;
    let token;
    
    [
        "email",
        "password",
    ].forEach(key => {
        isValid = (form[key].validity.valid) ? isValid : false;
    });

    if (isValid) { 
        try {       
            let response = await instance.post("/Auth/Login", {
                email: form["email"].value,
                password: form["password"].value,
            });

            token = response.data;
            return token;
        } catch (error) {
            //TODO: Error handling.
            //console.log(error.response);
        }
    }
}

/**
 * A function that sends form data to the server for registration.
 * Validation is done through attributes on the form's html
 * @param event submit event from a form.
 * @param needsAccessCode
 * @return The response from the server.
 */
 async function register(event, needsAccessCode) {
    event.preventDefault();

    let isValid = true;
    let form = event.target;
    let token;
    console.log("HELLO??");
    [
        "name",
        "email",
        "password",
        "accesscode",
        "role"
    ].forEach(key => {
        console.log(key, needsAccessCode);
        if(key == "accesscode" && !needsAccessCode)
            return
        isValid = (form[key].validity.valid) ? isValid : false;
    });

    if (isValid) {      
        try {
            let data = {
                name: form["name"].value,
                email: form["email"].value,
                password: form["password"].value,
                role: form["role"].value, 
            };

            if(data.role != "Student") data.accesscode = form["accesscode"].value;

            let response = await instance.post("/Auth/Register", data);

            token = response.data;
        } catch (error) {
            //TODO: Error handling.
            //console.log(error.response);
        }
    }

    return token;
}

export { maxAgeInHours, loggedIn, validatePassword, login, register };