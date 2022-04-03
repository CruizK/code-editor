import { describe, expect, test } from '@jest/globals';
import { levenshtein } from './Tutorials';

// passDistance check
test('Pass distance should be 5% of solution length', () => {
    const solution = '1'.repeat(100);

    const { passDistance } = levenshtein(solution, solution);

    expect(passDistance).toBe(5);
})

// Basic check
test('A string should have a distance of 0 with itself', () => {
    const aString = 'Hello World!';

    const { distance, passes } = levenshtein(aString, aString);

    expect(distance).toBe(0);
    expect(passes).toBeTruthy();
})

// Solutions sometimes have newline characters
test('"Hello World!\\n" should be one away from "Hello World!"', () => {
    const solution = "Hello World!\n";
    const output = "Hello World!";

    const { distance, passes } = levenshtein(solution, output);

    expect(distance).toBe(1);
    expect(passes).toBeTruthy();
});

// sanity check
test('Short should be far from noooooootShort', () => {
    const short = "Short";
    const long = "noooooootShort";

    const { distance, passes } = levenshtein(short, long);

    expect(distance).toBe(9);
    expect(passes).toBeFalsy();
});

// 
test('The Schwartz radius of a star is 3.00009% its gravitational pull', () => {
    const rightish = 'The Schwartz radius of a star is 3.00009% its gravitational pull';
    var wrong = 'The Schwartz radius of a star is 3.33333% its gravitational pull';
    
    const { distance, passes } = levenshtein(rightish, wrong);

    expect(distance).toBe(5);
    expect(passes).toBeFalsy();
})