import { useAuth0 } from '@auth0/auth0-react'
import { useEffect, useRef } from 'react'
import { useNavigate } from 'react-router-dom'
import { useSyncUserMutation } from '../api/usersApi'
import { accessTokenRequest } from './auth0Config'

type AuthResultState =
  | { ok: true; userId: string }
  | { ok: false; status?: number }

type Options = {
  enabled: boolean
}

export function useSyncUserAfterLogin({ enabled }: Options) {
  const navigate = useNavigate()
  const { getAccessTokenSilently } = useAuth0()
  const [syncUser, { isLoading, isError, isSuccess }] = useSyncUserMutation()
  const startedRef = useRef(false)

  useEffect(() => {
    if (!enabled || startedRef.current) return
    startedRef.current = true

    void (async () => {
      try {
        await getAccessTokenSilently(accessTokenRequest)
        const userId = await syncUser().unwrap()
        navigate('/auth-result', {
          replace: true,
          state: { ok: true, userId } satisfies AuthResultState,
        })
      } catch (err: unknown) {
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
      }
    })()
  }, [enabled, getAccessTokenSilently, syncUser, navigate])

  return { isLoading, isError, isSuccess }
}
