import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { Container, Form, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import './RegistrationPage.css';

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
    <Container className="text-center">
      <main className="form-signup">
        <Form onSubmit={handleRegistration}>
          <h1 className="h3 mb-3 fw-normal">Please sign up</h1>
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
          <Button type="submit" className="w-100 btn btn-lg btn-primary">Sign up</Button>
          <p className="mt-3">
            Do you have an account? <Link to="/login">Sign in</Link>
          </p>
        </Form>
      </main>
    </Container>
  );
};

export default RegistrationPage;
