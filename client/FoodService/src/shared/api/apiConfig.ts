export function resolveApiBaseUrl(): string {
  return import.meta.env.VITE_API_URL?.trim() || ''
}
