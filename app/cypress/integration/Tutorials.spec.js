import { loginAs } from '../support/utils'
import users from '../fixtures/auth.json'

describe('Tutorials', () => {
    beforeEach(() => {
        // reset and seed the database prior to every test
        cy.exec('npm run db:reset')
    })
    
    context('Unregistered Student', () => {
        beforeEach(function fetchUser () {
            loginAs(users.genericStudents[0]);
        })
        
        // tests
    })
    
    context('Registered Student', () => {
        beforeEach(function fetchUser () {
            loginAs(users.devStudent);
        })
        
        // tests
    })

    context('Teacher', () => {
        beforeEach(function fetchUser () {
            loginAs(users.devTeacher);
        })

        it('Can create new tutorial', function() {
            const tutorialTitle = "New Tutorial Title";

            // intercept GET for monaco editor worker
            cy.intercept('https://cdn.jsdelivr.net/npm/monaco-editor@0.28.1/min/vs/base/worker/workerMain.js').as('getMonaco')
            cy.intercept('https://cdn.jsdelivr.net/npm/monaco-editor@0.28.1/min/vs/language/*/*Worker.js').as('getMonacoLang')
            
            // visit tutorial page
            cy.visit('/tutorials/new')
            cy.wait('@getMonaco') // tutorial page has monaco editor
            cy.wait('@getMonacoLang')

            // type tutorial info
            cy.get('#tutorial_title').type(tutorialTitle)
            cy.get('#description').type("New Tutorial description!")

            cy.contains('Publish').click()
            
            // should be redirected to dashboard
            cy.url().should('match', /dashboard\/teacher/)

            // tutorial list
            cy.intercept('/api/Tutorials/CourseTutorials/*').as('getTutorials')

            cy.contains("C# Basics").click()
            cy.wait('@getTutorials')

            cy.wait(500) // for demo-ing purposes, wait for tutorial list to unfold
            cy.contains(tutorialTitle)
        })
    })
})
  