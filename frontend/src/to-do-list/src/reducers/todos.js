import {ADD_TODO, UPDATE_TODOS } from '../constants/actionTypes';

const todos = (state = [], action) => {
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
            return action.payload.todos
        default: 
            return state
    }
}

export default todos