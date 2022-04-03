import instance from "@Utils/instance";

/**
 * A function that generates an acess code.
 * @param {boolean} roleID 
 * @param {string} token JWT token.
 */
async function generateAccessCode(roleID, token) {

    const headers = {};

    if (typeof token != 'undefined') {
        headers["Authorization"] = "Bearer " + token;
    }

    try {
        let response = await instance.post("/Auth/GenerateAccessCode", {
            role: parseInt(roleID)
        }, {
            headers: {...headers},
        });

        return response.data;
    } catch (error) {
        console.log(error);
    }

    return false;
}

export { generateAccessCode }