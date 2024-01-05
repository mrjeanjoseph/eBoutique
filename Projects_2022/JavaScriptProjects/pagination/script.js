

// for (let i = 1; i <= 2; i++) {

//     for (let j = 1; j <= 2; j++) {

//         if ((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0)) {

//             var d = document.createElement("div");
//             document.body.appendChild(d);
//             d.className = "black";
//         }
//         else {

//             var d = document.createElement("div");
//             document.body.appendChild(d);
//             d.className = "pink";
//         }
//     }
// }

let totalProj = 3;

for (let i = 0; i <= totalProj; i++) {
    var d = document.createElement("div");
    document.body.appendChild(d);
}
d.className = "black";


$("div").prepend("<div class='child-div'>some text</div>");
