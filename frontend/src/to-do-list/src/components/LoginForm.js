import React from 'react';
import { Navbar, Container, Badge, Form, FormGroup, Label, Input, Button, FormText } from 'reactstrap';
 
export default function LoginForm(props){
    const { handleChange, handleSubmit, values, handleHome, handleRegister, errors, touched } = props 
    return(
        <div>
            <Navbar>
            ToDoList App
                <Button color='primary' onClick={handleHome}>Home</Button>
            </Navbar>
            <Container>
                <h1><Badge color="primary">Login page</Badge></h1>
                <Form onSubmit={handleSubmit}>
                    <FormGroup>
                        <Label for="username">Username</Label>
                        <Input type="username" name="username" placeholder="Username" value={values.username} onChange={handleChange}/>
                        {errors.username && touched.username && <FormText color="danger">{errors.username}</FormText>}
                    </FormGroup>
                    <FormGroup>
                        <Label for="password">Password</Label>
                        <Input type="password" name="password" placeholder="Password" value={values.password} onChange={handleChange}/>
                        {errors.password && touched.password && <FormText color="danger">{errors.password}</FormText>}
                    </FormGroup>
                    <FormGroup>
                        <Button type="submit">Login</Button>
                    </FormGroup>
                </Form>
                <Button color="success" onClick={handleRegister}>Register</Button>
            </Container>
        </div>
    )
}
