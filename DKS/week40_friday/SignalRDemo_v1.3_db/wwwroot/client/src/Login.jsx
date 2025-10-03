import { useState } from "react"

const Login = () => {
  const [username, setUsername] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState("")

  const handleLogin = async (e) => {
    e.preventDefault()
    try {
      const res = await fetch("/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password })
      })

      if (!res.ok) {
        throw new Error("Fel användarnamn eller lösenord")
      }

      const data = await res.json()
      sessionStorage.setItem("jwt", data.token)
      location.reload()
    } catch (err) {
      setError(err.message)
    }
  }

  return (
    <div className="flex items-center justify-center h-screen">
      <form onSubmit={handleLogin} className="bg-white shadow rounded p-6 w-80">
        <h2 className="text-2xl text-black font-bold mb-4">Login 🗫</h2>
        {error && <p className="text-red-500 font-semibold mb-2">{error}</p>}
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          className="input input-bordered w-full mb-2"
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="input input-bordered w-full mb-2"
        />
        <button type="submit" className="btn btn-primary w-full">Login</button>
      </form>
    </div>
  )
}
export default Login