import { useAuth0 } from '@auth0/auth0-react'
import { useEffect } from 'react'
import { setAccessTokenResolver } from './tokenBridge'

export function AuthTokenBridge() {
  const { getAccessTokenSilently, isAuthenticated } = useAuth0()

  useEffect(() => {
    if (!isAuthenticated) {
      setAccessTokenResolver(null)
      return
    }
    setAccessTokenResolver(() => getAccessTokenSilently())
    return () => {
      setAccessTokenResolver(null)
    }
  }, [getAccessTokenSilently, isAuthenticated])

  return null
}
