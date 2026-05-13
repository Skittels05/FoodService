import { useAuth0 } from '@auth0/auth0-react'
import { Link, Navigate } from 'react-router-dom'

export function CallbackPage() {
  const { isLoading, error, isAuthenticated } = useAuth0()

  if (error) {
    return (
      <div className="auth-screen">
        <div className="auth-card">
          <p className="auth-error">Не удалось завершить вход.</p>
          <Link className="auth-btn auth-btn-secondary" to="/">
            На главную
          </Link>
        </div>
      </div>
    )
  }

  if (isLoading) {
    return (
      <div className="auth-screen">
        <p className="auth-muted">Завершение входа…</p>
      </div>
    )
  }

  if (isAuthenticated) {
    return <Navigate to="/post-auth" replace />
  }

  return <Navigate to="/" replace />
}
