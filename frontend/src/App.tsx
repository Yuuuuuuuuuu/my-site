import { useEffect, useState } from 'react'

function App() {
  const [health, setHealth] = useState<string>('')
  const [users, setUsers] = useState<{ id: number; name: string; age: number }[]>([])

  useEffect(() => {
    fetch('/api/health')
      .then(res => res.json())
      .then(data => setHealth(data.status))

    fetch('/api/users')
      .then(res => res.json())
      .then(data => setUsers(data))
  }, [])

  return (
    <div style={{ padding: '2rem', fontFamily: 'sans-serif' }}>
      <h1>🚀 Go Practice</h1>
      <p>API Status: <strong>{health || 'Loading...'}</strong></p>

      <h2>Users</h2>
      <ul>
        {users.map(user => (
          <li key={user.id}>
            {user.name} — {user.age}岁
          </li>
        ))}
      </ul>
    </div>
  )
}

export default App
