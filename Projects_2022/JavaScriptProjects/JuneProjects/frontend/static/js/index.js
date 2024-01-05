import Dashboard from "./views/Dashboard.js";
import Posts from "./views/Posts.js";
import Settings from "./views/settings.js";
import PostView from "./views/PostView.js";
import TagInput from "./views/taginput.js";

const pathToRegex = path => new RegExp("^" + path.replace(/\//g, "\\/").replace(/:\w+/g, "(.+)") + "$");

const getParams = match => {
    const values = match.result.slice(1);
    const keys = Array.from(match.route.path.matchAll(/:(\w+)/g)).map(result => result[1]);

    return Object.fromEntries(keys.map((key, i) => {
        return [key, values[i]]
    }));
}

const navigateTo = url => {
    history.pushState(null, null, url);
    router();
}

const router = async () => {

    const routes = [
        { path: "/", view: Dashboard },
        { path: "/posts", view: Posts },
        { path: "/taginput", view: TagInput },
        { path: "/posts/:id", view: PostView },
        { path: "/settings", view: Settings },
    ];

    //Test each route for pentiential match
    const potentialMatches = routes.map(route => {
        return {
            route: route,
            result: location.pathname.match(pathToRegex(route.path))
        };
    });

    //Getting that one true match
    let match = potentialMatches.find(pm => pm.result !== null);

    //reroute to main page if not found
    if (!match) {
        match = {
            route: routes[0],
            result: [location.pathname]
        };
    }
    const view = new match.route.view(getParams(match));
    document.querySelector("#app").innerHTML = await view.getHtml();
};

window.addEventListener("popstate", router);

document.addEventListener("DOMContentLoaded", () => {
    //prevent page from displaying refresh but still direct url to pages
    document.body.addEventListener("click", e => {
        if (e.target.matches("[data-link]")) {
            e.preventDefault();
            navigateTo(e.target.href);
        }
    });
    router();    
})