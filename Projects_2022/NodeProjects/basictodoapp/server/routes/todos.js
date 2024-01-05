const express = require('express');
const TodoStore = require("../utils/todos-store");

const router = express.Router();

//GET http://localhost:2022/api/todos/123 - meaning get by Id
router.get("/:id", function(request, response) {
    const id = request.params.id;
    const todo = TodoStore.GetTodo(id);
    response.json(todo);
});

//GET http://localhost:2022/api/todos/ - meaning get all
router.get("/", function(request, response) {
    const todos = TodoStore.GetTodos();
    response.json(todos);
});

//POST http://localhost:2022/api/todos/
// { title: string, content: string }
router.post("/", function(request, response) { //There's an issue here I think
    const newTodo = request.body;
    TodoStore.CreateTodo(newTodo);
    response.json({ msg: 'todo added'} );
});

//POST http://localhost:2022/api/todos/123
// { title: string, content: string }
router.put("/:id", function(request, response) {
    const id = request.params.id;
    const editedTodo = req.body;
    TodoStore.UpdateTodo(id, editedTodo);
    response.json({msg: `todo id ${id} has been edited`});
});

//DELETE http://localhost:2022/api/todos/
router.delete("/:id", function(request, response) {
    const id = request.params.id;
    TodoStore.DeleteTodo(id);
    response.json({msg: `todo id ${id} has been deleted`});
});

module.exports = router;