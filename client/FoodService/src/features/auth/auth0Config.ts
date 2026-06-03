export const auth0Audience = import.meta.env.VITE_AUTH0_AUDIENCE

export const accessTokenRequest = auth0Audience
  ? { authorizationParams: { audience: auth0Audience } }
  : {}

export function auth0AuthorizationParams(redirectUri: string) {
  return {
    redirect_uri: redirectUri,
    ...(auth0Audience ? { audience: auth0Audience } : {}),
  }
}
