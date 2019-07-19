import React, {Component} from 'react'
import {Table, Button, Jumbotron} from 'reactstrap'
import axios from 'axios';
import {withRouter} from 'react-router-dom';
import ResultTodos from './ResultTodos';
import ResultNotAuth from './ResultNotAuth';

class ToDoTable extends Component{
    constructor(props){
        super(props)
        this.state ={
            user: undefined,
            todos: []
        }
        this.handleLogIn = this.handleLogIn.bind(this)
        this.handleRegister = this.handleRegister.bind(this)
        this.handleAdd = this.handleAdd.bind(this)
        this.handleDelete = this.handleDelete.bind(this)
    }
    
    handleLogIn() {
        this.props.history.push('/Login')
    }

    handleRegister(){
        this.props.history.push('/Register')
    }

    handleDelete(e){
        const jwt = localStorage.getItem('id_token')
        axios.delete(`https://localhost:5001/todo/${e}`, 
        {headers: {Authorization: `Bearer ${jwt}`}})
        .then(result => this.setState({
            todos: result.data
        }))
        this.props.history.push('/')
    }

    handleAdd(){
        this.props.history.push('/Add')
    }

    componentDidMount(){
        const jwt = localStorage.getItem('id_token')
        if(jwt){
        const apiCall = axios.create({
            baseURL: 'https://localhost:5001'
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
    }

    render() {
        if(localStorage.getItem('id_token')){
        return(
            <ResultTodos todos={this.state.todos} handleDelete={this.handleDelete} handleAdd={this.handleAdd}/>
        )
        }
        return(
           <ResultNotAuth handleLogIn={this.handleLogIn} handleRegister={this.handleRegister}/>
        )
    }
}
export default withRouter(ToDoTable)