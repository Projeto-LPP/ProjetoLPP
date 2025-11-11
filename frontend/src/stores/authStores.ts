import { defineStore } from 'pinia'

export interface User {
  id: number
  nome: string
  email: string
  papel: string
  bioPerfil?: string
  dataCadastro: string
  totalProjetos: number
  projetosComoDono: number
  projetosComoMembro: number
}

export interface LoginResponse {
  message: string
  usuario: User
  token: string
}

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null as User | null,
    token: localStorage.getItem('token') as string | null,
  }),

  getters: {
    isAuthenticated: (state) => !!state.token,
    userName: (state) => state.user?.nome || '',
  },

  actions: {
    async login(email: string, password: string) {
      try {
        const response = await fetch('http://localhost:5085/api/Auth/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            email,
            senha: password,
          }),
        })

        if (!response.ok) {
          const errorData = await response.json()
          throw new Error(errorData.message || 'Login failed')
        }

        const data: LoginResponse = await response.json()

        this.user = data.usuario
        this.token = data.token

        localStorage.setItem('token', data.token)
        localStorage.setItem('user', JSON.stringify(data.usuario))

        return data
      } catch (error: any) {
        console.error('Login error:', error)
        throw new Error(error.message || 'Erro ao fazer login')
      }
    },

    async register(userData: { nome: string; email: string; senha: string }) {
      try {
        const response = await fetch('http://localhost:5085/api/Auth/registrar', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            ...userData,
            papel: 'aluno', // Sempre aluno no registro
          }),
        })

        if (!response.ok) {
          const errorData = await response.json()
          throw new Error(errorData.message || 'Registration failed')
        }

        const data: LoginResponse = await response.json()

        this.user = data.usuario
        this.token = data.token

        localStorage.setItem('token', data.token)
        localStorage.setItem('user', JSON.stringify(data.usuario))

        return data
      } catch (error: any) {
        console.error('Registration error:', error)
        throw new Error(error.message || 'Erro ao registrar')
      }
    },

    logout() {
      this.user = null
      this.token = null
      localStorage.removeItem('token')
      localStorage.removeItem('user')
    },

    initialize() {
      const token = localStorage.getItem('token')
      const userData = localStorage.getItem('user')

      if (token && userData) {
        this.token = token
        this.user = JSON.parse(userData)
      }
    },
  },
})
