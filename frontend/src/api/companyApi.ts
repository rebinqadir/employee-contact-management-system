import axios from "axios";
import type { Company } from "../types/company";

const BASE_URL = "https://localhost:7204/api/companies";

export const getCompanies = () => axios.get<Company[]>(BASE_URL);
