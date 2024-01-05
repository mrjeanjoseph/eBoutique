import dashboard from './views/dashboard.js';
import postsview from './views/postsview.js';
import settingsview from './views/settingsview.js';
import viewingpost from './views/viewingpost.js';

const pathToRegex = path => // Must use ES6 for this one.
    new RegExp("^" + path.replace(/\//g, "\\/")
        .replace(/:\w+/g, "(.+)") + "$");

const getParams = match => {
    const values = match.result.slice(1);
    const keys = Array.from(match.route.path.matchAll(/:(\w+)/g))
        .map(result => result[1]);

    // console.log(Array.from(match.route.path.matchAll(/:(\w+)/g)))
    // return {};
    return Object.fromEntries(keys.map((key, i) => {
        return [key, values[i]];
    }));
}


const navigateTo = function (url) {
    history.pushState(null, null, url);
    router();
}

const router = async function () {
    console.log(pathToRegex("/posts/:id"));
    // /posts/:id/
    const routes = [
        { path: "/", view: dashboard },
        { path: "/posts", view: postsview },
        { path: "/posts/:id", view: viewingpost },
        { path: "/settings", view: settingsview },
    ];

    //Test each route for potential match
    const potentialMatches = routes.map(function (route) {
        return {
            route: route,
            result: location.pathname.match(pathToRegex(route.path))
        };
    });

    let match = potentialMatches.find(potentialMatch => potentialMatch.result !== null);

    //If not aviable, then default to root display
    if (!match) {
        match = {
            route: routes[0],
            // isMatch: true
            result: [location.pathname]
        };
    };

    // console.log(potentialMatches);
    // console.log(match.route.view());

    const view = new match.route.view(getParams(match));

    document.querySelector("#app").innerHTML = await view.getHtml();
};
window.addEventListener("popstate", router);

document.addEventListener("DOMContentLoaded", function () {
    document.body.addEventListener("click", function (event) {
        if (event.target.matches("[data-link]")) {
            event.preventDefault();
            navigateTo(event.target.href);
        }
    });
    router();
});