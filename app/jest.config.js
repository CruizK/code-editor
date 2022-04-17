module.exports = {
    "moduleNameMapper": {
        "^@Components(.*)$": "<rootDir>/src/common/components$1",
        "^@Hooks(.*)$": "<rootDir>/src/common/hooks$1",
        "^@Utils(.*)$": "<rootDir>/src/common/utils$1",
        "^@Common(.*)$": "<rootDir>/src/common$1",
        "^@Modules(.*)$": "<rootDir>/src/modules$1"
      },
    transform: {
      "^.+\\.(js|jsx)$": "babel-jest",
    },
    testPathIgnorePatterns: ["<rootDir>/cypress/"],
};