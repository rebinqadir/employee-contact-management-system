import { useEmployeeContext } from "../contexts/EmployeeContext";
import EmployeeCard from "../components/EmployeeCard";
import { Link } from "react-router-dom";
import PaginationComponent from "../components/Pagination";

export default function EmployeesPage() {
  const {
    employees,
    pageNumber,
    pageSize,
    totalCount,
    searchInput,
    setSearchInput,
    setPageNumber,
  } = useEmployeeContext();

  const totalPages = Math.ceil(totalCount / pageSize);

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchInput(e.target.value);
  };

  return (
    <div className="d-flex justify-content-center w-100">
      <div className="page-container py-4">

        {/* Header + Add */}
        <div className="d-flex justify-content-between align-items-center mb-4">
          <h2>Employees</h2>
          <Link className="btn btn-primary" to="/employees/add">
            Add Employee
          </Link>
        </div>

        {/* Debounced Search */}
        <div className="mb-4">
          <input
            type="text"
            placeholder="Search employee..."
            className="form-control"
            value={searchInput}
            onChange={handleSearch}
          />
        </div>

        {/* Cards Grid */}
        <div className="row g-4">
          {employees.map((emp) => (
            <div key={emp.id} className="col-12 col-md-6 col-lg-4">
              <EmployeeCard employee={emp} />
            </div>
          ))}
        </div>

        {/* No Results */}
        {employees.length === 0 && (
          <p className="text-center mt-4 text-muted">
            No employees found.
          </p>
        )}

        {/* Pagination */}
        {totalPages > 1 && (
          <div className="mt-4">
            <PaginationComponent
              page={pageNumber}
              totalPages={totalPages}
              onChange={setPageNumber}
            />
          </div>
        )}
      </div>
    </div>
  );
}
