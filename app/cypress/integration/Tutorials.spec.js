import { loginAs } from '../support/utils'
import users from '../fixtures/auth.json'

describe('Tutorials', () => {
    before(() => {
        cy.fixture('courses').as('courses')
        cy.fixture('tutorials').as('tutorials')
    })

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
            loginAs(users.genericStudents[0]);
        })
        
        it('Can run through tutorial', function() {
            const { url } = this.courses["5"]

            cy.visit(url) // Web Dev 101

            // intercept GET for monaco editor worker
            cy.intercept('https://cdn.jsdelivr.net/npm/monaco-editor@0.28.1/min/vs/base/worker/workerMain.js').as('getMonaco')
            cy.intercept('https://cdn.jsdelivr.net/npm/monaco-editor@0.28.1/min/vs/language/*/*Worker.js').as('getMonacoLang')

            // get start button
            cy.contains('Start from Beginning').click()

            // get redirected
            cy.url().should('match', /tutorials.*/)

            // wait for monaco editor to initialize
            cy.wait('@getMonaco') // tutorial page has monaco editor
            cy.wait('@getMonacoLang')

            // editor typing
            const solution = this.tutorials["6"].solution.join('')
            cy.get('.view-lines').type(solution);

            cy.get('input[type=checkbox]:first').click()
            cy.get('input[type=checkbox]:nth(1)').click()
            cy.get('input[type=checkbox]:nth(2)').click()
            cy.get('input[type=checkbox]:nth(3)').click()
            cy.get('input[type=checkbox]:nth(4)').click()
            cy.get('input[type=checkbox]:nth(5)').click()
            cy.get('input[type=checkbox]:nth(6)').click()

            cy.contains('SUBMIT').click()
            cy.contains('CONTINUE >')
        })
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
  