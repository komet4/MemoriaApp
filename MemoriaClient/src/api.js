import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7044',
    timeout: 5000
});

export default api;
