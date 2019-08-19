import { SET_USERNAME } from '../constants/actionTypes';
import { JWT_ID, REFRESH_ID } from '../constants/jwt';
import { SetUpError } from './error';
import axiosInstance from '../axios/config'
import { apiUrl } from '../constants/urls';

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
        return axiosInstance.get(`${apiUrl}/user`, {headers: {
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
        return axiosInstance.post(`${apiUrl}/user/login`, {
            UserName: user.username,
            Password: user.password
        })
        .then(result => {
            localStorage.setItem(JWT_ID, result.data.tokens.accessToken)
            localStorage.setItem(REFRESH_ID, result.data.tokens.refreshToken)
            redirect('/')
        })
        .catch(error => {
            dispatch(SetUpError(error.response.data))
            redirect('/Error')
        });
    }
}

export function RegisterUser(user, redirect){
    return(dispatch) => {
        return axiosInstance.post(`${apiUrl}/user/register`, {
            UserName: user.username,
            Email: user.email,
            Password: user.password,
            ConfirmPassword: user.confirmPassword
        })
        .then( () => {
            redirect('/RegisterSuccess')
        })
        .catch(error => {
            dispatch(SetUpError(error.response.data))
            redirect('/Error')
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
        return axiosInstance.post(`${apiUrl}/user/tokens/revoke`, JSON.stringify(refreshToken), {headers: headers})
        .then(result => {
            localStorage.removeItem(JWT_ID)
            localStorage.removeItem(REFRESH_ID)
            redirect('/')
        })
        .catch(error => {
            dispatch(SetUpError(error.response.data))
            redirect('/Error')
        })
    }
}




