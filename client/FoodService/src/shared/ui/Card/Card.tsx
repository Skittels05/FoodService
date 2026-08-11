import type { ReactNode } from 'react'
import './Card.css'

type CardProps = {
  children: ReactNode
  className?: string
}

export function Card({ children, className = '' }: CardProps) {
  return <div className={`auth-card ${className}`}>{children}</div>
}
