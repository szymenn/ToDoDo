import { withFormik } from 'formik';
import * as Yup from 'yup';
import { AddToDo } from '../actions';
import TaskForm from './TaskForm';

const EnhancedTaskForm = withFormik({
    mapPropsToValues() {
        return {
            task: '',
            date: ''
        }
    },
    handleSubmit(values, { props }) {
        const date = new Date(values.date).toJSON()
    
        const todo = {
            task: values.task,
            date: date
        }

        props.dispatch(AddToDo(todo, props.redirect))
        props.redirect('/')
    },
    validationSchema: Yup.object().shape({
        task: Yup.string().required("Task is required"),
        date: Yup.date().required("Date is required")
    })
})(TaskForm)    

export default EnhancedTaskForm