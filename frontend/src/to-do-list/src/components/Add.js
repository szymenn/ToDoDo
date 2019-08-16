import React, { useEffect } from 'react';
import { withRouter } from 'react-router-dom';
import TaskForm from './TaskForm';
import { connect } from 'react-redux';
import { AddToDo } from '../actions';
import { JWT_ID } from '../constants/jwt';
import EnhancedAddForm from './EnhancedAddForm'

function Add(props){
    useEffect(()=>{
        if(!localStorage.getItem(JWT_ID)){
            props.history.push('/Login')
        }
    }, props)

    return(
        <EnhancedAddForm dispatch={props.dispatch} redirect={props.history.push}/>
    )
}

export default connect()(withRouter(Add))