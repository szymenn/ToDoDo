import { withFormik } from 'formik';
import { LoginUser } from '../actions';
import * as Yup from 'yup';
import LoginForm from './LoginForm';

const EnhancedLoginForm = withFormik({
    mapPropsToValues() {
        return {
            username: '',
            password: ''
        }
    },
    handleSubmit(values, { props }) {
        const user = {
            username: values.username,
            password: values.password
        }
        props.dispatch(LoginUser(user, props.redirect))
    },
    validationSchema: Yup.object().shape({
        username: Yup.string().required(),
        password: Yup.string().required()
    })
})(LoginForm)

export default EnhancedLoginForm