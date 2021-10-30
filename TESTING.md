# Testing
A simple guide to how testing will be done in the project

## Unit Testing
A complete example of a test can be seen [here](https://github.com/CruizK/code-editor/blob/main/api/CodeEditorApi/CodeEditorApiUnitTests/Features/Auth/RegisterCommandTest.cs). Below is a checklist of things that should be tested.
* Each Command should have a separate test named "WhateverCommandTest.cs"
* Should inherit from the base UnitTest class with generic type of the command you are testing
* If the command can return an Error (such as Bad Request) all of those edge cases should be tested and made sure they return the proper result and message
* One test should go through the whole command and make sure all functions are called and the return is the one expected
* If a specific regex data annotation is used (Such as a password regex) it should be tested in the relevant command that it is used.
* Fixtures should be used for any generated data as it uses a random uuid to make sure no specific inputs break our system.

## Integration Testing
A complete example of an integration test can be seen [here](https://github.com/CruizK/code-editor/blob/main/api/CodeEditorApi/CodeEditorApiIntegrationTests/Features/Auth/GetUserTest.cs).
* Each file that touches or depends on the DatabaseContext should be integration tested to make sure the desired output is the actual one
* All data (should) be wiped between each tests so each one is a completely isolated context.
* Depending on what operation you are doing, different data may need to be created first
  * **Create**: No data needed, just execute it and then query the database for the created object
  * **Read**: Create data and insert it and then make sure the function returns the desired data, if there is some filter, make sure it only returns those who meet the desired criteria.
  * **Update**: Create data and then run the function with one item in the dataset being the one you are updating, query the database and make sure the properties that are meant to be updated, are updated properly
  * **Delete**: Create data and then run the function and make sure it only deletes the specified item.