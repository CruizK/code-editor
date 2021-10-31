/**
 * Ideally we'd get this from the DB, but we're fine to hardcode it here until we have reason to get it from the DB.
 * As of now, we don't expect config tables to change often enough to warrant changes here. 
 */

const difficultylevels = [
    'Easy',
    'Medium',
    'Hard',
];

const programmingLanguages = [
    'CSS', 
    'CSharp', 
    'HTML',
    'Java',
    'Javascript',
    'Python',
];

export { difficultylevels, programmingLanguages };