import { Auth0Provider } from '@auth0/auth0-react'
import { Provider } from 'react-redux'
import type { ReactNode } from 'react'
import { auth0AuthorizationParams } from '@/modules/auth'
import { store } from '../store'
import { AuthTokenBridge } from './AuthTokenBridge'

type AppProvidersProps = {
  children: ReactNode
}

export function AppProviders({ children }: AppProvidersProps) {
  const domain = import.meta.env.VITE_AUTH0_DOMAIN
  const clientId = import.meta.env.VITE_AUTH0_CLIENT_ID
  const redirectUri = `${window.location.origin}/callback`

  return (
    <Auth0Provider
      domain={domain}
      clientId={clientId}
      authorizationParams={auth0AuthorizationParams(redirectUri)}
      useRefreshTokens={true}
    >
      <Provider store={store}>
        <AuthTokenBridge />
        {children}
      </Provider>
    </Auth0Provider>
  )
}
