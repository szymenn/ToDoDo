import React from 'react';
import TaskForm from './TaskForm';
import { UpdateToDoRequest } from '../actions';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';

function mapStateToProps(state) {
    return {
        id: state.ids.updateId
    }
}
function Edit(props){
    function handleSubmit(e){
        e.preventDefault()
        const inputDate = document.getElementById('date').value
        const date = new Date(inputDate).toJSON()
        const task = document.getElementById('task').value

        const todo = {
            task: task,
            date: date
        }
        
        props.dispatch(UpdateToDoRequest(todo, props.id))
        props.history.push('/')
    }

    return (
        <TaskForm handleSubmit={handleSubmit}/>
    )
}

export default connect(mapStateToProps)(withRouter(Edit))