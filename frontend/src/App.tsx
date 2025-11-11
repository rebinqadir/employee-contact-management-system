import { BrowserRouter, Routes, Route } from "react-router-dom";
import EmployeesPage from "./pages/EmployeesPage";
import AddEmployeePage from "./pages/AddEmployeePage";
import EditEmployeePage from "./pages/EditEmployeePage";
import Toast from "./components/Toast";
import { CompanyProvider } from "./contexts/CompanyContext";
import { EmployeeProvider } from "./contexts/EmployeeContext";

import ApiHealthBanner from "./components/ApiHealthBanner";
import { useApiHealth } from "./hooks/useApiHealth";


export default function App() {

  const { isApiUp } = useApiHealth();

  return (
    <BrowserRouter>
      <CompanyProvider>
        <EmployeeProvider>
          <Toast />

          {/* API availability banner */}
          <ApiHealthBanner isApiUp={isApiUp} />

          <Routes>
            <Route path="/" element={<EmployeesPage />} />
            <Route path="/employees/add" element={<AddEmployeePage />} />
            <Route path="/employees/edit/:id" element={<EditEmployeePage />} />
          </Routes>
        </EmployeeProvider>
      </CompanyProvider>
    </BrowserRouter>
  );
}
