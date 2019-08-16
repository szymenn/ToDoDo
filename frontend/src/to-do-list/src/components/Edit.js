import React, { useEffect } from 'react';
import TaskForm from './TaskForm';
import { UpdateToDoRequest } from '../actions';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import EnhancedEditForm from './EnhancedEditForm';
import { JWT_ID } from '../constants/jwt';

function mapStateToProps(state) {
    return {
        id: state.ids.updateId,
        todos: state.todos.todos
    }
}
function Edit(props){
    useEffect(()=>{
        if(!localStorage.getItem(JWT_ID)){
            props.history.push('/Login')
        }
    }, props)

    const todo = props.todos.find((todo) => {
        return todo.id === props.id
    })
    const date = new Date(todo.date)
    return (
        <EnhancedEditForm dispatch={props.dispatch} redirect={props.history.push} id={props.id} task={todo.task} date={todo.date}/>
    )
}

export default connect(mapStateToProps)(withRouter(Edit))