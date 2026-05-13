import { Navigate, Route, Routes } from 'react-router-dom'
import { RequireAuth } from '../components/RequireAuth.tsx'
import { AuthResultPage } from '../pages/AuthResultPage.tsx'
import { CallbackPage } from '../pages/CallbackPage.tsx'
import { HomePage } from '../pages/HomePage.tsx'
import { PostAuthPage } from '../pages/PostAuthPage.tsx'

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
