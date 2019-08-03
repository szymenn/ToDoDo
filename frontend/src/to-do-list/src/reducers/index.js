import todos from './todos';
import ids from './ids';
import { combineReducers } from 'redux';

export default combineReducers({
    ids,
    todos
})