import React, {Component} from 'react';
import {Form, FormGroup, Label, Input, Badge, Button, Navbar, Container, NavItem} from 'reactstrap';
import axios from 'axios';
import {withRouter} from 'react-router-dom';
import { connect } from 'react-redux';
import {LoginUser } from '../actions';

function mapStateToProps(state) {
    return {
        jwt: state.jwt
    }
}

class Login extends Component{
    constructor(props){
        super(props);

        this.submit = this.submit.bind(this);
        this.handleRegister = this.handleRegister.bind(this);
        this.handleHome = this.handleHome.bind(this);

    }

    handleRegister(){
        this.props.history.push('/Register')
    }

    handleHome(){
        this.props.history.push('/')
    }

    submit(e){
        e.preventDefault();
        const user = {
            username: document.getElementById("username").value,
            password: document.getElementById("password").value
        }
        this.props.dispatch(LoginUser(user))
        this.props.history.push("/")
    }

    render(){
        return(
        <div>
        <Navbar>
            ToDoList App
                <Button color='primary' onClick={this.handleHome}>Home</Button>
        </Navbar>
        <Container>
            <h1><Badge color="primary">Login page</Badge></h1>
            <Form onSubmit={e => this.submit(e)}>
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
            <Button color="success" onClick={this.handleRegister}>Register</Button>
        </Container>
        </div>
        )
    }
}

export default connect(mapStateToProps)(withRouter(Login))