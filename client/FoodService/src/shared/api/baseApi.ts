import { createApi } from '@reduxjs/toolkit/query/react'
import axios, { AxiosError, type AxiosRequestConfig } from 'axios'
import { getAccessToken } from './tokenBridge'

export const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
})

const axiosBaseQuery =
  () =>
  async ({ url, method, data, params, headers }: AxiosRequestConfig) => {
    try {
      const token = await getAccessToken()
      
      const result = await axiosInstance({
        url,
        method,
        data,
        params,
        headers: {
          ...headers,
          ...(token ? { Authorization: `Bearer ${token}` } : {}),
        },
      })
      return { data: result.data }
    } catch (axiosError) {
      const err = axiosError as AxiosError
      return {
        error: {
          status: err.response?.status,
          data: err.response?.data || err.message,
        },
      }
    }
  }

export const baseApi = createApi({
  reducerPath: 'api',
  baseQuery: axiosBaseQuery(),
  tagTypes: ['User'],
  endpoints: () => ({}),
})
