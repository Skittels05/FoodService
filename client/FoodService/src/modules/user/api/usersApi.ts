import { baseApi } from '@/shared/api'

export const usersApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    syncUser: builder.mutation<string, void>({
      query: () => ({
        url: '/api/users/sync',
        method: 'POST',
      }),
      invalidatesTags: ['User'],
    }),
  }),
})

export const { useSyncUserMutation } = usersApi
