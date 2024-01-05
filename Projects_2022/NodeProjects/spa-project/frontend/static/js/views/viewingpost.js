import abstractview from "./abstractview.js";

export default class extends abstractview {
    constructor(params) {
        super(params);
        this.setTitle("Viewing Post");
    }

    async getHtml(){
        console.log(this.params.id);
        return `
        <h1>You are now viewing a post</h1>
        <p>Where you were viewing a post, Lorem ipsum minus aliquid ea provident esse aperiam. dolor sit amet consectetur adipisicing elit?</p>
        <p><a href="/" data-link>Home</a></p>
        `;
    }
}