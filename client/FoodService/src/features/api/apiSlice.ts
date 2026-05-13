import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { getAccessToken } from './tokenBridge'

export const apiSlice = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: import.meta.env.VITE_API_URL ?? '',
    prepareHeaders: async (headers) => {
      const token = await getAccessToken()
      if (token) headers.set('authorization', `Bearer ${token}`)
      return headers
    },
  }),
  tagTypes: ['User'],
  endpoints: (build) => ({
    syncUser: build.mutation<string, void>({
      query: () => ({
        url: '/api/users/sync',
        method: 'POST',
      }),
    }),
  }),
})

export const { useSyncUserMutation } = apiSlice
