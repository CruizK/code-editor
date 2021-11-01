/**
 * Ideally we'd get this from the DB, but we're fine to hardcode it here until we have reason to get it from the DB.
 * As of now, we don't expect config tables to change often enough to warrant changes here. 
 */

const difficultylevels = [
    {
        value: 'Easy',
        dbIndex: 1,
    },
    {
        value: 'Medium',
        dbIndex: 2,
    },
    {
        value: 'Hard',
        dbIndex: 3,
    },
];

const programmingLanguages = [
    {
        value: 'CSS',
        dbIndex: 1,
    }, 
    {
        value: 'CSharp',
        dbIndex: 2,
    }, 
    {
        value: 'HTML',
        dbIndex: 3,
    },
    {
        value: 'Java',
        dbIndex: 4,
    },
    {
        value: 'Javascript',
        dbIndex: 5,
    },
    {
        value: 'Python',
        dbIndex: 6,
    },
];

export { difficultylevels, programmingLanguages };