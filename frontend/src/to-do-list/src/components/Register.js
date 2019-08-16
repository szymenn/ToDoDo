import React from 'react';
import {withRouter} from 'react-router-dom';
import EnhancedRegisterForm from './EnhancedRegisterForm';
import { connect } from 'react-redux';

function Register(props){
    
    function handleHome(){
        props.history.push('/')
    }

    return(
        <EnhancedRegisterForm handleHome={handleHome} dispatch={props.dispatch} redirect={props.history.push}/>
    )
}

export default connect()(withRouter(Register))
