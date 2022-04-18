import { loginAs } from '../support/utils'
import users from '../fixtures/auth.json'

describe('Courses', () => {
    before(() => {
        cy.fixture('courses').as('courses')
    })

    beforeEach(() => {
        // reset and seed the database prior to every test
        cy.exec('npm run db:reset')
    })

    let userToken
    
    context('Unregistered Student', () => {
        beforeEach(function fetchUser () {
            loginAs(users.genericStudents[0]).then((cookie) => {
                userToken = cookie.value
            });
        })
        
        it('Can start from beginning', function() {
            const { url, title } = this.courses["1"]

            cy.visit(url) // C# Basics

            // get start button
            cy.contains('Start from Beginning').click()

            // get redirected
            cy.url().should('match', /tutorials.*/)

            // course should show up in my courses
            cy.visit('/dashboard')
            cy.contains(title.toUpperCase())
        })

        it('Can Start from any tutorial', function() {
            const { url, title } = this.courses["1"]

            cy.visit(url) // C# Basics

            // get start button
            cy.get('.start').first().click()

            // get redirected
            cy.url().should('match', /tutorials.*/)

            // course should show up in my courses
            cy.visit('/dashboard')
            cy.contains(title.toUpperCase())
        })

        it('Can Start from any tutorial after being registered', function() {
            const { id, url, title } = this.courses["1"]

            // send login request without going through UI
            cy.request({
                method: 'POST',
                url: 'https://localhost:44377/api/Courses/RegisterUser',
                auth: {
                    bearer: userToken
                },
                body: {
                    courseId: id
                }
            })

            cy.visit(url)

            // get start button
            cy.get('.start').first().click()

            // get redirected
            cy.url().should('match', /tutorials.*/)

            // course should show up in my courses
            cy.visit('/dashboard')
            cy.contains(title.toUpperCase())
        })
    })
    
    context('Registered Student', () => {
        beforeEach(function fetchUser () {
            loginAs(users.devStudent);
        })
        
        it('Can start from beginning', function() {
            const { url, title } = this.courses["1"]

            cy.visit(url) // C# Basics

            // get start button
            cy.contains('Start from Beginning').click()

            // get redirected
            cy.url().should('match', /tutorials.*/)

            // course should show up in my courses
            cy.visit('/dashboard')
            cy.contains(title.toUpperCase())
        })

        it('Can Start from any tutorial', function() {
            const { url, title } = this.courses["1"]

            cy.visit(url) // C# Basics

            // get start button
            cy.get('.start').first().click()

            // get redirected
            cy.url().should('match', /tutorials.*/)

            // course should show up in my courses
            cy.visit('/dashboard')
            cy.contains(title.toUpperCase())
        })
    })

    context('Teacher', () => {
        beforeEach(function fetchUser () {
            loginAs(users.devTeacher);
        })

        it('Can create new course', function() {
            const courseTitle = "New Course Title";

            cy.visit('/courses/new')

            // type course info
            cy.get('#course_title').type(courseTitle)
            cy.get('#description').type("New Course description!")

            cy.contains('Publish').click()
            
            // should be redirected to dashboard
            cy.url().should('match', /dashboard\/teacher/)

            cy.contains(courseTitle)
        })
    })
})
  