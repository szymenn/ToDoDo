import { SET_UP_ERROR } from '../constants/actionTypes';

const initialState = {
    title: '',
    status: null,
    detail: '',
    occurred: false,
}

const error = (state = initialState, action) => {
    switch(action.type){
        case SET_UP_ERROR:
            return Object.assign({}, state, {
                title: action.payload.title,
                status: action.payload.status,
                detail: action.payload.detail,
                occurred: action.payload.occurred,
            })
        default:
            return state;
    }
}

export default error 