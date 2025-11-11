import { createContext, useContext, useEffect, useState } from "react";
import type {ReactNode} from 'react';
import { getCompanies } from "../api/companyApi";
import type { Company } from "../types/company";

interface CompanyContextType {
  companies: Company[];
}

const CompanyContext = createContext<CompanyContextType>({ companies: [] });

export function CompanyProvider({ children }: { children: ReactNode }) {
  const [companies, setCompanies] = useState<Company[]>([]);

  useEffect(() => {
    getCompanies().then((res) => setCompanies(res.data));
  }, []);

  return (
    <CompanyContext.Provider value={{ companies }}>
      {children}
    </CompanyContext.Provider>
  );
}

export const useCompanyContext = () => useContext(CompanyContext);
