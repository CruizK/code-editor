describe('Authentication', () => {
    beforeEach(() => {
        // reset and seed the database prior to every test
        cy.exec('npm run db:reset')
    })
    
    context('Unauthorized', () => {
        it('Is shown landing page', function() {
            cy.visit('/')
    
            // should see signup link-button
            cy.contains('SIGN UP')

            // should see login link-button
            cy.contains('SIGN IN')
        })

        it('Can login', function() {
            cy.visit('/auth/login')

            cy.fixture('users.json').then((users) => {
                const { email, password } = users.devStudent;

                // type credentials
                cy.get('input[id=email]').type(email)
                cy.get('input[id=password]').type(password, { log: false })

                // submit
                cy.get('button').contains('Sign In').click()

                // should be redirected to home page
                cy.url().should('match', /home/)
            })
        })

        it('Cannot access non-auth pages', function() {
            cy.visit('/home')

            // should be redirected to an auth page
            // probably doesn't matter which one
            cy.url().should('match', /auth.*/)
        })
    })
})
  