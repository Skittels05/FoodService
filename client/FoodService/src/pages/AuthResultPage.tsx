import { useAuth0 } from '@auth0/auth0-react'
import { Link, Navigate, useLocation } from 'react-router-dom'

type AuthResultState =
  | { ok: true; userId: string }
  | { ok: false; status?: number }

export function AuthResultPage() {
  const { state } = useLocation()
  const { logout } = useAuth0()
  const data = state as AuthResultState | null

  if (!data || typeof data.ok !== 'boolean') {
    return <Navigate to="/" replace />
  }

  if (data.ok) {
    return (
      <div className="auth-screen">
        <div className="auth-card">
          <p className="auth-success">
            Success: your profile has been synced with the server.
          </p>
          {data.userId ? (
            <p className="auth-muted">ID: {data.userId}</p>
          ) : null}
          <div className="auth-actions">
            <Link className="auth-btn auth-btn-secondary" to="/">
              Home
            </Link>
            <button
              type="button"
              className="auth-btn auth-btn-ghost"
              onClick={() =>
                void logout({
                  logoutParams: { returnTo: window.location.origin },
                })
              }
            >
              Sign out
            </button>
          </div>
        </div>
      </div>
    )
  }

  return (
    <div className="auth-screen">
      <div className="auth-card">
        <p className="auth-error">
          Could not sync your profile with the server.
        </p>
        {data.status != null ? (
          <p className="auth-muted">Response code: {String(data.status)}</p>
        ) : null}
        <div className="auth-actions">
          <Link className="auth-btn auth-btn-primary" to="/post-auth">
            Try again
          </Link>
          <Link className="auth-btn auth-btn-secondary" to="/">
            Home
          </Link>
        </div>
      </div>
    </div>
  )
}
