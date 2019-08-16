import React, {Component} from 'react';
import {Navbar, Button, Nav, NavItem} from 'reactstrap';
import {withRouter} from 'react-router-dom';
import { connect } from 'react-redux';
import { JWT_ID } from '../constants/jwt';
import { LogoutUser } from '../actions';
import AuthHeader from '../components/AuthHeader';
import NotAuthHeader from '../components/NotAuthHeader';

function Header(props) {
  
    function handleLogin(){
        props.history.push('/Login')
    }

    function handleLogout(){
        props.dispatch(LogoutUser(props.history.push))
    }

    function handleRegister(){
        props.history.push('/Register')
    }

    function handleHome(){
        props.history.push('/')
    }
    
    if(localStorage.getItem(JWT_ID) !== null){
    return(
        <AuthHeader handleHome={handleHome} handleLogout={handleLogout}/>
    )
    }
    return(
        <NotAuthHeader handleLogin={handleLogin} handleRegister={handleRegister}/>
    )
}

export default connect()(withRouter(Header))
