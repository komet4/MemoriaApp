import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { setAccessToken } from '../auth'; // Импортируйте функции для работы с токенами

const LoginPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('/api/auth/login', {
        username,
        password,
      });

      if (response.status === 200) {
        const { token } = response.data;
        setAccessToken(token); // Сохраните access token
        navigate('/dashboard'); // Перенаправление после успешной аутентификации
      }
    } catch (error) {
      console.error('Login failed', error);
      // Обработка ошибки аутентификации
    }
  };

  return (
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card">
            <div className="card-body">
              <h2 className="card-title text-center">Login Page</h2>
              <form onSubmit={handleLogin}>
                <div className="mb-3">
                  <label htmlFor="username" className="form-label">Username:</label>
                  <input type="text" className="form-control" id="username" value={username} onChange={(e) => setUsername(e.target.value)} />
                </div>
                <div className="mb-3">
                  <label htmlFor="password" className="form-label">Password:</label>
                  <input type="password" className="form-control" id="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <button type="submit" className="btn btn-primary w-100">Login</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
