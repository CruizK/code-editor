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
 * Cannot be same as email.
 */
function passwordRegEx(email) {
    let baseRegEx = "^(?!placeholder$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,20}$";

    function escapeRegExp(string) {
        return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
    }
    
    let emailRegEx = (typeof email == "string" && email.length > 0) ? escapeRegExp(email) : "placeholder";
    let fullRegEx = baseRegEx.replace("placeholder", emailRegEx);

    return fullRegEx;
};

/**
 * For use in FormTooltip
 */
const passwordTooltipLines = [
    'At least eight characters, but no more than twenty.',
    'At least one lowercase letter.',
    'At least one uppercase letter.',
    'At least one number.',
    'At least one special character: !, @, #, $, %, ^, &, *.',
    'Cannot be same as email.',
];

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
        } catch (error) {
            //TODO: Error handling.
            //console.log(error.response);
        }
    }
    return token;
}

/**
 * A function that sends form data to the server for registration.
 * Validation is done through attributes on the form's html
 * @param event submit event from a form.
 * @return The response from the server.
 */
 async function register(event) {
    event.preventDefault();

    let isValid = true;
    let form = event.target;
    let token;
    
    [
        "name",
        "email",
        "password",
        "admin",
    ].forEach(key => {
        isValid = (form[key].validity.valid) ? isValid : false;
    });

    if (isValid) {      
        try {
            let response = await instance.post("/Auth/Register", {
                name: form["name"].value,
                email: form["email"].value,
                password: form["password"].value,
                admin: form["admin"].checked,
            })

            if (response.statusText == "OK")
            token = response.data;
        } catch (error) {
            //TODO: Error handling.
            //console.log(error.response);
        }
    }

    return token;
}

export { maxAgeInHours, loggedIn, passwordRegEx, passwordTooltipLines, login, register };