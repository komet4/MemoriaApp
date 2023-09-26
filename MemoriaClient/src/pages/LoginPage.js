import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { setAccessToken } from '../auth'; // Импортируйте функции для работы с токенами
import { Container, Form, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import './LoginPage.css';

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
    <Container className="text-center">
      <main className="form-signin">
        <Form onSubmit={handleLogin}>
          <h1 className="h3 mb-3 fw-normal">Please sign in</h1>
          <Form.Floating>
            <Form.Control
              type="text"
              id="floatingInput"
              placeholder="Your login"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            <label htmlFor="floatingInput">Login</label>
          </Form.Floating>
          <Form.Floating>
            <Form.Control
              type="password"
              id="floatingPassword"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <label htmlFor="floatingPassword">Password</label>
          </Form.Floating>

          <div className="checkbox mb-3">
            <label>
              <input type="checkbox" value="remember-me" /> Remember me
            </label>
          </div>
          <Button className="w-100 btn btn-lg btn-primary" type="submit">
            Sign in
          </Button>
          <p className="mt-3">
            Don't have an account? <Link to="/register">Sign up</Link>
          </p>
        </Form>
      </main>
    </Container>
  );
};

export default LoginPage;
