<script setup lang="ts">
import { useAuthStore } from '../stores/authStores'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}
</script>

<template>
  <div class="home-container">
    <header class="header">
      <div class="header-content">
        <h1>Bem-vindo, {{ authStore.userName }}! 🎉</h1>
        <button @click="handleLogout" class="logout-btn">Sair</button>
      </div>
    </header>

    <main class="main-content">
      <div class="welcome-card">
        <h2>Seu Dashboard</h2>
        <p>Você está logado no sistema LPP.</p>

        <div class="user-info">
          <h3>Informações do Usuário:</h3>
          <p><strong>Nome:</strong> {{ authStore.user?.nome }}</p>
          <p><strong>Email:</strong> {{ authStore.user?.email }}</p>
          <p><strong>Papel:</strong> {{ authStore.user?.papel }}</p>
          <p>
            <strong>Data de Cadastro:</strong>
            {{ new Date(authStore.user?.dataCadastro || '').toLocaleDateString('pt-BR') }}
          </p>
        </div>
      </div>
    </main>
  </div>
</template>

<style scoped>
.home-container {
  min-height: 100vh;
  background-color: #f8f9fa;
}

.header {
  background: white;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  padding: 1rem 0;
}

.header-content {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 2rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.header h1 {
  color: #333;
  margin: 0;
}

.logout-btn {
  background: #dc3545;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 6px;
  cursor: pointer;
  font-weight: 600;
}

.logout-btn:hover {
  background: #c82333;
}

.main-content {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem;
}

.welcome-card {
  background: white;
  padding: 2rem;
  border-radius: 12px;
  box-shadow: 0 4px 16px rgba(0, 0, 0, 0.1);
}

.welcome-card h2 {
  color: #333;
  margin-bottom: 1rem;
}

.user-info {
  margin-top: 2rem;
  padding: 1.5rem;
  background: #f8f9fa;
  border-radius: 8px;
}

.user-info h3 {
  color: #555;
  margin-bottom: 1rem;
}

.user-info p {
  margin: 0.5rem 0;
  color: #666;
}
</style>
