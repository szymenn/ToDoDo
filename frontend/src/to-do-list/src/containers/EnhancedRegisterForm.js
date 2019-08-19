import { withFormik } from 'formik';
import { RegisterUser } from '../actions';
import * as Yup from 'yup';
import RegisterForm from '../components/RegisterForm';

const EnhancedRegisterForm = withFormik({
    mapPropsToValues() {
        return {
            username: '',
            email: '',
            password: '',
            confirmPassword: ''
        }
    },
    handleSubmit(values, { props }) {
        const user = {
            username: values.username,
            email: values.email,
            password: values.password, 
            confirmPassword: values.confirmPassword
        }
        props.dispatch(RegisterUser(user, props.redirect))
    },
    validationSchema: Yup.object().shape({
        username: Yup.string().required("Username is required"),
        email: Yup.string().email().required(),
        password: Yup.string().required("Password is required"),
        confirmPassword: Yup.string().required("Password confirmation is required").oneOf([Yup.ref('password'), null], "Passwords do not match")
    })
})(RegisterForm)

export default EnhancedRegisterForm