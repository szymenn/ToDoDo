import { withFormik } from 'formik';
import * as Yup from 'yup';
import { UpdateToDoRequest } from '../actions';
import TaskForm from '../components/TaskForm';

const EnhancedEditForm = withFormik({
    mapPropsToValues({task, date}) {
        return {
            task: task || '',
            date: date || null
        }
    },
    handleSubmit(values, { props }) {
        const date = new Date(values.date).toJSON()
    
        const todo = {
            task: values.task,
            date: date
        }

        props.dispatch(UpdateToDoRequest(todo, props.id, props.redirect))
        props.redirect('/')
    },
    validationSchema: Yup.object().shape({
        task: Yup.string(),
        date: Yup.date()
    })
})(TaskForm)    

export default EnhancedEditForm