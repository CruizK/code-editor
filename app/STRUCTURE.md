## Pages
* Actual routes. Don't put raw components in here or nextjs'll try to render them individually as pages.

## src
* Holds most of our code. Add tests for a given \<file\>.js as \<file\>.test.js. See https://www.testim.io/blog/node-js-unit-testing-get-started-quickly-with-examples/ for a quick demo on how to write jest tests. Put chakra component style definitions into \<file\>.style.js.
```
/src
    /common
        /components
            /<Name>
                <Name>.js
                <Name>.style.js
                <Name>.test.js
        /hooks
        /utils
    /modules
        /auth
            /components
                AuthForm.js
                AuthForm.test.js
            auth.js
        /<feature>
            /components
                <FeatureButton>.js
                <FeatureButton?>.test.js
                <OtherComponent>.js
                <OtherComponent>.test.js
            <Feature>.js
```

* /modules
    * If you can map code to an api route / feature, then it probably deserves its own folder in modules.
    * Add components under a 'components' subfolder.
* /common
    * If you're writing components, hooks, or other code, but they don't seem like they fit in a module, then just shove them here in the meantime. 
    * Each Component is inside a folder sharing its name and is put next to its jest test. 
    * /utils
        * Functions that aren't hooks and don't fit into a \<Feature\>.
