import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { resolveApiBaseUrl } from './apiConfig'
import { getAccessToken } from './tokenBridge'

export const baseApi = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: resolveApiBaseUrl(),
    prepareHeaders: async (headers) => {
      const token = await getAccessToken()
      if (token) headers.set('authorization', `Bearer ${token}`)
      return headers
    },
  }),
  tagTypes: ['User'],
  endpoints: () => ({}),
})
