import {ADD_TODO, UPDATE_TODOS, UPDATE_TODO } from '../constants/actionTypes';

const initialState ={
    todos: []
}
const todos = (state = initialState, action) => {
    switch (action.type){
        case ADD_TODO: 
        return [
            ...state,
            {
                text: action.payload.text,
                date: action.payload.date,
                id: action.payload.id
            }
        ]
        case UPDATE_TODOS:
            return Object.assign({}, state, {
                todos: action.payload.todos
            })
        case UPDATE_TODO:
            return Object.assign({}, state, {
                todos: state.todos.map((todo, index) => {
                    if(todo.id === action.payload.todo.id){
                        return Object.assign({}, todo, {
                            task: action.payload.todo.task,
                            date: action.payload.todo.date,
                            id: action.payload.todo.id
                        })
                    }
                    return todo
                })
            })
        default: 
            return state
    }
}

export default todos