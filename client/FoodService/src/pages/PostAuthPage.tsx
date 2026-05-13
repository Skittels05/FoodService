import { useAuth0 } from '@auth0/auth0-react'
import { useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { useSyncUserMutation } from '../features/api/apiSlice'

type AuthResultState =
  | { ok: true; userId: string }
  | { ok: false; status?: number }

export function PostAuthPage() {
  const { isAuthenticated, isLoading: authLoading } = useAuth0()
  const navigate = useNavigate()
  const [syncUser, { isLoading }] = useSyncUserMutation()

  useEffect(() => {
    if (authLoading || !isAuthenticated) return
    void syncUser()
      .unwrap()
      .then((userId) =>
        navigate('/auth-result', {
          replace: true,
          state: { ok: true, userId } satisfies AuthResultState,
        }),
      )
      .catch((err: unknown) => {
        const status =
          err &&
          typeof err === 'object' &&
          'status' in err &&
          typeof (err as { status: unknown }).status === 'number'
            ? (err as { status: number }).status
            : undefined
        navigate('/auth-result', {
          replace: true,
          state: { ok: false, status } satisfies AuthResultState,
        })
      })
  }, [authLoading, isAuthenticated, syncUser, navigate])

  if (authLoading || !isAuthenticated) {
    return (
      <div className="auth-screen">
        <p className="auth-muted">Проверка сессии…</p>
      </div>
    )
  }

  return (
    <div className="auth-screen">
      <p className="auth-muted">
        {isLoading ? 'Синхронизация с сервером…' : 'Почти готово…'}
      </p>
    </div>
  )
}
