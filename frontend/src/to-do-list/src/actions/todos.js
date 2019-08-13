import { UPDATE_TODOS, UPDATE_TODO} from '../constants/actionTypes';
import { JWT_ID, REFRESH_ID } from '../constants/jwt'; 
import { resolve } from 'url';
import axiosInstance from '../axios/config';
import { apiUrl } from '../constants/urls';

export function UpdateToDos(todos){
    return {
        type: UPDATE_TODOS,
        payload:{
            todos: todos
        }
    }
}

export function UpdateToDo(todo){
    return {
        type: UPDATE_TODO,
        payload:{
            todo: todo
        }
    }
}

export function DeleteToDo(id){
    const jwt = localStorage.getItem(JWT_ID)
    return(dispatch) => {
        return axiosInstance.delete(`${apiUrl}/todos/${id}`, {headers:{
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
        return axiosInstance.post(`${apiUrl}/todos`, todo, {headers: headers})
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
        return axiosInstance.get(`${apiUrl}/todos`, {headers: {
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

export function UpdateToDoRequest(todo, id){
    const jwt = localStorage.getItem(JWT_ID);
    const headers = {
        Authorization: `Bearer ${jwt}`
    }
    return(dispatch) => {
        return axiosInstance.put(`${apiUrl}/todos/${id}`, todo, {headers: headers})
        .then(result =>{
            dispatch(UpdateToDo(result.data))
        })
        .catch(error => {
            throw (error)
        })
    }
}

