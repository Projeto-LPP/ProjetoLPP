<script setup lang="ts">
import { ref } from "vue";
import { useAuthStore } from "../stores/authStores";
import image_lpp from "../assets/logo_lpp.png";

const email = ref("");
const password = ref("");
const loading = ref(false);
const authStore = useAuthStore();

const handleLogin = async () => {
  loading.value = true;
  try {
    await authStore.login(email.value, password.value);
    // Redireciona após login
    window.location.href = "/";
  } catch (err) {
    alert("Usuário ou senha inválidos");
  } finally {
    loading.value = false;
  }
};
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
          {{ loading ? "Entrando..." : "Entrar" }}
        </button>
      </form>
    </section>
  </main>
</template>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f6f8fa;
}

.login-card {
  background: white;
  padding: 2rem;
  border-radius: 12px;
  box-shadow: 0 4px 16px rgba(0, 0, 0, 0.1);
  width: 350px;
}

h1 {
  text-align: center;
  margin-bottom: 1.5rem;
}

.input-group {
  margin-bottom: 1rem;
}

input {
  width: 100%;
  padding: 0.6rem;
  border: 1px solid #ddd;
  border-radius: 6px;
}

button {
  width: 100%;
  padding: 0.7rem;
  border: none;
  border-radius: 6px;
  background-color: #42b883;
  color: white;
  font-weight: 600;
  cursor: pointer;
}

button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.logo {
    max-width: 100%;
    height: auto;
}
</style>
