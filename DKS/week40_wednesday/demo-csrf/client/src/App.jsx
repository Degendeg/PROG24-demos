import { useState } from 'react'

function App() {
  const [result, setResult] = useState('')
  const [amount, setAmount] = useState(0)

  const login = async () => {
    await fetch('http://localhost:4000/login', {
      method: 'POST',
      credentials: 'include'
    })
    setResult('Logged in')
  }

  const logout = async () => {
    await fetch('http://localhost:4000/clear', {
      method: 'DELETE',
      credentials: 'include'
    })
    setResult('Logged out')
  }

  const transfer = async () => {
    const res = await fetch('http://localhost:4000/transfer', {
      method: 'POST',
      credentials: 'include',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ amount: Number(amount) })
    })
    const data = await res.json()
    setResult(data.message || data.error)
  }

  return (
    <div className="min-h-screen bg-gray-700 flex items-center justify-center p-4">
      <div className="flex flex-col items-start space-y-4 mb-[30vh]">
        <h1 className="text-2xl font-bold text-white">CSRF Demo</h1>
        <div className="space-x-4">
          <button onClick={login} className="bg-blue-500 text-white px-4 py-2 rounded">Login</button>
          <button onClick={logout} className="bg-red-500 text-white px-4 py-2 rounded">Logout</button>
          <div className="flex rounded-lg mt-5 overflow-hidden border border-gray-300">
            <input
              type="number"
              min="0"
              value={amount}
              onChange={(e) => setAmount(e.target.value)}
              className="py-2 px-4 w-32 sm:w-40 bg-white border-0 focus:outline-none focus:ring-2 focus:ring-blue-500"
              placeholder="Amount"
            />
            <button
              onClick={transfer}
              className="bg-green-500 text-white px-4 py-2 hover:bg-green-600 transition-colors"
            >
              Transfer {amount || 0} SEK
            </button>
          </div>
        </div>
        <div className="text-lg text-white">{result}</div>
      </div>
    </div>
  )
}

export default App