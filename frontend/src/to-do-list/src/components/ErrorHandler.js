import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { SetUpError } from '../actions';
import { Jumbotron, Button } from 'reactstrap';

function mapStateToProps(state){
    return {
        title: state.error.title,
        status: state.error.status,
        detail: state.error.detail,
        occurred: state.occurred
    }
}

function ErrorHandler(props){
    function handleHome(){
        props.history.push('/')
    }

    return (
        <Jumbotron>
            <h1>Oops, Something went wrong : (</h1>
            <h1 className="display-3">{props.status}</h1>
            <p className="lead">{props.title}</p>
            <hr className="my-2" />
            <p>{props.detail}</p>
            <p className="lead">
                <Button color="primary" onClick={handleHome}>Back Home</Button>
            </p>
      </Jumbotron>
    )
}

export default connect(mapStateToProps)(withRouter(ErrorHandler))