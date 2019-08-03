import React,{ useEffect }from 'react'
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import {UpdateToDosRequest, DeleteToDo, SetUpdateId} from '../actions/index';
import { JWT_ID } from '../constants/jwt';
import Welcome from './Welcome';
import ToDoTable from './ToDoTable';

function mapStateToProps(state) {
    return {
    todos: state.todos.todos
    }
}

function MainContent(props){

    useEffect(() => {
        if(localStorage.getItem(JWT_ID)!== null && props.todos.length === 0){
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

    function handleEdit(id){
        props.dispatch(SetUpdateId(id))
        props.history.push('/Edit')
    }
    
    if(localStorage.getItem(JWT_ID) !== null){
        return(
            <ToDoTable todos={props.todos} handleDelete={handleDelete} handleAdd={handleAdd} handleEdit={handleEdit}/>
        )
    }
    return(
        <Welcome handleLogIn={handleLogIn} handleRegister={handleRegister}/>
    )
}

export default connect(mapStateToProps)(withRouter(MainContent))
