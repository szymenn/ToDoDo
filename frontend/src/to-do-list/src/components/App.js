import React from 'react';
import Login from './Login';
import {BrowserRouter, Switch, Route, withRouter} from 'react-router-dom';
import Register from './Register';
import Add from './Add';
import Home from './Home';
import Edit from './Edit';
import ErrorHandler from './ErrorHandler';

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


