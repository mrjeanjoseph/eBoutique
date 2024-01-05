import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Dashboard")
    }

    async getHtml() {
        return `
        <h1 class="header">Welcome to setting page</h1>
        <p>
            Lorem ipsum setting pages sit amet consectetur adipisicing elit. Quod, consequuntur error aliquid doloremque ex aspernatur, labore enim expedita ipsum illo quos, similique quaerat ratione. Nulla beatae quod dicta nemo. Animi provident nihil soluta atque aliquam - settings.
        </p>
        <p>
            <a href="/" data-link>Dashboard</a>.
        </p>
    `};
};
