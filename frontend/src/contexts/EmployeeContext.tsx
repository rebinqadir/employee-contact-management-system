import {
  createContext,
  useContext,
  useEffect,
  useState
} from "react";

import type{
  ReactNode
} from "react";



import {
  getEmployees,
  deleteEmployee as apiDeleteEmployee,
} from "../api/employeeApi";

import type { Employee } from "../types/employee";

interface EmployeeContextType {
  employees: Employee[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  searchInput: string;
  setSearchInput: (value: string) => void;
  setPageNumber: (value: number) => void;
  deleteEmployee: (id: number) => Promise<void>;
  fetchEmployees: () => Promise<void>;
}

const EmployeeContext = createContext<EmployeeContextType>({
  employees: [],
  pageNumber: 1,
  pageSize: 6,
  totalCount: 0,
  searchInput: "",
  setSearchInput: () => {},
  setPageNumber: () => {},
  deleteEmployee: async () => {},
  fetchEmployees: async () => {},
});

export function EmployeeProvider({ children }: { children: ReactNode }) {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [pageNumber, setPageNumber] = useState(1);
  const pageSize = 6; // 3 per row × 2 rows
  const [totalCount, setTotalCount] = useState(0);

  const [searchInput, setSearchInput] = useState("");
  const [search, setSearch] = useState("");

  // Fetch paginated employees
  const fetchEmployees = async () => {
    const res = await getEmployees({
      pageNumber,
      pageSize,
      search,
    });

    setEmployees(res.data);
    const total = Number(res.headers["x-total-count"] ?? 0);
    setTotalCount(total);
  };

  // Delete wrapper
  const deleteEmployee = async (id: number) => {
    await apiDeleteEmployee(id);
    await fetchEmployees();
  };

  // Debounce input → update search
  useEffect(() => {
    const timeout = setTimeout(() => {
      setSearch(searchInput);
      setPageNumber(1);
    }, 400);

    return () => clearTimeout(timeout);
  }, [searchInput]);

  // Fetch when page or search changes
  useEffect(() => {
    fetchEmployees();
  }, [pageNumber, search]);

  return (
    <EmployeeContext.Provider
      value={{
        employees,
        pageNumber,
        pageSize,
        totalCount,
        searchInput,
        setSearchInput,
        setPageNumber,
        deleteEmployee,
        fetchEmployees,
      }}
    >
      {children}
    </EmployeeContext.Provider>
  );
}

export const useEmployeeContext = () => useContext(EmployeeContext);
