import { SET_UPDATE_ID } from '../constants/actionTypes';

const initialState = {
    updateId: ''
} 

const ids = (state = initialState, action) => {
    switch(action.type){
        case SET_UPDATE_ID:
            return {
                updateId: action.payload.id
            }
        default:
            return state
    }
}

export default ids