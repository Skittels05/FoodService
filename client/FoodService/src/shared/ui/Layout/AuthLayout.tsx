import type { ReactNode } from 'react'
import './AuthLayout.css'

type AuthLayoutProps = {
  children: ReactNode
  className?: string
}

export function AuthLayout({ children, className = '' }: AuthLayoutProps) {
  return <div className={`auth-screen ${className}`}>{children}</div>
}
