<template>
  <!-- The form element prevents the default page-reload on submit. -->
  <form @submit.prevent="handleSubmit">
    <h2>Login</h2>

    <!-- v-model creates a two-way binding between the input and the data property. -->
    <div class="field">
      <label for="login-email">Email</label>
      <input
        id="login-email"
        v-model="email"
        type="email"
        required
        autocomplete="email"
      />
    </div>

    <div class="field">
      <label for="login-password">Password</label>
      <input
        id="login-password"
        v-model="password"
        type="password"
        required
        autocomplete="current-password"
      />
    </div>

    <!-- Show an error banner only when errorMessage is non-empty. -->
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>

    <!-- The button is disabled while the request is in-flight to prevent double-submits. -->
    <button type="submit" :disabled="loading">
      {{ loading ? 'Logging in…' : 'Login' }}
    </button>
  </form>
</template>

<script>
// Import the login helper from our API layer.
import { login } from '../api/auth.js'

export default {
  name: 'LoginForm',

  // Emits declares the custom events this component can fire.
  // The parent (App.vue) listens for 'success' to know login worked.
  emits: ['success'],

  data() {
    return {
      email: '',
      password: '',
      // errorMessage is shown inline when the API call fails.
      errorMessage: '',
      // loading prevents double-submits and drives the button label.
      loading: false,
    }
  },

  methods: {
    async handleSubmit() {
      // Clear any previous error before each attempt.
      this.errorMessage = ''
      this.loading = true

      try {
        // Call the backend login endpoint.
        const user = await login(this.email, this.password)

        // Emit the user object upwards so App.vue can store it.
        this.$emit('success', user)
      } catch (err) {
        // Display the error message returned by the API.
        this.errorMessage = err.message
      } finally {
        // Always re-enable the button, whether the call succeeded or failed.
        this.loading = false
      }
    },
  },
}
</script>

<style scoped>
/* scoped means these styles only apply to elements inside this component */
form {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.field {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

label {
  font-weight: 600;
  font-size: 0.875rem;
}

input {
  padding: 8px 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 1rem;
}

button {
  padding: 10px;
  background: #2563eb;
  color: #fff;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  cursor: pointer;
}

button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.error {
  color: #dc2626;
  font-size: 0.875rem;
  margin: 0;
}
</style>
