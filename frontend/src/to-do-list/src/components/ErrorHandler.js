import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { SetUpError } from '../actions';

function mapStateToProps(state){
    return {
        title: state.error.title,
        status: state.error.status,
        detail: state.error.detail,
        occurred: state.occurred
    }
}

function ErrorHandler(props){
    return (
        <div>
            <h1>Something went wrong </h1>
            <h2>{props.title}</h2>
            <h2>{props.status}</h2>
            <h2>{props.detail}</h2>
        </div>
    )
}

export default connect(mapStateToProps)(withRouter(ErrorHandler))