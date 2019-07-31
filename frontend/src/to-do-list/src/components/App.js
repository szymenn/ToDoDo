import React, {Component} from 'react';
import Login from './Login';
import {BrowserRouter, Switch, Route, withRouter} from 'react-router-dom';
import AppInner from './AppInner';
import Register from './Register';
import Add from './Add';

class App extends Component{
    render(){
        return(
            <BrowserRouter>
                <Switch>
                    <Route path="/Login" component={Login}/>
                    <Route path='/Register' component={Register}/>
                    <Route path='/Add' component={Add}/>
                    <Route path='/' component={AppInner}/>
                </Switch>
            </BrowserRouter>
        )
    }
}

export default withRouter(App)


