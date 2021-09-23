function loggedIn() {
    return false;
}

const validKeys = {
    login: ["email", "password"],
    register: ["email", "name", "password"],
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

    console.log(fullRegEx);
    return fullRegEx;
};

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
    let formData = {};
    
    validKeys.register.forEach(key => {
        isValid = (form[key].validity.valid) ? isValid : false;
        formData[key] = form[key].value;
    });

    if (isValid) {        
        const res = await fetch(process.env.NEXT_PUBLIC_API + '/Auth/Register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(formData),
        });
        return res;  
    }

}

exports.loggedIn = loggedIn;
exports.register = register;
exports.validKeys = validKeys;
exports.passwordRegEx = passwordRegEx;