import { SET_USERNAME } from '../constants/actionTypes';
import { JWT_ID } from '../constants/jwt';
import axios from 'axios';

const apiUrl = "https://localhost:5001"

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
        return axios.get(`${apiUrl}/user`, {headers: {
            Authorization: `Bearer ${jwt}`}})
        .then(result => {
            dispatch(SetUsernameSuccess(result.data.userName))
        })
        .catch(error => {
            throw (error)
        })
    }
}

export function LoginUser(user, redirect){
    return(dispatch) => {
        return axios.post(`${apiUrl}/user/login`, {
            UserName: user.username,
            Password: user.password
        })
        .then(result => {
            localStorage.setItem(JWT_ID, result.data.token)
            redirect('/')
        })
        .catch(error => {
            throw (error)
        });
    }
}

export function RegisterUser(user, redirect){
    return(dispatch) => {
        return axios.post(`${apiUrl}/user/register`, {
            UserName: user.username,
            Password: user.password,
            ConfirmPassword: user.confirmPassword
        })
        .then(result => {
            localStorage.setItem(JWT_ID, result.data.token)
            redirect('/')
        })
        .catch(error => {
            throw (error)
        })
    }
}

export function LogoutUser(redirect) {
    return(dispatch) => {
        localStorage.removeItem(JWT_ID)
        redirect('/')
    }
}


