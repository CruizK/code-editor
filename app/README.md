## Running Tests

# Cypress

Basically, you'll need to:
1. Run `npm install`, ensuring cypress gets downloaded
2. Open SQL Server Management studio, connecting to the canonical database (dev database, ideally)
3. Make a backup (eg: CodeEditorApi.bak) from tasks
4. Copy that backup here, into <root>/CodeEditorApi.bak. Make sure it's not committed to Version Control
5. Run `npm run cypress:open`

# Jest

Just run `npm run test`. Cypress tests are already excluded from our jest configuration.