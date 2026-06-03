import { useAuth0 } from '@auth0/auth0-react'
import { useSyncUserAfterLogin } from '../features/auth/useSyncUserAfterLogin'

export function PostAuthPage() {
  const { isAuthenticated, isLoading: authLoading } = useAuth0()
  const ready = !authLoading && isAuthenticated
  const { isLoading: syncLoading } = useSyncUserAfterLogin({ enabled: ready })

  if (!ready) {
    return (
      <div className="auth-screen">
        <p className="auth-muted">Checking session…</p>
      </div>
    )
  }

  return (
    <div className="auth-screen">
      <p className="auth-muted">
        {syncLoading ? 'Syncing with server…' : 'Almost done…'}
      </p>
    </div>
  )
}
