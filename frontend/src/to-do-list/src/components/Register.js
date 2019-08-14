import React from 'react';
import {withRouter} from 'react-router-dom';
import EnhancedRegisterForm from './EnhancedRegisterForm';
import { connect } from 'react-redux';

function Register(props){
    
    function handleHome(){
        props.history.push('/')
    }

    // function check () {
    //     if (document.getElementById('password').value === document.getElementById('confirm_password').value) {
    //         document.getElementById('message').style.color = 'green';
    //         document.getElementById('message').innerHTML = 'matching';
    //     } else {
    //         document.getElementById('message').style.color = 'red';
    //         document.getElementById('message').innerHTML = 'not matching';
    //     }
    // }

    return(
        <EnhancedRegisterForm handleHome={handleHome} dispatch={props.dispatch} redirect={props.history.push}/>
    )
}

export default connect()(withRouter(Register))
