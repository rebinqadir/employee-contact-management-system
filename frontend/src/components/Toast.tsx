import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

export const notifySuccess = (message: string) => toast.success(message);
export const notifyError = (message: string) => toast.error(message);

export default function Toast() {
  return <ToastContainer position="top-right" autoClose={3000} />;
}
