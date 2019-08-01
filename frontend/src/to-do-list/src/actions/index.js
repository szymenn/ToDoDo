import {UPDATE_TODOS, SET_USERNAME } from '../constants/actionTypes';
import axios from 'axios';
import { JWT_ID } from '../constants/jwt';

const apiUrl = "https://localhost:5001"

export function UpdateToDos(todos){
    return {
        type: UPDATE_TODOS,
        payload:{
            todos: todos
        }
    }
}

export function SetUsernameSuccess(username){
    return{
        type: SET_USERNAME,
        payload:{
            username: username
        }
    }

}

export function SetUsername(){
    const jwt = localStorage.getItem(JWT_ID);
    return(dispatch) => {
        return axios.get('https://localhost:5001/user', {headers: {
            Authorization: `Bearer ${jwt}`}})
        .then(result => {
            dispatch(SetUsernameSuccess(result.data.userName))
        })
        .catch(error => {
            throw (error)
        })
    }
}

// export function LoginSuccess(){
//     return{
//         type: LOGIN_SUCCESS
//     }
// }

// export function LoginFail(error){
//     return {
//         type: LOGIN_FAIL,
//         payload: {
//             error: error
//         }
//     }
// }

// export function Logout(){
//     return {
//         type: LOGOUT
//     }
// }

export function LoginUser(user){
    return(dispatch) => {
        return axios.post(`${apiUrl}/user/login`, {
            UserName: user.username,
            Password: user.password
        })
        .then(result => {
            localStorage.setItem(JWT_ID, result.data.token)
            // dispatch(LoginSuccess())
        })
        .catch(error => {
            throw (error)
            // dispatch(LoginFail(error))
        });
    }
}

export function LogoutUser() {
    return(dispatch) => {
        localStorage.removeItem(JWT_ID)
        // dispatch(Logout())
    }
}


