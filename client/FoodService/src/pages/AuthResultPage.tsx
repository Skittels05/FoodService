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
            Успешно: профиль синхронизирован с сервером.
          </p>
          {data.userId ? (
            <p className="auth-muted">Идентификатор: {data.userId}</p>
          ) : null}
          <div className="auth-actions">
            <Link className="auth-btn auth-btn-secondary" to="/">
              На главную
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
              Выйти
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
          Не удалось синхронизировать профиль с сервером.
        </p>
        {data.status != null ? (
          <p className="auth-muted">Код ответа: {String(data.status)}</p>
        ) : null}
        <div className="auth-actions">
          <Link className="auth-btn auth-btn-primary" to="/post-auth">
            Повторить
          </Link>
          <Link className="auth-btn auth-btn-secondary" to="/">
            На главную
          </Link>
        </div>
      </div>
    </div>
  )
}
