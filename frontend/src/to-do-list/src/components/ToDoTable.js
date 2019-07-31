import React, { Component } from 'react'
import { Table, Button, Jumbotron } from 'reactstrap'
import axios from 'axios';
import { withRouter } from 'react-router-dom';
import ResultTodos from './ResultTodos';
import ResultNotAuth from './ResultNotAuth';
import { connect } from 'react-redux';
import {UpdateToDos} from '../actions/index';
import { JWT_ID } from '../constants/jwt';

function mapStateToProps(state) {
    return {
    todos: state.todos,
    jwt: state.jwt
    }
}

class ToDoTable extends Component{
    constructor(props){
        super(props)
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
        axios.delete(`https://localhost:5001/todos/${e}`, 
        {headers: {Authorization: `Bearer ${jwt}`}})
        .then(result => {
            this.props.dispatch(UpdateToDos(result.data))
        })

        this.props.history.push('/')
    }

    handleAdd(){
        this.props.history.push('/Add')
    }

    componentDidMount(){
        const jwt = localStorage.getItem('id_token')
        axios.get('https://localhost:5001/todos', {headers: {Authorization: `Bearer ${jwt}`}})
        .then(result => this.props.dispatch(UpdateToDos(result.data)))
    }

    render() {
        if(localStorage.getItem(JWT_ID) !== null){
        return(
            <ResultTodos todos={this.props.todos} handleDelete={this.handleDelete} handleAdd={this.handleAdd}/>
        )
        }
        return(
           <ResultNotAuth handleLogIn={this.handleLogIn} handleRegister={this.handleRegister}/>
        )
    }
}


export default connect(mapStateToProps)(withRouter(ToDoTable))