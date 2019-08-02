import ToDoTable from './ToDoTable';
import Header from './Header';
import React, {Component} from 'react';
import {withRouter} from 'react-router-dom';

class AppInner extends Component{
    render(){
        return(
        <div>
            <Header/>
            <ToDoTable/>
        </div>
        )
    }
}

export default withRouter(AppInner)