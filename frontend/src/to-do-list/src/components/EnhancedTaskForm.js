import { withFormik } from 'formik';
import * as Yup from 'yup';
import { AddToDo } from '../actions';
import TaskForm from './TaskForm';

const EnhancedTaskForm = withFormik({
    mapPropsToValues({task, date}) {
        return {
            task: task || '',
            date: date || ''
        }
    },
    handleSubmit(values, { props }) {
        const date = new Date(values.date).toJSON()
    
        const todo = {
            task: values.task,
            date: date
        }

        props.dispatch(AddToDo(todo))
        props.redirect('/')
    },
    validationSchema: Yup.object().shape({
        task: Yup.string().required(),
        date: Yup.date().required()
    })
})(TaskForm)    

export default EnhancedTaskForm