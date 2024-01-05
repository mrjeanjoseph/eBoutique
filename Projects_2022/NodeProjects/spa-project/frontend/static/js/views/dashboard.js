import abstractview from "./abstractview.js";

export default class extends abstractview {
    constructor(params) {
        super(params);
        this.setTitle("Dashboard");
    }

    async getHtml(){
        return `
        <h1>Welcome Louna</h1>
        <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quos minus aliquid ea provident esse aperiam. dolor sit amet consectetur adipisicing elit?</p>
        <p><a href="/posts" data-link>View recent posts</a></p>
        `;
    }
}