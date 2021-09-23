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

    return fullRegEx;
};

exports.loggedIn = loggedIn;
exports.validKeys = validKeys;
exports.passwordRegEx = passwordRegEx;