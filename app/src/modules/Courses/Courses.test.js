import { courseRegEx } from '@Modules/Courses/Courses';
import { describe, expect, test } from '@jest/globals';

let re = new RegExp(courseRegEx());

const incorrect = [
    '*', '&', '!', '_', '-',
    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
    'J*va 32',
    '_python_stuff',
    'Hello World!',
    '00000000000000000',
]; // see courseRegEx definition for rationale

// titles that should fail
test.each(incorrect)(`re-course-should-fail-'%s'`, (badString) => {
    let passes = re.test(badString);
    expect(passes).toBe(false);
});

const correct = [
    'Java 32',
    'python stuff',
    'Hello World',
    'ABCD00000000',
]; // see courseRegEx definition for rationale

//titles that should pass
test.each(correct)(`re-course-should-pass-'%s'`, (goodString) => {
    let passes = re.test(goodString);
    expect(passes).toBe(true);
});
