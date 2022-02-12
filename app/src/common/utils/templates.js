import { programmingLanguages } from "@Utils/static";

const CSS = [
    {
        template: 'Hello World!',
        code: `
<html>
    <head>
        <style>
            h1 {color:red;}
            p {color:blue;}
        </style>
    </head>
    <body>
        <h1>A heading</h1>
        <p>A paragraph.</p>
    </body>
</html>
        ` // hello world
    }
]

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
<html>
    <head>
    </head>
    <body>
        <button 
            onClick="document.getElementById('demo').innerHTML = 'Change me!'"
        >
            Click Me!
        </button>
        <p id='demo'></p> 
    </body>
</html>
        `, // hello world
    }
];

const Java = [
    {
        template: 'Hello World',
        code: `
class HW {
    public static void main(String[] args) {
        System.out.println("Hello World!"); 
    }
}
        `, // hello world
    }
];

const Javascript = [
    {
        template: 'Hello World!',
        code: `
<html>
        <head>
            <script>
                console.log("Hello World!");
            </script>
        </head>
        <body></body>
</html>
        `, // hello world
    }
];

const Python = [
    {
        template: 'Hello World',
        code: `
print("Hello world")
        `, // hello world
    }
];

/**
 * Templates for each language, accessible by the key
 */
const templates = {
    CSS: CSS,
    CSharp: CSharp,
    HTML: HTML,
    Java: Java,
    Javascript: Javascript,
    Python: Python,
}

const getLanguageFromId = (id) => {
    let languageName;
    
    let languageObj = programmingLanguages.find((lang) => {
        return lang.dbIndex == id;
    }) || programmingLanguages[0];
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