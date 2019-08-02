import React, { Component } from 'react';
import {FormGroup, Form, Button, Badge, Label, Input, Container, Navbar} from 'reactstrap';
import {withRouter} from 'react-router-dom';
import RegisterForm from './RegisterForm';
import axios from 'axios';
import { connect } from 'react-redux';
import { RegisterUser } from '../actions';

function Register(props){
    
    function handleHome(){
        props.history.push('/')
    }

    function submit(e) {
        e.preventDefault();
        const user = {
            username: document.getElementById('username').value,
            password: document.getElementById('password').value,
            confirmPassword: document.getElementById('confirm_password').value
        }
        props.dispatch(RegisterUser(user, props.history.push))
    }

    function check () {
        if (document.getElementById('password').value === document.getElementById('confirm_password').value) {
            document.getElementById('message').style.color = 'green';
            document.getElementById('message').innerHTML = 'matching';
        } else {
            document.getElementById('message').style.color = 'red';
            document.getElementById('message').innerHTML = 'not matching';
        }
    }

    return(
        <RegisterForm handleHome={handleHome} submit={submit} check={check}/>
    )
}

export default connect()(withRouter(Register))
