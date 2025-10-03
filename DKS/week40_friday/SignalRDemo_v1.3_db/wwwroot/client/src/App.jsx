import { useState } from "react"
import Login from "./Login"
import Chat from "./Chat"

const App = () => {
  const [jwt, setJwt] = useState(() => sessionStorage.getItem('jwt')) // lazy init

  const handleLogout = () => {
    sessionStorage.removeItem('jwt')
    setJwt(null)
  }

  return (
    <>
      <nav className="bg-primary text-white h-12 flex items-center justify-between px-4">
        <span className="font-bold text-lg">Cool Chat</span>
        {jwt && (
          <button onClick={handleLogout}
            className="bg-white text-primary px-2 py-1 rounded text-sm cursor-pointer">
            Logout
          </button>
        )}
      </nav>

      {jwt ? <Chat /> : <Login />}
    </>
  )
}
export default App