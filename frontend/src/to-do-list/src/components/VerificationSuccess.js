import React from 'react';
import { withRouter } from 'react-router-dom';
import { Jumbotron, Button } from 'reactstrap';

function VerificationSuccess(props){
    function handleLogin(){
        props.history.push('/Login')
    }

    function handleHome(){
        props.history.push('/')
    }

    return(
        <Jumbotron>
        <h1>Email verification successful</h1>
        <p className="lead">               
            <Button color="primary" onClick={handleLogin}>Login</Button>
        </p>
        <p className="lead">               
            <Button color="primary" onClick={handleHome}>Home</Button>
        </p>
        </Jumbotron>
    )
}

export default withRouter(VerificationSuccess)
