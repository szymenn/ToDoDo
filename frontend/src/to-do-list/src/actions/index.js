import { SET_JWT, UPDATE_TODOS, DELETE_JWT } from '../constants/actionTypes';
import axios from 'axios';
import { JWT_ID } from '../constants/jwt';

const apiUrl = "https://localhost:5001"

export function SetJwt(jwt){
    return {
        type: SET_JWT,
        payload: {
            jwt: jwt
        }
    }
} 

export function DeleteJwt(){
    return {
        type: DELETE_JWT
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

export function LoginUser(user){
    return() => {
        return axios.post(`${apiUrl}/user/login`, {
            UserName: user.username,
            Password: user.password
        })
        .then(result => {
            localStorage.setItem(JWT_ID, result.data.token)
        })
        .catch(error => {
            throw(error);
        });
    }
}

export function Logout() {
    return(dispatach) => {
        localStorage.removeItem(JWT_ID)
    }
}


