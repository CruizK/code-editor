import { loginAs } from '../support/utils'
import users from '../fixtures/auth.json'

describe.skip('Demo', () => {
    before(() => {
        cy.fixture('courses').as('courses')

        // reset and seed the database prior to demo-ing
        cy.exec('npm run db:reset')
    })

    const courseTitle = "New Course Title";

    it('Can create Course', function() {
        loginAs(users.devTeacher)

        cy.visit('/courses/new')

        // type course info
        cy.get('#course_title').type(courseTitle)
        cy.get('#description').type("New Course description!")

        cy.contains('Publish').click()
        
        // should be redirected to dashboard
        cy.url().should('match', /dashboard\/teacher/)

        cy.contains(courseTitle)
    })

    const tutorialTitle = "New Tutorial Title";
    const tutorialPrompt = 'Do some stuff!';

    it('Can create Tutorial', function() {
        loginAs(users.devTeacher)

        // intercept GET for monaco editor worker
        cy.intercept('https://cdn.jsdelivr.net/npm/monaco-editor@0.28.1/min/vs/base/worker/workerMain.js').as('getMonaco')
        cy.intercept('https://cdn.jsdelivr.net/npm/monaco-editor@0.28.1/min/vs/language/*/*Worker.js').as('getMonacoLang')
        
        // visit tutorial page
        cy.visit('/tutorials/new')
        cy.wait('@getMonaco') // tutorial page has monaco editor
        cy.wait('@getMonacoLang')
        
        // select course id
        cy.get('#course_id').select("12")

        // type tutorial info
        cy.get('#tutorial_title').type(tutorialTitle)
        cy.get('#description').type("New Tutorial description!")

        // type tutorial prompt
        cy.get('.demo-editor').type(tutorialPrompt)

        // select template
        cy.get('#template-selector').select("Hello World!")

        cy.contains('Publish').click()
        
        // should be redirected to dashboard
        cy.url().should('match', /dashboard\/teacher/)

        // tutorial list
        cy.intercept('/api/Tutorials/CourseTutorials/*').as('getTutorials')

        cy.contains(courseTitle).click()
        cy.wait('@getTutorials')

        cy.wait(500) // for demo-ing purposes, wait for tutorial list to unfold
        cy.contains(tutorialTitle)
    })

    it('Can view tutorial', function() {
        loginAs(users.devStudent)

        cy.visit('/courses/12')

        // get start button
        cy.get('.start').first().click()

        // get redirected
        cy.url().should('match', /tutorials.*/)

        cy.contains(tutorialPrompt)
    })

    it(`Can see ${courseTitle} in my courses`, function() {
        loginAs(users.devStudent)

        cy.visit('/dashboard')

        cy.contains(courseTitle.toUpperCase())
    })
})
  