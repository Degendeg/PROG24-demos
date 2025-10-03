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

  const handleGoogleLogin = () => {
    window.location.href = "/auth/google-login";
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
        <div className="divider divider-neutral text-neutral">or</div>
        <button
          type="button"
          onClick={handleGoogleLogin}
          className="btn btn-outline text-neutral hover:text-white w-full flex items-center justify-center gap-2 mt-2"
        >
          <img src="https://upload.wikimedia.org/wikipedia/commons/c/c1/Google_%22G%22_logo.svg" alt="Google" className="w-5 h-5" />
          Continue with Google
        </button>
      </form>
    </div>
  )
}
export default Login