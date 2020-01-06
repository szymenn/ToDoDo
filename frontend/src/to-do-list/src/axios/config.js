import axios from 'axios';
import { JWT_ID, REFRESH_ID } from '../constants/jwt';

const apiUrl = 'http://localhost:8000'
const axiosInstance = axios.create()

axiosInstance.interceptors.response.use((response) => {
    return response
}, error => {
    if(error.response.status === 401){
        const refreshToken = localStorage.getItem(REFRESH_ID)
        const headers = {
            'Content-Type':'application/json'
        }
        return axios.post(`${apiUrl}/user/tokens/refresh`, JSON.stringify(refreshToken), {headers: headers})
        .then(result => {
            localStorage.setItem(JWT_ID, result.data.tokens.accessToken)
            localStorage.setItem(REFRESH_ID, result.data.tokens.refreshToken)
            error.config.headers.Authorization = `Bearer ${localStorage.getItem(JWT_ID)}`
            return axios.request(error.config)
        })
    }
    return Promise.reject(error)
})

export default axiosInstance