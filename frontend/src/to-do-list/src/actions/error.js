import { SET_UP_ERROR } from '../constants/actionTypes';

export function SetUpError(error) {
    return {
        type: SET_UP_ERROR,
        payload: { 
            title: error.title,
            status: error.status,
            detail: error.detail,
            occurred: true,
        }
    }
}