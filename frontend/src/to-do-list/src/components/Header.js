import React, {Component} from 'react';
import {Navbar, Button, Nav, NavItem} from 'reactstrap';
import {withRouter} from 'react-router-dom';
import { connect } from 'react-redux';

function mapStateToProps(state) {
    return {
        jwt: state.jwt
    }
}

class Header extends Component{
    constructor(props){
        super(props)
        this.handleLogin = this.handleLogin.bind(this)
        this.handleLogout = this.handleLogout.bind(this)
        this.handleRegister = this.handleRegister.bind(this)
        this.handleHome = this.handleHome.bind(this)
    }

    handleLogin(){
        this.props.history.push('/Login')
    }

    handleLogout(){
        localStorage.removeItem('id_token')
        this.props.history.push('/')
    }

    handleRegister(){
        this.props.history.push('/Register')
    }

    handleHome(){
        this.props.history.push('/')
    }
    render(){
        if(this.props.jwt === ''){
        return(
            <div>
            <Navbar>ToDoList App
            <Nav className="ml-auto" navbar>
              <NavItem>
                <Button color='primary' onClick={this.handleLogin}>Log in</Button> {' '}
                <Button color='success' onClick={this.handleRegister}>Register</Button>
              </NavItem>
            </Nav>
            </Navbar>
            </div>
        )
        }
        
        return(
            <div>
                <Navbar>
                    ToDoList App
                <Nav className="ml-auto">
                    <NavItem>
                        <Button color='primary' onClick={this.handleHome}>Home</Button>{' '}
                        <Button color='primary' onClick={this.handleLogout}>Log out</Button>
                    </NavItem>
                </Nav>
                </Navbar>
            </div>
        )
    }
}

export default connect(mapStateToProps)(withRouter(Header))
