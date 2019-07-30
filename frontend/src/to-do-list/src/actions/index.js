import { SET_JWT, UPDATE_TODOS } from '../constants/actionTypes';

export function SetJwt(jwt){
    return {
        type: SET_JWT,
        payload: {
            jwt: jwt
        }
    }
} 

export function UpdateToDos(todos){
    return {
        type: UPDATE_TODOS,
        payload:{
            todos: todos
        }
    }
}
