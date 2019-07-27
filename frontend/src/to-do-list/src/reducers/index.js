import todos from './todos';
import jwt from './jwt';
import { combineReducers } from 'redux';

export default combineReducers({
    todos,
    jwt
})