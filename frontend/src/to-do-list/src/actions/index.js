import { SET_JWT, ADD_TODO, ADD_TODOS } from '../constants/actionTypes';

export default function SetJwt(jwt){
    return {
        type: SET_JWT,
        jwt: jwt
    }
} 

export default function AddToDo(todo){
    return {
        type: ADD_TODO,
        text: todo.text,
    }
}

export default function AddToDos(todos){
    return {
        type: ADD_TODOS,
        todos: todos
    }
}