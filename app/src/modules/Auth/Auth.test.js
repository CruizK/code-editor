import { passwordRegEx } from "./Auth.js";

test('A generic email should pass through passwordRegEx without being mutated beyond usability.', () => {
    expect(passwordRegEx("genericEmail123@live.com")).toBe("^(?!genericEmail123@live\\.com$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,20}$");
    expect(passwordRegEx("generic2Email+1000@gmail.com")).toBe("^(?!generic2Email\\+1000@gmail\\.com$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,20}$");
})

test('Undefined variable should return a placeholder regex.', () => {
    let notDefinedVar;

    expect(passwordRegEx(notDefinedVar)).toBe("^(?!placeholder$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,20}$");
})

test('Empty email should return a placeholder regex.', () => {
    expect(passwordRegEx("")).toBe("^(?!placeholder$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,20}$");
})