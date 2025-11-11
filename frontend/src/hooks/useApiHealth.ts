import { useEffect, useState, useRef } from "react";
import { checkHealth } from "../api/healthApi";
import { notifyError, notifySuccess } from "../components/Toast";

export function useApiHealth(pollIntervalMs = 5000) {
  const [isApiUp, setIsApiUp] = useState(true);
  const previous = useRef<boolean>(true);

  const check = async () => {
    try {
      const res = await checkHealth();
      const healthy = typeof res.data === "string" && res.data.toLowerCase().includes("healthy");

      if (healthy && previous.current === false) {
        notifySuccess("✅ API is back online");
      }

      setIsApiUp(healthy);
      previous.current = healthy;
    } catch {
      if (previous.current === true) {
        notifyError("❌ API is not available");
      }
      setIsApiUp(false);
      previous.current = false;
    }
  };

  useEffect(() => {
    check();
    const interval = setInterval(check, pollIntervalMs);
    return () => clearInterval(interval);
  }, []);

  return { isApiUp };
}
