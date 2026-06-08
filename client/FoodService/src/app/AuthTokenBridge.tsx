import { useAuth0 } from '@auth0/auth0-react'
import { accessTokenRequest } from '../features/auth/auth0Config'
import { setAccessTokenResolver } from '../features/api/tokenBridge'

export function AuthTokenBridge() {
  const { getAccessTokenSilently, isAuthenticated } = useAuth0()

  if (isAuthenticated) {
    setAccessTokenResolver(() =>
      getAccessTokenSilently(accessTokenRequest),
    )
  } else {
    setAccessTokenResolver(null)
  }

  return null
}
