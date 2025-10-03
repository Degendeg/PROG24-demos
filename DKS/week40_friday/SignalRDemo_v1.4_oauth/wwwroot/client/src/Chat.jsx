import { useState, useEffect, useRef } from "react"
import * as signalR from "@microsoft/signalr"
import DOMPurify from "dompurify"
import { ToastContainer, toast } from "react-toastify"
import { jwtDecode } from "jwt-decode"

const Chat = () => {
  const [connection, setConnection] = useState(null)
  const [messages, setMessages] = useState([])
  const [user, setUser] = useState("")
  const [message, setMessage] = useState("")
  const messagesEndRef = useRef(null)

  useEffect(() => {
    const start = async () => {
      const token = sessionStorage.getItem("jwt")
      if (!token) return
      if (connection) await connection.stop()

      const decoded = jwtDecode(token)
      const username = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
      setUser(username)

      const conn = new signalR.HubConnectionBuilder()
        .withUrl("/chathub", {
          accessTokenFactory: () => token
        })
        .withAutomaticReconnect()
        .build()

      conn.start()
        .then(() => console.log("Connected to hub"))
        .catch(err => console.error("SignalR error:", err))

      conn.on("ReceiveMessage", (u, msg) => {
        const safeUser = DOMPurify.sanitize(u)
        const safeMsg = DOMPurify.sanitize(msg)
        setMessages(prev => [...prev, { user: safeUser, text: safeMsg }])
      })

      setConnection(conn)
      return () => conn.stop()
    }
    start()
  }, [])

  useEffect(() => {
    if (messagesEndRef.current) {
      messagesEndRef.current.scrollIntoView({ behavior: "smooth" })
    }
  }, [messages])

  const sendMessage = async () => {
    if (connection && message && user) {
      try {
        await connection.invoke("SendMessage", message)
        setMessage("")
      } catch (err) {
        console.error("Send error:", err)
      }
    }
  }

  return (
    <div className="p-6 max-w-lg mx-auto mt-10">
      <div className="h-96 overflow-y-scroll border rounded-lg p-4 bg-primary">
        {messages && messages.length <= 0 && (
          <small className="text-gray-400">Inga meddelanden ännu 😞</small>
        )}
        {messages.length > 0 && messages.map((m, i) => (
          <div key={i} className={`chat ${m.user === user ? "chat-end" : "chat-start"}`}>
            <div className="chat-header">{m.user}</div>
            <div className={`chat-bubble ${m.user === user ? "bg-gray-700" : ""}`}>
              {m.text}
            </div>
          </div>
        ))}
        <div ref={messagesEndRef} />
      </div>

      <div className="mt-4 flex gap-2">
        <input
          type="text"
          placeholder="Name"
          value={user}
          onChange={e => setUser(e.target.value)}
          className="input input-bordered w-1/3"
          disabled
        />
        <input
          type="text"
          placeholder="Message"
          value={message}
          onChange={e => setMessage(e.target.value)}
          onKeyDown={e => { if (e.key === "Enter") sendMessage() }}
          className="input input-bordered flex-1"
        />
        <button className="btn btn-primary" onClick={sendMessage}>
          Send
        </button>
      </div>
      <ToastContainer />
    </div>
  )
}

export default Chat