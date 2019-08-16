import React from 'react';
import Login from '../containers/Login';
import {BrowserRouter, Switch, Route, withRouter} from 'react-router-dom';
import Register from '../containers/Register';
import Add from '../containers/Add';
import Home from './Home';
import Edit from '../containers/Edit';
import ErrorHandler from '../containers/ErrorHandler';

function App(props){
    return(
        <BrowserRouter>
            <Switch>
                <Route path="/Login" component={Login}/>
                <Route path='/Register' component={Register}/>
                <Route path='/Add' component={Add}/>
                <Route path='/Edit' component={Edit}/>
                <Route path='/Error' component={ErrorHandler}/>
                <Route path='/' component={Home}/>
            </Switch>
        </BrowserRouter>
    )
}

export default withRouter(App)


