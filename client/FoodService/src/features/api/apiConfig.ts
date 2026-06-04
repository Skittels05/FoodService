
export const API_BASE_URL_LOCAL = 'http://localhost:5270'

export const API_BASE_URL_CONTAINER = 'http://localhost:5270'

export type ApiDeployTarget = 'local' | 'container'

function getDeployTarget(): ApiDeployTarget {
  return import.meta.env.VITE_API_DEPLOY_TARGET === 'container'
    ? 'container'
    : 'local'
}

function getFallbackBaseUrl(): string {
  if (getDeployTarget() === 'container') {
    return import.meta.env.VITE_API_URL_CONTAINER?.trim() || API_BASE_URL_CONTAINER
  }
  return import.meta.env.VITE_API_URL_LOCAL?.trim() || API_BASE_URL_LOCAL
}

export function resolveApiBaseUrl(): string {
  const explicit = import.meta.env.VITE_API_URL?.trim()
  if (explicit) return explicit
  return getFallbackBaseUrl()
}
