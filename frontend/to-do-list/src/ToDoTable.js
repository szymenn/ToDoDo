import React, {Component} from 'react'
import {Table, Button, Jumbotron} from 'reactstrap'
import axios from 'axios';
import {withRouter} from 'react-router-dom';

 class ToDoTable extends Component{
    constructor(props){
        super(props)
        this.state ={
            user: undefined,
            todos: []
        }
        this.handleLogIn = this.handleLogIn.bind(this)
        this.handleRegister = this.handleRegister.bind(this)
    }
    
    handleLogIn() {
        this.props.history.push('/Login')
    }

    handleRegister(){
        this.props.history.push('/Register')
    }

    handleDelete(e){
        const jwt = localStorage.getItem('id_token')
        axios.delete(`https://localhost:44364/todo/${e}`, 
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
    }

    render() {
        const {todos} = this.state
        const resultTodos = todos.map((entry, index) => {
            return(
                <tbody key={index}>
                    <tr>
                        <td >{entry.task}</td>
                        <td >{entry.date}</td>
                        <th>
                            <Button color="secondary">Edit</Button>
                        </th>
                        <th>
                            <Button color="danger" onClick={(e)=>{this.handleDelete(entry.id)}}>Delete</Button>
                        </th>
                    </tr>
                </tbody>
            )
        })
        if(localStorage.getItem('id_token'))
        return(
            <Table>
                <thead>
                    <tr>
                        <th>Task</th>
                        <th>Date</th>
                        <th>
                            <Button color="success">Add new Task</Button>
                        </th>
                    </tr>
                </thead>
                {resultTodos}
            </Table>
        )

        return(
            <Jumbotron>
                <h1 className="display-3">Welcome to ToDoList App</h1>
                <p className="lead">Simple ToDoList app I made in order to learn basics of frontend web development</p>
                <hr className="my-2" />
                <p className="lead">               
                    <Button color="primary" onClick={this.handleLogIn}>Log in</Button>
                </p>
                <p className="lead">               
                    <Button color="success" onClick={this.handleRegister}>Register</Button>
                </p>
            </Jumbotron>
        )
    }
}
export default withRouter(ToDoTable)