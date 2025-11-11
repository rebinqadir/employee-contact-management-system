import axios from "axios";

const BASE_URL = "https://localhost:7204/health";

export const checkHealth = () => axios.get<string>(BASE_URL);
