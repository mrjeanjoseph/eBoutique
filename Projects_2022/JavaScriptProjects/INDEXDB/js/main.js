import productdb, { bulkcreate, getData, createDynamicElement, } from './Module.js';

window.onload = () => {
    textID(userid);
    table();
}

function textID(textboxid) {
    getData(db.products, data => {
        textboxid.value = data.id + 1 || 1;
    });
}

let db = productdb("Productdb", {
    products: `++id, name, seller, price`
});

//input tags
const userid = document.getElementById("userid");
const proname = document.getElementById("proname");
const seller = document.getElementById("seller");
const price = document.getElementById("price");

//buttons
const btncreate = document.getElementById("btn-create");
const btnread = document.getElementById("btn-read");
const btnupdate = document.getElementById("btn-update");
const btndeleteAll = document.getElementById("btn-delete");

//init notfound
const notfound = document.querySelector("#notfound");

//insert values using create button
btncreate.onclick = (event) => {
    let flag = bulkcreate(db.products, {
        name: proname.value,
        seller: seller.value,
        price: price.value,
    })
    //console.log(flag);

    //empty input field on create
    // proname.value = "";
    // seller.value = "";
    // price.value = "";

    getData(db.products, (data) => {
        userid.value = data.id + 1 || 1;// adding 1 b/c we read existing data but need next value. the or 1 is if there's no data in the database. without it it will return nn
    });   

    let insertmsg = document.querySelector(".insertmsg");
    insertmsg.textContent = 'Data Inserted Successfully...!';
    getMsg(flag, insertmsg);
    table();
}

//Create event on btn read button
btnread.onclick = table;

//Update event on btn update button
btnupdate.onclick = () => {
    const id = parseInt(userid.value || 0);
    if (id) {
        db.products.update(id, {
            name: proname.value,
            seller: seller.value,
            price: price.value
        }).then((updated) => {
            //let get = updated ? 'Data Updated' : `Could not update data`;
            let get = updated ? true: false;
            table();
            let updatemsg = document.querySelector(".updatemsg");
            updatemsg.textContent = 'Data Updated Successfull...!'
            getMsg(get,updatemsg);
            
        })
    }
}

//Delete all records
btndeleteAll.onclick = () => {
    db.delete();
    db = productdb("Productdb", {
        products: `++id, name, seller, price`
    });
    db.open();
    table();
    textID(userid);
        
    let deletemsg = document.querySelector(".deletemsg");
    deletemsg.textContent = 'All data deleted Successfull...!'
    getMsg(true, deletemsg);
}

function table() {
    proname.value = seller.value = price.value = "";//much simpler logic

    //This is one way to create dynamic tables in javascript - don't do that
    /*const tablebody = document.getElementById("tbody");
    let tabledata = document.createElement("td");
    tablebody.appendChild(tabledata);
    console.log(tablebody);*/

    //With fewer lines of code, we can create dynamic element 
    /*const tbody = document.getElementById("tbody");
    createDynamicElement("td", tbody, (td) => {
        console.log(tbody);
    })*/

    const tbody = document.getElementById("tbody");

    while (tbody.hasChildNodes()) {
        tbody.removeChild(tbody.firstChild);
    }

    getData(db.products, (data) => {
        if (data) {
            createDynamicElement("tr", tbody, tr => {
                for (const value in data) {
                    createDynamicElement("td", tr, td => {
                        td.textContent = data.price === data[value] ? `$${data[value]}` : data[value];
                    })
                }
                createDynamicElement("td", tr, td => {
                    createDynamicElement("i", td, i => {
                        i.className += "fas fa-edit btnedit";
                        i.setAttribute('data-id', data.id)
                        i.onclick = editBtn;
                    })
                })
                createDynamicElement("td", tr, td => {
                    createDynamicElement("i", td, i => {
                        i.className += "fas fa-trash-alt btndelete";
                        i.setAttribute('data-id', data.id)
                        i.onclick = deleteBtn;
                    })
                })
            })
        } else {
            notfound.textContent = "No Record Found in the Database...!";
        }
    })

}

function editBtn(event) {
    let id = parseInt(event.target.dataset.id);

    db.products.get(id, data => {
        userid.value = data.id || 0;
        proname.value = data.name || "";
        seller.value = data.seller || "";
        price.value = data.price || "";
    })
}

function deleteBtn(event) {
    let id = parseInt(event.target.dataset.id);
    db.products.delete(id);
    table();
    let deletemsg = document.querySelector(".deletemsg");
    deletemsg.textContent = 'Data Deleted Successfull...!'
    getMsg(true, deletemsg);
}

function getMsg(flag, element) {
    if (flag) {
        element.className += " movedown";
        setTimeout(() => {
            element.classList.forEach(classname => {
                classname == "movedown" ? undefined : element.classList.remove("movedown");
            })
        }, 4000);
    }
}