import React from 'react';
import ReactDOM from 'react-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './components/App';
import {BrowserRouter} from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './store';
const rootElement = document.getElementById('root');
ReactDOM.render(
    <BrowserRouter>
        <App />
    </BrowserRouter>, rootElement);