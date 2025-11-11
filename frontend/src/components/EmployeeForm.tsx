import { useForm } from "react-hook-form";
import type { Employee } from "../types/employee";
import { useCompanies } from "../hooks/useCompanies";
import { Button, Form } from "react-bootstrap";
import { useEffect } from "react";

interface Props {
  defaultValues?: Partial<Employee>;
  onSubmit: (data: Omit<Employee, "id">) => void;
  mode?: "add" | "edit";
}

export default function EmployeeForm({ defaultValues, onSubmit, mode = "add" }: Props) {
  const { companies } = useCompanies();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<Omit<Employee, "id">>({
    defaultValues: defaultValues as Omit<Employee, "id">,
  });

  useEffect(() => {
    if (defaultValues) {
      reset(defaultValues as Omit<Employee, "id">);
    }
  }, [defaultValues, reset]);

  const handleFormSubmit = (data: Omit<Employee, "id">) => {
    if (mode === "add") {
      data.isActive = true;
    }
    onSubmit(data);
  };

  return (
    <Form onSubmit={handleSubmit(handleFormSubmit)} className="p-3">

      {/* Name */}
      <Form.Group className="mb-3">
        <Form.Label>Name</Form.Label>
        <Form.Control
          {...register("name", { required: "Name is required" })}
        />
        {errors.name && <small className="text-danger">{errors.name.message}</small>}
      </Form.Group>

      {/* Email */}
      <Form.Group className="mb-3">
        <Form.Label>Email</Form.Label>
        <Form.Control
          type="email"
          {...register("email", {
            required: "Email is required",
            pattern: {
              value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
              message: "Invalid email format",
            },
          })}
        />
        {errors.email && <small className="text-danger">{errors.email.message}</small>}
      </Form.Group>

      {/* Phone */}
      <Form.Group className="mb-3">
        <Form.Label>Phone</Form.Label>
        <Form.Control {...register("phone")} />
      </Form.Group>

      {/* Job Title */}
      <Form.Group className="mb-3">
        <Form.Label>Job Title</Form.Label>
        <Form.Control {...register("jobTitle")} />
      </Form.Group>

      {/* Company */}
      <Form.Group className="mb-3">
        <Form.Label>Company</Form.Label>
        <Form.Select
          {...register("companyId", {
            required: "Company is required",
            valueAsNumber: true
          })}
        >
          <option value="">Select company</option>
          {companies.map((c) => (
            <option key={c.id} value={c.id}>
              {c.companyName}
            </option>
          ))}
        </Form.Select>
        {errors.companyId && (
          <small className="text-danger">{errors.companyId.message}</small>
        )}
      </Form.Group>

      {/* isActive checkbox only for edit */}
      {mode === "edit" && (
        <Form.Group className="mb-3">
          <Form.Check type="checkbox" label="Active" {...register("isActive")} />
        </Form.Group>
      )}

      <Button type="submit">Save</Button>
    </Form>
  );
}
