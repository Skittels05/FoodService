import type { ReactNode } from 'react'
import clsx from 'clsx'
import './Card.css'

type CardProps = {
  children: ReactNode
  className?: string
}

export function Card({ children, className }: CardProps) {
  return <div className={clsx('auth-card', className)}>{children}</div>
}
