import todos from './todos';
import ids from './ids';
import error from './error';
import { combineReducers } from 'redux';

export default combineReducers({
    ids,
    todos,
    error
})