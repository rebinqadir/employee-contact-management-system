import { useEmployeeContext } from "../contexts/EmployeeContext";

export function useEmployees() {
  return useEmployeeContext();
}
