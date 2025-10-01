import { useState } from "react"
import Login from "./Login"
import Chat from "./Chat"

const App = () => {
  const [jwt] = useState(() => sessionStorage.getItem('jwt')) // lazy init

  if (!jwt)
    return <Login />

  return <Chat />
}
export default App