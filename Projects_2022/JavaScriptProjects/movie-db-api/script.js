

const SEARCH_API = `https://api.themoviedb.org/3/movie/550?api_key=${API_KEY}`;
//const SEARCH_API = `https://api.themoviedb.org/3/search/movie?&api_key=${ API_KEY }&query=`;
const IMG_PATH = "http://image.tmbd.org/t/p/w500";


const searchPage = document.querySelector('#search-page');
const searchResult = document.querySelector('#search-result');
const btn = document.querySelector('#btn');
const form = document.querySelector('#form');

const search = document.querySelector('#search');
const results = document.querySelector('#results');
const offlinePage = document.querySelector('#offline-page');
const searchTitle = document.querySelector('#search-title');
const message = document.querySelector('#message');

const detailsPage = document.querySelector('#details-page');
const cardName = document.querySelector('#card-name');
const title = document.querySelector('#title');
const rating = document.querySelector('#rating');

const date = document.querySelector('#date');
const img = document.querySelector('#card-img');
const description = document.querySelector('#description');
const body = document.querySelector('#body');


form.addEventListener('submit', (e) => {
    e.preventDefault();
    handleEvents();
});

function handleEvents() {
    // console.log("Test - "+ window.navigator.onLine);
    if (window.navigator.onLine) {
        searchResult.style.display = "block";
        searchPage.style.display = "none";

        const title = search.value;
        if(title){
            searchTitle.innerText = title;
            showMovies(SEARCH_API + title);
            search.value = "";
        } else {
            searchTitle.innerText = "No Title";
            searchPage.style.display = "none";
            offlinePage.style.display = "flex";
            message.innerHTML = "Type something to search";
        }
    } else {
        searchPage.style.display = "none";
        offlinePage.style.display = "flex";
    }
}

function showMovies(url){
    fetch(url)
    .then(res=>res.json())
    .then(function(data){
        if(data.results.lenghth){
            data.results.forEach(element =>{
                if(element.poster_path){
                    const el = createElement("div");
                    const image = createElement("img");
                    const text = createElement("p");
                }
            })
        }
    })
}