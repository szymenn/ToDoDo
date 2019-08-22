import React from 'react';
import {Navbar, Button, Nav, NavItem} from 'reactstrap';

export default function NotAuthHeader(props) {
    return(
        <div>
            <Navbar>ToDoList App                     
                <Nav className="ml-auto" navbar>
                    <NavItem>
                        <Button color='primary' onClick={props.handleLogin}>Log in</Button> {' '}
                        <Button color='success' onClick={props.handleRegister}>Register</Button>
                    </NavItem>
                </Nav>
            </Navbar>
        </div>
    )
}
