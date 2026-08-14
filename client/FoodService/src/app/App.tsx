import { Navigate, Route, Routes } from 'react-router-dom'
import { RequireAuth } from '@/modules/auth'
import { AuthResultPage, CallbackPage, HomePage, PostAuthPage } from '@/pages'

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/callback" element={<CallbackPage />} />
      <Route
        path="/post-auth"
        element={
          <RequireAuth>
            <PostAuthPage />
          </RequireAuth>
        }
      />
      <Route
        path="/auth-result"
        element={
          <RequireAuth>
            <AuthResultPage />
          </RequireAuth>
        }
      />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  )
}
