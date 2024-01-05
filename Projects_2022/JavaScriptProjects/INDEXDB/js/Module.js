

const productdb = (dbname, table) => {
    //Create database
    const db = new Dexie(dbname);
    db.version(1).stores(table);
    db.open();

    return db;
}

//insert function 
const bulkcreate = (dbtable, data) => {
    let flag = empty(data);
    if (flag) {
        dbtable.bulkAdd([data]);
        console.log("Data inserted successfully...!");
    } else {
        console.log("Please provide Data...!");
    }
    return flag;
}

//check textbox validation
const empty = object => {
    let flag = false;

    for (const value in object) {
        if (object[value] != "" && object.hasOwnProperty(value)) {
            flag = true;
        } else {
            flag = false;
        }
    }
    return flag;
}

//Fetch qty of data from db
const getData = (dbtable, fn) => {
    let index = 0;
    let obj = {};

    dbtable.count((count) => {
        if (count) {
            dbtable.each(table => {
                obj = sortObject(table);
                fn(obj, index++);
            })
        } else {
            fn(0);
        }
    })
}

//sort object
const sortObject = dataObj => {
    let objSorted = {};

    objSorted = {
        id: dataObj.id,
        name: dataObj.name,
        seller: dataObj.seller,
        price: dataObj.price
    }
    return objSorted;
}

// Creating dynamic element
const createDynamicElement = (tagName, appendTo, fn) => {
    const element = document.createElement(tagName);
    if (appendTo) appendTo.appendChild(element);
    if (fn) fn(element);
}

export default productdb;
export {
    bulkcreate,
    getData,
    createDynamicElement,
}