import axios from "axios";

const API_URL = "http://127.0.0.1:5208/api";

const getToken = () => localStorage.getItem("token");

const api = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.request.use((config) => {
  const token = getToken();
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Tenant services
export const registerTenant = (data) => api.post("/tenants/register", data);
export const loginTenant = (data) => api.post("/tenants/login", data);
export const getAllTenants = () => api.get("/tenants");

// Employee services
export const getEmployees = () => api.get("/employees");
export const createEmployee = (data) => api.post("/employees", data);