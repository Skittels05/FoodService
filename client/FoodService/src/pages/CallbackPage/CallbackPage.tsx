import { useAuth0 } from '@auth0/auth0-react'
import { Link, Navigate } from 'react-router-dom'
import { AuthLayout, Card } from '@/shared/ui'

export function CallbackPage() {
  const { isLoading, error, isAuthenticated } = useAuth0()

  if (error) {
    return (
      <AuthLayout>
        <Card>
          <p className="auth-error">Could not complete sign-in.</p>
          <Link className="auth-btn auth-btn-secondary" to="/">
            Home
          </Link>
        </Card>
      </AuthLayout>
    )
  }

  if (isLoading) {
    return (
      <AuthLayout>
        <p className="auth-muted">Completing sign-in…</p>
      </AuthLayout>
    )
  }

  if (isAuthenticated) {
    return <Navigate to="/post-auth" replace />
  }

  return <Navigate to="/" replace />
}
