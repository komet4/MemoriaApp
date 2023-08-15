import jwt_decode from 'jwt-decode';

const TOKEN_KEY = 'access_token';
const REFRESH_TOKEN_KEY = 'refresh_token';

export const setAccessToken = (token) => {
  localStorage.setItem(TOKEN_KEY, token);
};

export const getAccessToken = () => {
  return localStorage.getItem(TOKEN_KEY);
};

export const removeAccessToken = () => {
  localStorage.removeItem(TOKEN_KEY);
};

export const setRefreshToken = (token) => {
  localStorage.setItem(REFRESH_TOKEN_KEY, token);
};

export const getRefreshToken = () => {
  return localStorage.getItem(REFRESH_TOKEN_KEY);
};

export const removeRefreshToken = () => {
  localStorage.removeItem(REFRESH_TOKEN_KEY);
};

export const isTokenExpired = (token) => {
  if (!token) return true;

  try {
    const decoded = jwt_decode(token);
    return Date.now() >= decoded.exp * 1000;
  } catch (error) {
    return true;
  }
};
