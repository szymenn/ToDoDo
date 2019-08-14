import React from 'react';
import {withRouter} from 'react-router-dom';
import { connect } from 'react-redux';
import {LoginUser} from '../actions';
import LoginForm from './LoginForm';
import EnhancedLoginForm from './EnhancedLoginForm'; 

function Login(props){
    
    function handleRegister(){
        props.history.push('/Register')
    }

    function handleHome(){
        props.history.push('/')
    } 

    return(
        <EnhancedLoginForm redirect={props.history.push} dispatch={props.dispatch} handleHome={handleHome} handleRegister={handleRegister}/>
    )
}

export default connect()(withRouter(Login))
