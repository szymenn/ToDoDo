import { SET_USERNAME } from '../constants/actionTypes';

const initialState = {
    username: ''    
}
const user = (state = initialState, action) => {
    switch(action.types){
        case SET_USERNAME:
            return {
                username: action.payload.username
            }
        default: 
            return state;
    }
}

export default user