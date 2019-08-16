import React from 'react';
import { FormGroup, Form, Button, Badge, Label, Input, Container, Navbar, FormText } from 'reactstrap';

export default function RegisterForm(props){
    const { handleHome, handleSubmit, handleChange, values, errors, touched } = props
    return(
        <div>
            <Navbar>
                ToDoList App
                <Button color="primary" onClick={handleHome}>Home</Button>
            </Navbar>
            <Container>
                <h1><Badge color="primary">Register page</Badge></h1>
                <Form onSubmit={handleSubmit}>
                    <FormGroup>
                        <Label for="username">Username</Label>
                        <Input type="username" name="username" placeholder="Username" value={values.username} onChange={handleChange}/>
                        {errors.username && touched.username &&<FormText color="danger">{errors.username}</FormText>}
                    </FormGroup>
                    <FormGroup>
                        <Label for="password">Password</Label>
                        <Input type="password" name="password"placeholder="Password" value={values.password} onChange={handleChange}/>
                        {errors.password && touched.password &&<FormText color="danger">{errors.password}</FormText>}
                        {errors.confirmPassword && touched.confirmPassword && touched.password && <FormText color="danger">{errors.confirmPassword}</FormText>}
                    </FormGroup>
                    <FormGroup>
                        <Label for="confirmPassword">Confirm password</Label>
                        <Input type="password" name="confirmPassword" placeholder="Confirm password" value={values.confirmPassword} onChange={handleChange}/>
                        {errors.confirmPassword && touched.confirmPassword && <FormText color="danger">{errors.confirmPassword}</FormText>}
                    </FormGroup>
                    <FormGroup>
                        <Button type="submit" color="success">Register</Button>
                    </FormGroup>
                </Form>
            </Container>
        </div> 
    )
}