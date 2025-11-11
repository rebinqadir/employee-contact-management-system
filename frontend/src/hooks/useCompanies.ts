import { useCompanyContext } from "../contexts/CompanyContext";

export function useCompanies() {
  return useCompanyContext();
}
