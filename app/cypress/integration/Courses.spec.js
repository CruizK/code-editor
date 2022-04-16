describe('Courses', () => {
    beforeEach(() => {
        // reset and seed the database prior to every test
        cy.exec('npm run db:reset')
    })
    
    context('Unregistered Student', () => {
        beforeEach(function fetchUser () {
            cy.fixture('users.json').then((users) => {
                const { email, password } = users.genericStudents[0];

                // send login request without going through UI
                cy.request('POST', 'https://localhost:44377/api/Auth/Login', {
                    email: email,
                    password: password,
                })
                .its('body')
                .then((token) => {
                    cy.setCookie('user', token)
                })
            })
        })
        
        it('Can start from beginning', function() {
            cy.visit('/courses/1') // C# Basics

            // get start button
            cy.contains('Start from Beginning').click()

            // get redirected
            cy.url().should('match', /tutorials.*/)
        })

        it('Can Start from any tutorial', function() {
            cy.visit('/courses/1') // C# Basics

            // get start button
            cy.get('.start').first().click()

            // get redirected
            cy.url().should('match', /tutorials.*/)
        })

        it('Can Start from any tutorial after being registered', function() {
            cy.visit('/courses/1') // C# Basics

            // get start button
            cy.get('.start').first().click()

            // get redirected
            cy.url().should('match', /tutorials.*/)

            cy.visit('/courses/1')

            // get start button
            cy.get('.start').first().click()

            // get redirected
            cy.url().should('match', /tutorials.*/)
        })
    })
    
    context('Registered Student', () => {
        beforeEach(function fetchUser () {
            cy.fixture('users.json').then((users) => {
                const { email, password } = users.devStudent;

                // send login request without going through UI
                cy.request('POST', 'https://localhost:44377/api/Auth/Login', {
                    email: email,
                    password: password,
                })
                .its('body')
                .then((token) => {
                    cy.setCookie('user', token)
                })
            })
        })
        
        it('Can start from beginning', function() {
            cy.visit('/courses/1') // C# Basics

            // get start button
            cy.contains('Start from Beginning').click()

            // get redirected
            cy.url().should('match', /tutorials.*/)
        })

        it('Can Start from any tutorial', function() {
            cy.visit('/courses/1') // C# Basics

            // get start button
            cy.get('.start').first().click()

            // get redirected
            cy.url().should('match', /tutorials.*/)
        })
    })

    context('Teacher', () => {
        // tests
    })
})
  