import abstractview from "./abstractview.js";

export default class extends abstractview {
    constructor(params) {
        super(params);
        this.setTitle("Posts");
    }

    async getHtml(){
        return `
        <h1>Most recent post</h1>
        <p>In the most recent Lorem ipsum, Quos minus aliquid ea provident esse aperiam. dolor sit amet consectetur adipisicing elit?</p>
        <p><a href="/" data-link>Home</a></p>
        `;
    }
}