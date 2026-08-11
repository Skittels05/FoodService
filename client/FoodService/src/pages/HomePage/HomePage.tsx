import { useAuth0 } from '@auth0/auth0-react'
import { AuthLayout, Button } from '@/shared/ui'

export function HomePage() {
  const { isAuthenticated, isLoading, loginWithRedirect } = useAuth0()

  if (isLoading) {
    return (
      <AuthLayout>
        <p className="auth-muted">Loading…</p>
      </AuthLayout>
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
    <AuthLayout className="home-screen">
      <Button
        variant="primary"
        className="home-auth-btn"
        onClick={goAuth0}
      >
        {isAuthenticated ? 'Continue' : 'Sign in'}
      </Button>
    </AuthLayout>
  )
}
