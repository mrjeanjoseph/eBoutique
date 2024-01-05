import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Viewing Posts")
    }

    async getHtml() {
        console.log(this.params.id);
        return `
        <h1>Welcome to view post page</h1>
        <p>
            Lorem ipsum dolor posts sit amet consectetur adipisicing elit.
        </p>
        <p>
            <a href="/" data-link>Home</a>.
        </p>
    `};
};
