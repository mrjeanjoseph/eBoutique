import abstractview from "./abstractview.js";

export default class extends abstractview {
    constructor(params) {
        super(params);
        this.setTitle("Settings");
    }

    async getHtml(){
        return `
        <h1>You're all set</h1>
        <p>If not, then you can Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quos minus aliquid ea provident esse aperiam. dolor sit amet consectetur adipisicing elit?</p>
        <p><a href="/" data-link>Home</a></p>
        `;
    }
}