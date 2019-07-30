import React, {Component} from 'react';
import {Form, FormGroup, Label, Input, Badge, Button, Navbar, Container, NavItem} from 'reactstrap';
import axios from 'axios';
import {withRouter} from 'react-router-dom';
import { connect } from 'react-redux';
import { SetJwt } from '../actions';

function mapStateToProps(state) {
    return {
        jwt: state.jwt
    }
}

class Login extends Component{
    constructor(props){
        super(props);

        this.handleChange = this.handleChange.bind(this);
        this.submit = this.submit.bind(this);
        this.handleRegister = this.handleRegister.bind(this);
        this.handleHome = this.handleHome.bind(this);

    }

    handleChange(e){
        this.setState({
            [e.target.name]: e.target.value
        })
    }

    handleRegister(){
        this.props.history.push('/Register')
    }

    handleHome(){
        this.props.history.push('/')
    }

    submit(e){
        e.preventDefault();
        const apiCall = axios.create({
            baseURL: "https://localhost:5001",
        });
        apiCall.post('/user/login', {
            UserName: document.getElementById("username").value,
            Password: document.getElementById('password').value
        }).then(result => {
            this.props.dispatch(SetJwt(result.data.token))
            this.props.history.push('/');
        });
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
                    <Input type="username" name="username" id="username" placeholder="Username" onChange={e => this.handleChange(e)}/>
                </FormGroup>
                <FormGroup>
                    <Label for="password">Password</Label>
                    <Input type="password" name="password" id="password" placeholder="Password" onChange={e=> this.handleChange(e)}/>
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