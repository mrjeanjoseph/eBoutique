import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Tag Input")
    }

    async getHtml() {
        return `
        
            <application class="inputTag">
            <h1 class="header">Welcome to Tag Input</h1>
                <div class="inputTag-wrapper">
                    <div class="inputTag-title">
                        <img src="/static/asset/tag.ico" alt="tag-icon">
                        <h2>tags</h2>
                    </div>
                    <div class="inputTag-content">
                        <p>Press enter or add a comma after each tag</p>
                        <div class="inputTag-tagBox">
                            <ul><input type="text"></ul>
                        </div>
                    </div>
                    <div class="inputTag-details">
                        <p><span># of Tags</span> tags are remaining</p>
                        <button>Remove All</button>
                    </div>
                </div>
            </application>

            <p>
                <a href="/" data-link>Dashboard</a>.
            </p>`
    };    
};