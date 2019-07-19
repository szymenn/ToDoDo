import React, {Component} from 'react';
import axios from 'axios';
import {withRouter} from 'react-router-dom';
import TaskForm from './TaskForm';

class Add extends Component {
    constructor(props){
        super(props)
        this.state = {
            task: undefined,
            date: undefined
        }
        this.handleChange = this.handleChange.bind(this)
        this.handleSubmit = this.handleSubmit.bind(this)
    }
    handleChange(e){
        this.setState({
            [e.target.name]: e.target.value
        })
    }

    componentDidMount(){
        if(!localStorage.getItem('id_token')){
            this.props.history.push('/Login')
        }
    }

    handleSubmit(e){ 
        e.preventDefault()
        const headers = {
            Authorization: `Bearer ${localStorage.getItem('id_token')}`
        }
        
        const inputDate = document.getElementById('date').value
        const date = new Date(inputDate).toJSON()
        const task = document.getElementById('task').value
        
        const data = {
            task: task,
            date: date
        }
        axios.post('https://localhost:5001/todo', data, {headers: headers})
        this.props.history.push('/')
    }

    render(){
        return(
           <TaskForm handleSubmit={this.handleSubmit} handleChange={this.handleChange} date={this.state.date} task={this.state.task}/>
        )
    }
}

export default withRouter(Add)