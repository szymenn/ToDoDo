import React from 'react';
import {withRouter} from 'react-router-dom';
import { connect } from 'react-redux';
import {LoginUser} from '../actions';
import LoginForm from './LoginForm'

function Login(props){
    function handleRegister(){
        props.history.push('/Register')
    }

    function handleHome(){
        props.history.push('/')
    }

    function submit(e){
        e.preventDefault();
        const user = {
            username: document.getElementById("username").value,
            password: document.getElementById("password").value
        }
        props.dispatch(LoginUser(user, props.history.push))
    }

    return(
        <LoginForm handleHome={handleHome} handleRegister={handleRegister} submit={submit}/>
    )
}

export default connect()(withRouter(Login))
