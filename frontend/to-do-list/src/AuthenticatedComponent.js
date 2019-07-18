import React, {Component} from 'react';
import {Form, FormGroup, Input, Label, Badge} from 'reactstrap'
import Header from './Header';
import axios from 'axios';
import {withRouter} from 'react-router-dom';
import API from './utils/API';
 

class AuthenticatedComponent extends Component{
    constructor(props){
        super(props)
        this.state={
            user: undefined,
            todos: []
        }

    }
    componentDidMount(){
        const jwt = localStorage.getItem('id_token')
        if(!jwt){
            this.props.history.push('/Login')
        }
        const apiCall = axios.create({
            baseURL: 'https://localhost:44364'
        });

        apiCall.get('/todo', {headers: {Authorization: `Bearer ${jwt}`}})
        .then(result => this.setState({
            todos: result.data
        }))
        
        apiCall.get('/user', {headers: {Authorization: `Bearer ${jwt}`}})
        .then(result => this.setState({
            user: result.data.userName
        }))
    }

    render(){
        const {todos} = this.state
        const {user} = this.state
        const resultTodos = todos.map((entry, index) => {
            return <li key={index}>{entry.task} {entry.date}</li>
          })
        
        return(
            <ul>
                {resultTodos}
                <li>{user}</li>
            </ul>
        )
    }
}

export default withRouter(AuthenticatedComponent)