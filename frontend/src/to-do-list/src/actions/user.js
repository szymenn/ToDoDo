import { SET_USERNAME } from '../constants/actionTypes';
import { JWT_ID, REFRESH_ID } from '../constants/jwt';
import axios from 'axios';
import { resolve } from 'dns';

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
            localStorage.setItem(JWT_ID, result.data.tokenInfo.accessToken)
            localStorage.setItem(REFRESH_ID, result.data.tokenInfo.refreshToken)
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
            localStorage.setItem(JWT_ID, result.data.tokenInfo.accessToken)
            localStorage.setItem(REFRESH_ID, result.data.tokenInfo.refreshToken)
            redirect('/')
        })
        .catch(error => {
            throw (error)
        })
    }
}

export function LogoutUser(redirect) {
    return(dispatch) => {
        const refreshToken = localStorage.getItem(REFRESH_ID)
        const jwt = localStorage.getItem(JWT_ID)
        const headers = {
            Authorization: `Bearer ${jwt}`,
            'Content-Type': 'application/json'
        }
        return axios.post(`${apiUrl}/user/tokens/revoke`, JSON.stringify(refreshToken), {headers: headers})
        .then(result => {
            localStorage.removeItem(JWT_ID)
            localStorage.removeItem(REFRESH_ID)
            redirect('/')
        })
        .catch(error => {
            throw (error)
        })
    }
}

axios.interceptors.response.use((response) => {
    return response
}, error => {
    if(error.response.status === 401){
        const refreshToken = localStorage.getItem(REFRESH_ID)
        const headers = {
            'Content-Type':'application/json'
        }
        return axios.post(`${apiUrl}/user/tokens/refresh`, JSON.stringify(refreshToken), {headers: headers})
        .then(result => {
            localStorage.setItem(JWT_ID, result.data.tokenInfo.accessToken)
            localStorage.setItem(REFRESH_ID, result.data.tokenInfo.refreshToken)
            error.config.headers.Authorization = `Bearer ${localStorage.getItem(JWT_ID)}`
            return axios.request(error.config)
        })
    }
    return Promise.reject(error)
})

