import { useAuth0 } from '@auth0/auth0-react'
import type { ReactNode } from 'react'
import { Navigate, useLocation } from 'react-router-dom'

type RequireAuthProps = {
  children: ReactNode
}

export function RequireAuth({ children }: RequireAuthProps) {
  const { isAuthenticated, isLoading } = useAuth0()
  const location = useLocation()

  if (isLoading) {
    return (
      <div className="auth-screen">
        <p className="auth-muted">Loading…</p>
      </div>
    )
  }

  if (!isAuthenticated) {
    return <Navigate to="/" replace state={{ from: location.pathname }} />
  }

  return children
}
