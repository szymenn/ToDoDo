import {Jumbotron, Button} from 'reactstrap';
import React from 'react';

export default function ResultNotAuth(props){
    return(
        <Jumbotron>
            <h1 className="display-3">Welcome to ToDoList App</h1>
            <p className="lead">Simple ToDoList app I made in order to learn basics of frontend web development</p>
            <hr className="my-2" />
            <p className="lead">               
                <Button color="primary" onClick={props.handleLogIn}>Log in</Button>
            </p>
            <p className="lead">               
                <Button color="success" onClick={props.handleRegister}>Register</Button>
            </p>
        </Jumbotron>
    )
}