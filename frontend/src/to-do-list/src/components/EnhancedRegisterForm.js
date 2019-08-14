import { withFormik } from 'formik';
import { RegisterUser } from '../actions';
import * as Yup from 'yup';
import RegisterForm from './RegisterForm';

const EnhancedRegisterForm = withFormik({
    mapPropsToValues() {
        return {
            username: '',
            password: '',
            confirmPassword: ''
        }
    },
    handleSubmit(values, { props }) {
        const user = {
            username: values.username,
            password: values.password, 
            confirmPassword: values.confirmPassword
        }
        props.dispatch(RegisterUser(user, props.redirect))
    },
    validationSchema: Yup.object().shape({
        username: Yup.string().required(),
        password: Yup.string().required(),
        confirmPassword: Yup.string().required()
    })
})(RegisterForm)

export default EnhancedRegisterForm