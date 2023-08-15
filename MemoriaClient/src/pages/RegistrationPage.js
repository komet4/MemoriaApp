import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const RegistrationPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleRegistration = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('/api/auth/register', {
        username,
        password,
      });

      if (response.status === 201) {
        navigate('/login'); // Перенаправление на страницу входа после успешной регистрации
      }
    } catch (error) {
      console.error('Registration failed', error);
      // Обработка ошибки регистрации
    }
  };

  return (
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card">
            <div className="card-body">
              <h2 className="card-title text-center">Registration Page</h2>
              <form onSubmit={handleRegistration}>
                <div className="mb-3">
                  <label htmlFor="username" className="form-label">Username:</label>
                  <input type="text" className="form-control" id="username" value={username} onChange={(e) => setUsername(e.target.value)} />
                </div>
                <div className="mb-3">
                  <label htmlFor="password" className="form-label">Password:</label>
                  <input type="password" className="form-control" id="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <button type="submit" className="btn btn-primary w-100">Register</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RegistrationPage;
