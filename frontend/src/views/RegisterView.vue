<script setup lang="ts">
import { ref } from 'vue'
import { useAuthStore } from '../stores/authStores'
import { useRouter } from 'vue-router'

const nome = ref('')
const email = ref('')
const password = ref('')
const loading = ref(false)
const authStore = useAuthStore()
const router = useRouter()

const handleRegister = async () => {
  if (!nome.value || !email.value || !password.value) {
    alert('Por favor, preencha todos os campos')
    return
  }

  loading.value = true
  try {
    await authStore.register({
      nome: nome.value,
      email: email.value,
      senha: password.value,
    })

    router.push('/')
  } catch (err: any) {
    alert(err.message || 'Erro ao criar conta')
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <main class="register-container">
    <section class="register-card">
      <h1>
        <img src="../assets/logo_lpp.png" alt="Logo LPP" class="logo" />
      </h1>
      <h2>Criar Conta</h2>

      <form @submit.prevent="handleRegister">
        <div class="input-group">
          <label>Nome Completo *</label>
          <input v-model="nome" type="text" placeholder="Digite seu nome completo" required />
        </div>

        <div class="input-group">
          <label>Email *</label>
          <input v-model="email" type="email" placeholder="Digite seu email" required />
        </div>

        <div class="input-group">
          <label>Senha *</label>
          <input v-model="password" type="password" placeholder="Crie uma senha" required />
        </div>

        <button type="submit" :disabled="loading">
          {{ loading ? 'Criando conta...' : 'Criar Conta' }}
        </button>
      </form>

      <p class="login-link">Já tem conta? <router-link to="/login">Faça login</router-link></p>
    </section>
  </main>
</template>

<style scoped>
.register-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  padding: 2rem 0;
}

.register-card {
  background: white;
  padding: 2.5rem;
  border-radius: 16px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
  width: 420px;
}

h1,
h2 {
  text-align: center;
  margin-bottom: 1rem;
}

h2 {
  color: #333;
  font-weight: 700;
  font-size: 1.5rem;
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
  margin-top: 1rem;
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

.login-link {
  text-align: center;
  margin-top: 1.5rem;
  color: #666;
  font-size: 0.9rem;
}

.login-link a {
  color: #42b883;
  text-decoration: none;
  font-weight: 600;
}

.login-link a:hover {
  text-decoration: underline;
}
</style>
