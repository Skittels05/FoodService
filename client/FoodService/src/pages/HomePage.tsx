import { useAuth0 } from '@auth0/auth0-react'

export function HomePage() {
  const { isAuthenticated, isLoading, loginWithRedirect } = useAuth0()

  if (isLoading) {
    return (
      <div className="auth-screen">
        <p className="auth-muted">Загрузка…</p>
      </div>
    )
  }

  const goAuth0 = () =>
    void loginWithRedirect({
      ...(isAuthenticated
        ? { authorizationParams: { prompt: 'login' } }
        : {}),
      appState: { returnTo: '/post-auth' },
    })

  return (
    <div className="auth-screen home-screen">
      <button
        type="button"
        className="auth-btn auth-btn-primary home-auth-btn"
        onClick={goAuth0}
      >
        {isAuthenticated ? 'Продолжить' : 'Авторизация'}
      </button>
    </div>
  )
}
