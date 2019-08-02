import React from 'react';
import {FormGroup, Form, Button, Badge, Label, Input, Container, Navbar} from 'reactstrap';

export default function RegisterForm(props){
    return(
        <div>
            <Navbar>
                ToDoList App
                <Button color="primary" onClick={props.handleHome}>Home</Button>
            </Navbar>
            <Container>
                <h1><Badge color="primary">Register page</Badge></h1>
                <Form onSubmit={e => props.submit(e)}>
                    <FormGroup>
                        <Label for="username">Username</Label>
                        <Input type="username" name="username" id="username" placeholder="Username"/>
                    </FormGroup>
                    <FormGroup>
                        <Label for="password">Password</Label>
                        <Input type="password" name="password" id="password" placeholder="Password" onKeyUp={props.check}/>
                    </FormGroup>
                    <FormGroup>
                        <Label for="confirm_password">Confirm password</Label>
                        <Input type="password" name="confirm_password" id="confirm_password" placeholder="Confirm password" onKeyUp={props.check}/>
                        <span id='message'></span>
                    </FormGroup>
                    <FormGroup>
                        <Button type="submit" color="success">Register</Button>
                    </FormGroup>
                </Form>
            </Container>
        </div> 
    )
}