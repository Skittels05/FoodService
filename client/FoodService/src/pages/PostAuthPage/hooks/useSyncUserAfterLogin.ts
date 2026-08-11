import { useEffect, useRef } from 'react'
import { useNavigate } from 'react-router-dom'
import { useSyncUserMutation } from '@/modules/user'

type AuthResultState =
  | { ok: true; userId: string }
  | { ok: false; status?: number }

type Options = {
  enabled: boolean
}

export function useSyncUserAfterLogin({ enabled }: Options) {
  const navigate = useNavigate()
  const [syncUser, { isLoading, isError, isSuccess }] = useSyncUserMutation()
  const startedRef = useRef(false)

  useEffect(() => {
    if (!enabled || startedRef.current) return
    startedRef.current = true

    void (async () => {
      try {
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
  }, [enabled, syncUser, navigate])

  return { isLoading, isError, isSuccess }
}
