import { UPDATE_TODOS, UPDATE_TODO} from '../constants/actionTypes';
import { JWT_ID } from '../constants/jwt'; 
import axios from 'axios';

const apiUrl = "https://localhost:5001"

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

export function UpdateToDoRequest(todo, id){
    const jwt = localStorage.getItem(JWT_ID);
    const headers = {
        Authorization: `Bearer ${jwt}`
    }
    return(dispatch) => {
        return axios.put(`${apiUrl}/todos/${id}`, todo, {headers: headers})
        .then(result =>{
            dispatch(UpdateToDo(result.data))
        })
        .catch(error => {
            throw (error)
        })
    }
}