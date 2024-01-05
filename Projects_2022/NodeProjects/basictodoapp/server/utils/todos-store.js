const fs = require("fs");
let todos = { nextid: 0};

if(fs.existsSync("todos.json")){
    todos = JSON.parse(fs.readFileSync("todos.json"));
}

const getTodos = function() {
    return Object.assign({}, todos);
}

const getTodo = function(id) {
    return Object.assign({}, todos[id]);
}

const createTodo = function(todo) {
    todos[todos.nextid++] = todo;
    writeTodos();
}

const updateTodo = function(id, todo){
    todos[id] = todo;
    writeTodos();
}

const deleteTodo = function(id) {
    delete todos[id];
    writeTodos();
}



const writeTodos = function(){
    fs.writeFileSync("todos.json", JSON.stringify(todos));
}

module.exports = {
    CreateTodo: createTodo,
    GetTodo: getTodo,
    GetTodos: getTodos,
    UpdateTodo: updateTodo,
    DeleteTodo: deleteTodo
};