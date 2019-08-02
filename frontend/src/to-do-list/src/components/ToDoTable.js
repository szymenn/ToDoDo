import React,{ useEffect }from 'react'
import axios from 'axios';
import { withRouter } from 'react-router-dom';
import AuthTable from './AuthTable';
import NotAuthTable from './NotAuthTable';
import { connect } from 'react-redux';
import {UpdateToDos, UpdateToDosRequest, DeleteToDo} from '../actions/index';
import { JWT_ID } from '../constants/jwt';

function mapStateToProps(state) {
    return {
    todos: state.todos
    }
}

function ToDoTable(props){

    useEffect(() => {
        if(localStorage.getItem(JWT_ID)!== null){
            props.dispatch(UpdateToDosRequest())
        }
    }, props)

    function handleLogIn() {
        props.history.push('/Login')
    }

    function handleRegister(){
        props.history.push('/Register')
    } 

    function handleDelete(id){
        props.dispatch(DeleteToDo(id))
        props.history.push('/')
    }

    function handleAdd(){
        props.history.push('/Add')
    }

    function handleEdit(){
        props.history.push('/Edit')
    }
    
    if(localStorage.getItem(JWT_ID) !== null){
        return(
            <AuthTable todos={props.todos} handleDelete={handleDelete} handleAdd={handleAdd}/>
        )
    }
    return(
        <NotAuthTable handleLogIn={handleLogIn} handleRegister={handleRegister}/>
    )
}

export default connect(mapStateToProps)(withRouter(ToDoTable))
