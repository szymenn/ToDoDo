import React from 'react';
import { 
    Navbar,
    Container,
    Badge,
    Form,
    FormGroup,
    Label,
    Input,
    Button
 } from 'reactstrap';

export default function LoginForm(props){
    return(
        <div>
            <Navbar>
            ToDoList App
                <Button color='primary' onClick={props.handleHome}>Home</Button>
            </Navbar>
            <Container>
                <h1><Badge color="primary">Login page</Badge></h1>
                <Form onSubmit={e => props.submit(e)}>
                    <FormGroup>
                        <Label for="username">Username</Label>
                        <Input type="username" name="username" id="username" placeholder="Username" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="password">Password</Label>
                        <Input type="password" name="password" id="password" placeholder="Password" />
                    </FormGroup>
                    <FormGroup>
                        <Button type="submit">Login</Button>
                    </FormGroup>
                </Form>
                <Button color="success" onClick={props.handleRegister}>Register</Button>
            </Container>
        </div>
    )
}