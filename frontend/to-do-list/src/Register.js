import React, {Component} from 'react';
import {FormGroup, Form, Button, Badge, Label, Input, Container, Navbar} from 'reactstrap';
import {withRouter} from 'react-router-dom';
import axios from 'axios';
class Register extends Component{
    constructor(props){
        super(props);

        this.state={
            username: '',
            password: '',
            confirm_password: ''
        }

        this.handleChange = this.handleChange.bind(this);
        this.submit = this.submit.bind(this);
        this.handleHome = this.handleHome.bind(this);
    }

    handleChange(e){
        this.setState({
            [e.target.name]: e.target.value
        })
    }

    handleHome(){
        this.props.history.push('/')
    }

    submit(e){
        e.preventDefault();
        const apiCall = axios.create({
            baseURL: "https://localhost:44364",
        });
        apiCall.post('/user/register', {
            UserName: this.state.username,
            Password: this.state.password,
            ConfirmPassword: this.state.confirm_password
        }).then(result => {
            localStorage.setItem('id_token', result.data.token)
            this.props.history.push('/');
    
    });
    }
    check () {
        if (document.getElementById('password').value === document.getElementById('confirm_password').value) {
            document.getElementById('message').style.color = 'green';
            document.getElementById('message').innerHTML = 'matching';
        } else {
            document.getElementById('message').style.color = 'red';
            document.getElementById('message').innerHTML = 'not matching';
        }
    }

    render(){
        return(
        <div>
            <Navbar>
                ToDoList App
                <Button color="primary" onClick={this.handleHome}>Home</Button>
            </Navbar>
            <Container>
            <h1><Badge color="primary">Register page</Badge></h1>
            <Form onSubmit={e => this.submit(e)}>
                <FormGroup>
                    <Label for="username">Username</Label>
                    <Input type="username" name="username" id="username" placeholder="Username" onChange={e => this.handleChange(e)} value={this.state.username}/>
                </FormGroup>
                <FormGroup>
                    <Label for="password">Password</Label>
                    <Input type="password" name="password" id="password" placeholder="Password" onChange={e=> this.handleChange(e)} value={this.state.password} onKeyUp={this.check}/>
                </FormGroup>
                <FormGroup>
                    <Label for="confirm_password">Confirm password</Label>
                    <Input type="password" name="confirm_password" id="confirm_password" placeholder="Confirm password" onChange={e=> this.handleChange(e)} value={this.state.confirm_password} onKeyUp={this.check}/>
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
}

export default withRouter(Register)