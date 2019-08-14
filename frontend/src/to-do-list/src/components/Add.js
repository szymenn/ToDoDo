import React, { useEffect } from 'react';
import { withRouter } from 'react-router-dom';
import TaskForm from './TaskForm';
import { connect } from 'react-redux';
import { AddToDo } from '../actions';
import { JWT_ID } from '../constants/jwt';
import EnhancedTaskForm from './EnhancedTaskForm';

function Add(props){
    useEffect(()=>{
        if(!localStorage.getItem(JWT_ID)){
            props.history.push('/Login')
        }
    }, props)

    // function handleSubmit(e){
    //     const inputDate = document.getElementById('date').value
    //     const date = new Date(inputDate).toJSON()
    //     const task = document.getElementById('task').value

    //     const todo = {
    //         task: task,
    //         date: date
    //     }
        
    //     props.dispatch(AddToDo(todo))
    //     props.history.push('/')
    // }

    return(
        <EnhancedTaskForm dispatch={props.dispatch} redirect={props.history.push}/>
    )
}

export default connect()(withRouter(Add))