import React from 'react';
import {Button, Navbar, Nav, NavItem} from 'reactstrap';

export default function AuthHeader(props){
    return (
        <div>
            <Navbar>
                ToDoList App
                <Nav className="ml-auto">
                    <NavItem>
                        <Button color='primary' onClick={props.handleHome}>Home</Button>{' '}
                        <Button color='primary' onClick={props.handleLogout}>Log out</Button>
                    </NavItem>
                </Nav>
            </Navbar>
        </div>
    )
}
