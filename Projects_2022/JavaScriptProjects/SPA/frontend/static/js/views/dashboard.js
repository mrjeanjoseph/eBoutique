import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Dashboard")
    }

    async getHtml() {
        return `
        <h1 class="header">${GreetingMessage()}</h1>
        <p>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Quod, consequuntur error aliquid doloremque ex aspernatur, labore enim expedita ipsum illo quos, similique quaerat ratione. Nulla beatae quod dicta nemo. Animi provident nihil soluta atque aliquam.
        </p>
        <p>
            <a href="/posts" data-link>View recent posts</a>.
        </p>
    `;}
}

function GreetingMessage() {
    return `Welcome to CRUD web app`;
}