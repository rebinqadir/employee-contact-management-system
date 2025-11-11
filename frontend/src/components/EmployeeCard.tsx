import { Button, Card } from "react-bootstrap";
import type { Employee } from "../types/employee";
import { useEmployeeContext } from "../contexts/EmployeeContext";
import { Link } from "react-router-dom";
import { notifySuccess } from "./Toast";

import { useGravatar } from "../hooks/useGravatar";         // IMPORTg


interface Props {
  employee: Employee;
}

export default function EmployeeCard({ employee }: Props) {
  const { deleteEmployee } = useEmployeeContext();

  const avatarUrl = useGravatar(employee.email);              // CALL

  const handleDelete = async () => {
    if (confirm("Are you sure?")) {
      await deleteEmployee(employee.id);
      notifySuccess("Employee deleted");
    }
  };

  return (
    <Card className="p-3 h-100 shadow-sm rounded-3">

      {/* DISPLAY AVATAR */}
      <img
        src={avatarUrl}
        alt={employee.name}
        className="rounded-circle mb-3"
        width={80}
        height={80}
        style={{ objectFit: "cover" }}
      />

      <h5 className="mb-1">{employee.name}</h5>
      <p className="mb-1">{employee.email}</p>
      {employee.phone && <p className="mb-1">{employee.phone}</p>}
      {employee.jobTitle && <p className="mb-1">{employee.jobTitle}</p>}
      {employee.companyName && (
        <p className="mb-1">Company: {employee.companyName}</p>
      )}

      <p className="fw-semibold mb-3">
        Status:{" "}
        {employee.isActive ? (
          <span className="text-success">Active ✅</span>
        ) : (
          <span className="text-danger">Inactive ❌</span>
        )}
      </p>

      <div className="mt-auto d-flex gap-2">
        <Link
          to={`/employees/edit/${employee.id}`}
          className="btn btn-primary w-50"
        >
          Edit
        </Link>

        <Button variant="danger" className="w-50" onClick={handleDelete}>
          Delete
        </Button>
      </div>
    </Card>
  );
}
