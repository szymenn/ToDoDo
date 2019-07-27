import {ADD_TODO, ADD_TODOS} from '../constants/actionTypes';

const todos = (state = [], action) => {
    switch (action.type){
        case ADD_TODO: 
        return [
            ...state,
            {
                id: action.id,
                text: action.text
            }
        ]
        case ADD_TODOS:
            return[
                ...state,
                action.todos
            ]
        default:
            return state
    }
}

export default todos