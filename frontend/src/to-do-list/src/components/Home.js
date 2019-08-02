import ToDoTable from './ToDoTable';
import Header from './Header';
import React from 'react';
import {withRouter} from 'react-router-dom';

function Home(props){
    return(
        <div>
            <Header/>
            <ToDoTable/>
        </div>
    )
}

export default withRouter(Home)