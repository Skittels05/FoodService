import { useAuth0 } from '@auth0/auth0-react'
import { AuthLayout } from '@/shared/ui'
import { useSyncUserAfterLogin } from './hooks/useSyncUserAfterLogin'

export function PostAuthPage() {
  const { isAuthenticated, isLoading: authLoading } = useAuth0()
  const ready = !authLoading && isAuthenticated
  const { isLoading: syncLoading } = useSyncUserAfterLogin({ enabled: ready })

  if (!ready) {
    return (
      <AuthLayout>
        <p className="auth-muted">Checking session…</p>
      </AuthLayout>
    )
  }

  return (
    <AuthLayout>
      <p className="auth-muted">
        {syncLoading ? 'Syncing with server…' : 'Almost done…'}
      </p>
    </AuthLayout>
  )
}
