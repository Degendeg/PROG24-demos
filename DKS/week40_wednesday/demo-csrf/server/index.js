const express = require('express')
const cors = require('cors')
const cookieParser = require('cookie-parser')
const { v4: uuidv4 } = require('uuid')
const app = express()
const PORT = 4000

app.use(cookieParser())
app.use(express.json())
app.use(cors({
  origin: 'http://localhost:5173',
  credentials: true
}))

app.post('/login', (req, res) => {
  res.cookie('sessionId', uuidv4(), {
    httpOnly: true, // kan ej kommas åt via JS, testa byt till false sen
    sameSite: 'lax'
  })
  res.json({ message: 'Logged in' })
})

app.delete('/clear', (req, res) => {
  res.clearCookie('sessionId', { path: '/' })
  res.json({ message: 'Cookie cleared!' })
})

// skyddad endpoint
app.post('/transfer', (req, res) => {
  const sessionId = req.cookies.sessionId
  if (!sessionId) return res.status(401).json({ error: 'Not logged in' })
  const { amount } = req.body
  res.json({ message: `Transferred ${amount} SEK` })
})

app.listen(PORT, () => console.log(`Server körs på http://localhost:${PORT}`))