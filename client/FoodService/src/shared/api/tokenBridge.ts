type TokenFn = () => Promise<string>

let tokenFn: TokenFn | null = null

export function setAccessTokenResolver(fn: TokenFn | null) {
  tokenFn = fn
}

export async function getAccessToken(): Promise<string | undefined> {
  if (!tokenFn) return undefined
  try {
    return await tokenFn()
  } catch {
    return undefined
  }
}
