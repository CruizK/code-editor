import { programmingLanguages } from "@Utils/static";

const CSharp = [
    {
        template: 'Hello World',
        code: `
using System;
using System.Threading;

class Example
{
    static void Main()
    {
        Thread.Sleep(10000);
        Console.WriteLine("Hello World!");
    }
}
        `, // hello world
    }
];

const HTML = [
    {
        template: 'Hello World',
        code: `
<button 
    onClick="document.getElementById('demo').innerHTML = 'Change me!'"
>
    Click Me!
</button>
        `, // hello world
    }
]

/**
 * Templates for each language, accessible by the key
 */
const templates = {
    CSharp: CSharp,
    HTML: HTML,
}

const getLanguageFromId = (id) => {
    let languageName;
    
    let languageObj = programmingLanguages.find((lang) => {
        return lang.dbIndex == id;
    });
    if (languageObj)
    languageName = languageObj.value;

    return languageName;
}

/**
 * Should eventually call an api route for grabbing Templates from the DB, then concatenate that
 * @param {*} language String
 * @returns Array<Object> Array of objects
 */
const getCodeTemplates = async (language) => {
    //console.log(language, templates);
    let templatesForLanguage = [];

    if (typeof language != 'undefined' && language != null && templates.hasOwnProperty(language))
    templatesForLanguage = templatesForLanguage.concat(templates[language]);
    // templatesForLanguage = templatesForLanguage.concat(await getTemplatesFromApi);

    return templatesForLanguage;
}

export { templates, getLanguageFromId, getCodeTemplates };