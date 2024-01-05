export default class NavHeader {
    constructor (querySelectStr) {
        this.root = document.querySelector(querySelectStr);
        this.root.innerHTML = NavHeader.navHeader();
    }

    static navHeader() {
        const listOfProj = {
            calculator: "calculator",

        };
        return `
            <div class="container-fluid">
            <a class="navbar-brand" href="#">All Projects</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false"
                aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                    <li class="nav-item">
                        <a class="nav-link active" aria-current="page" href="#">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">${listOfProj.calculator}</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">Project Two</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">Project Three</a>
                    </li>
                </ul>
                <form class="d-flex" role="search">
                    <input class="form-control me-2" type="search" placeholder="Search for a project" aria-label="Search">
                </form>
                <ul class="navbar-nav mb-2 mb-lg-0">
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown"
                            aria-expanded="false">
                            Under Construction
                        </a>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="#">Project One</a></li>
                            <li><a class="dropdown-item" href="#">Project Two</a></li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li><a class="dropdown-item" href="#">More Information</a></li>
                        </ul>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link disabled">Admin</a>
                    </li>
                </ul>
            </div>
        </div>`;        
    }
}

