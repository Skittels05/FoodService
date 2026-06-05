import { baseApi } from './baseApi'

export const usersApi = baseApi.injectEndpoints({
  endpoints: (build) => ({
    syncUser: build.mutation<string, void>({
      query: () => ({
        url: '/api/users/sync',
        method: 'POST',
      }),
      invalidatesTags: ['User'],
    }),
  }),
})

export const { useSyncUserMutation } = usersApi
