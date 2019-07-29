import {ADD_TODO, ADD_TODOS} from '../constants/actionTypes';

const todos = (state = [], action) => {
    switch (action.type){
        case ADD_TODO: 
        return [
            ...state,
            {
                id: action.id,
                text: action.text,
                date: action.date
            }
        ]
        case INITIAL_TODOS:
            return
                action.todos            
        default:
            return state
    }
}

export default todos