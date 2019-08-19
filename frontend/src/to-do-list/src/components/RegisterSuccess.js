import React from 'react';
import { withRouter } from 'react-router-dom';
import { Jumbotron, Button } from 'reactstrap';

function RegisterSuccess(props){
    function handleHome(){
        props.history.push('/')
    }

    return(
        <Jumbotron>
            <h1>Thank you for registering, please verify your email</h1>
            <p className="lead">               
                <Button color="primary" onClick={handleHome}>Home</Button>
            </p>
        </Jumbotron>
    )
}

export default withRouter(RegisterSuccess)