import axios from "axios";
import type { Employee } from "../types/employee";

const BASE_URL = "https://localhost:7204/api/employees";

export const getEmployees = (params?: {
  pageNumber?: number;
  pageSize?: number;
  search?: string;
}) => axios.get<Employee[]>(BASE_URL, { params });

export const getEmployee = (id: number) =>
  axios.get<Employee>(`${BASE_URL}/${id}`);

export const addEmployee = (data: Omit<Employee, "id">) =>
  axios.post<Employee>(BASE_URL, data);

export const updateEmployee = (id: number, data: Partial<Employee>) =>
  axios.put<Employee>(`${BASE_URL}/${id}`, data);

export const deleteEmployee = (id: number) =>
  axios.delete(`${BASE_URL}/${id}`);
