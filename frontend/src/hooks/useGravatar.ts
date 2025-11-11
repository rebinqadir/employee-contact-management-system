import md5 from "crypto-js/md5";

export function useGravatar(email?: string, size: number = 80) {
  if (!email) {
    return `https://www.gravatar.com/avatar/?d=identicon&s=${size}`;
  }

  const normalized = email.trim().toLowerCase();
  const hash = md5(normalized).toString();

  return `https://www.gravatar.com/avatar/${hash}?d=identicon&s=${size}`;
}
