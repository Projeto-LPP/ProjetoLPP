<script setup lang="ts">
import { ref } from 'vue'
import { useAuthStore } from '../stores/authStores'
import { useRouter } from 'vue-router'

const email = ref('')
const password = ref('')
const loading = ref(false)
const authStore = useAuthStore()
const router = useRouter()

const handleLogin = async () => {
  loading.value = true
  try {
    await authStore.login(email.value, password.value)
    router.push('/')
  } catch (err: any) {
    alert(err.message || 'Usuário ou senha inválidos')
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <main class="login-container">
    <section class="login-card">
      <h1>
        <img src="../assets/logo_lpp.png" alt="Logo LPP" class="logo" />
      </h1>

      <form @submit.prevent="handleLogin">
        <div class="input-group">
          <label>Email</label>
          <input v-model="email" type="email" placeholder="Digite seu email" required />
        </div>

        <div class="input-group">
          <label>Senha</label>
          <input v-model="password" type="password" placeholder="Digite sua senha" required />
        </div>

        <button type="submit" :disabled="loading">
          {{ loading ? 'Entrando...' : 'Entrar' }}
        </button>
      </form>

      <p class="register-link">
        Não tem conta? <router-link to="/register">Cadastre-se</router-link>
      </p>
    </section>
  </main>
</template>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.login-card {
  background: white;
  padding: 2.5rem;
  border-radius: 16px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
  width: 380px;
}

h1 {
  text-align: center;
  margin-bottom: 2rem;
}

.logo {
  max-width: 180px;
  height: auto;
}

.input-group {
  margin-bottom: 1.5rem;
}

label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 600;
  color: #333;
  font-size: 0.9rem;
}

input {
  width: 100%;
  padding: 0.875rem;
  border: 2px solid #e1e5e9;
  border-radius: 8px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

input:focus {
  outline: none;
  border-color: #42b883;
  box-shadow: 0 0 0 3px rgba(66, 184, 131, 0.1);
}

button {
  width: 100%;
  padding: 0.875rem;
  border: none;
  border-radius: 8px;
  background: linear-gradient(135deg, #42b883, #369870);
  color: white;
  font-weight: 600;
  font-size: 1rem;
  cursor: pointer;
  transition:
    transform 0.2s,
    box-shadow 0.2s;
  margin-top: 0.5rem;
}

button:hover:not(:disabled) {
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(66, 184, 131, 0.4);
}

button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
  transform: none;
}

.register-link {
  text-align: center;
  margin-top: 1.5rem;
  color: #666;
  font-size: 0.9rem;
}

.register-link a {
  color: #42b883;
  text-decoration: none;
  font-weight: 600;
}

.register-link a:hover {
  text-decoration: underline;
}
</style>
