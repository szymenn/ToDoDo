import { SET_UPDATE_ID } from '../constants/actionTypes';

export function SetUpdateId(id){
    return {
        type: SET_UPDATE_ID,
        payload:{
            id: id
        }
    }
}