function loginAs(user) {
    const { email, password } = user;

    // send login request without going through UI
    return cy.request('POST', 'https://localhost:44377/api/Auth/Login', {
        email: email,
        password: password,
    })
    .its('body')
    .then((token) => {
        cy.setCookie('user', token)
    })
}

export { loginAs }