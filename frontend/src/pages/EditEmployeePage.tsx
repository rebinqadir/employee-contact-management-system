import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import EmployeeForm from "../components/EmployeeForm";
import { getEmployee, updateEmployee } from "../api/employeeApi";
import type { Employee } from "../types/employee";
import { notifySuccess, notifyError } from "../components/Toast";
import { useEmployeeContext } from "../contexts/EmployeeContext";
import axios from "axios";

export default function EditEmployeePage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { fetchEmployees } = useEmployeeContext();
  const [emp, setEmp] = useState<Employee>();

  useEffect(() => {
    if (id) {
      getEmployee(+id).then((res) => {
      const raw = res.data as any;

      setEmp({
        ...raw,
        companyId: raw.companyId ?? raw.companyID,   // normalize casing
      });
    });
  }
  }, [id]);

  async function handleUpdate(data: any) {
    try {
      await updateEmployee(Number(id), data);

      await fetchEmployees();

      notifySuccess("Updated successfully");
      navigate("/");
    } catch (err: any) {
      if (axios.isAxiosError(err)) {
        const msg =
          err.response?.data?.message ||
          JSON.stringify(err.response?.data?.errors) ||
          "Error updating employee";

        notifyError(msg);
      } else {
        notifyError("Unexpected error");
      }
    }
  }

  return (
    <div className="container py-4">
      <h2>Edit Employee</h2>
      {emp && (
        <EmployeeForm
          defaultValues={emp}
          onSubmit={handleUpdate}
          mode="edit"
        />
      )}
    </div>
  );
}
