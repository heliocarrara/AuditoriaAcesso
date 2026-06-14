import axios from 'axios';

export const api = axios.create({
    baseURL: 'https://localhost:7013/api'
});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('@AuditoriaAcesso:token');

    if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, (error) => {
    return Promise.reject(error);
});