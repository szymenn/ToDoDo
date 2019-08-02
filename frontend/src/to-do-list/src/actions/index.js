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

export function DeleteToDo(id){
    const jwt = localStorage.getItem(JWT_ID)
    return(dispatch) => {
        return axios.delete(`${apiUrl}/todos/${id}`, {headers:{
            Authorization: `Bearer ${jwt}`
        }})
        .then(result => {
            dispatch(UpdateToDos(result.data))
        })
        .catch(error => {
            throw (error)
        })
    }
}

export function AddToDo(todo){
    const jwt = localStorage.getItem(JWT_ID)
    const headers = {
        Authorization: `Bearer ${jwt}`
    }
    return(dispatch) => {
        return axios.post(`${apiUrl}/todos`, todo, {headers: headers})
        .then(result => {
            dispatch(UpdateToDos(result.data))
        })
        .catch(error => {
            throw (error)
        })
    }
}

export function UpdateToDosRequest(){
    const jwt = localStorage.getItem(JWT_ID);
    return (dispatch) => {
        return axios.get(`${apiUrl}/todos`, {headers: {
            Authorization: `Bearer ${jwt}`
        }})
        .then(result => {
            dispatch(UpdateToDos(result.data))
        })
        .catch(error =>{
            throw (error)
        }) 
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


