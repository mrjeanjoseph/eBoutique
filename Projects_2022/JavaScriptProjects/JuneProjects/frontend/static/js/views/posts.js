import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Posts")
    }

    async getHtml() {
        return `
        <h1>Welcome to posting page</h1>
        <p>
            Lorem ipsum dolor posts sit amet consectetur adipisicing elit. Quod, consequuntur error aliquid doloremque ex aspernatur, labore enim expedita ipsum illo quos, similique quaerat ratione. Nulla beatae quod dicta nemo. Animi provident nihil soluta atque aliquam post.
        </p>
        <p>
            <a href="/" data-link>Home</a>.
        </p>
    `};
};
