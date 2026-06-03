import { useAuth0 } from '@auth0/auth0-react'
import { Link, Navigate } from 'react-router-dom'

export function CallbackPage() {
  const { isLoading, error, isAuthenticated } = useAuth0()

  if (error) {
    return (
      <div className="auth-screen">
        <div className="auth-card">
          <p className="auth-error">Could not complete sign-in.</p>
          <Link className="auth-btn auth-btn-secondary" to="/">
            Home
          </Link>
        </div>
      </div>
    )
  }

  if (isLoading) {
    return (
      <div className="auth-screen">
        <p className="auth-muted">Completing sign-in…</p>
      </div>
    )
  }

  if (isAuthenticated) {
    return <Navigate to="/post-auth" replace />
  }

  return <Navigate to="/" replace />
}
