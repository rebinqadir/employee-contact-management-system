import EmployeeForm from "../components/EmployeeForm";
import { addEmployee } from "../api/employeeApi";
import { notifySuccess, notifyError } from "../components/Toast";
import { useNavigate } from "react-router-dom";
import { useEmployeeContext } from "../contexts/EmployeeContext";
import axios from "axios";

export default function AddEmployeePage() {
  const navigate = useNavigate();
  const { fetchEmployees } = useEmployeeContext();

  async function handleAdd(data: any) {
    try {
      await addEmployee(data);

      await fetchEmployees();

      notifySuccess("Employee added.");
      navigate("/");
    } catch (err: any) {
      if (axios.isAxiosError(err)) {
        const msg =
          err.response?.data?.message ||
          JSON.stringify(err.response?.data?.errors) ||
          "Error adding employee";

        notifyError(msg);
      } else {
        notifyError("Unexpected error");
      }
    }
  }

  return (
    <div className="container py-4">
      <h2>Add Employee</h2>
      <EmployeeForm onSubmit={handleAdd} mode="add" />
    </div>
  );
}
