<template>
  <!-- @submit.prevent stops the browser from reloading the page on submit. -->
  <form @submit.prevent="handleSubmit">
    <h2>Create Account</h2>

    <div class="field">
      <label for="reg-firstname">First Name</label>
      <input
        id="reg-firstname"
        v-model="firstName"
        type="text"
        required
        autocomplete="given-name"
      />
    </div>

    <div class="field">
      <label for="reg-lastname">Last Name</label>
      <input
        id="reg-lastname"
        v-model="lastName"
        type="text"
        required
        autocomplete="family-name"
      />
    </div>

    <div class="field">
      <label for="reg-email">Email</label>
      <input
        id="reg-email"
        v-model="email"
        type="email"
        required
        autocomplete="email"
      />
    </div>

    <div class="field">
      <label for="reg-password">Password</label>
      <input
        id="reg-password"
        v-model="password"
        type="password"
        required
        autocomplete="new-password"
      />
    </div>

    <!-- v-if renders this element only when errorMessage has content. -->
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>

    <button type="submit" :disabled="loading">
      {{ loading ? 'Creating account…' : 'Register' }}
    </button>
  </form>
</template>

<script>
import { register } from '../api/auth.js'

export default {
  name: 'RegisterForm',

  // The parent listens to 'success' to switch to the welcome screen.
  emits: ['success'],

  data() {
    return {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      errorMessage: '',
      loading: false,
    }
  },

  methods: {
    async handleSubmit() {
      this.errorMessage = ''
      this.loading = true

      try {
        // Call the backend registration endpoint with all four fields.
        const user = await register(
          this.firstName,
          this.lastName,
          this.email,
          this.password
        )

        // Propagate the returned user object to App.vue.
        this.$emit('success', user)
      } catch (err) {
        this.errorMessage = err.message
      } finally {
        this.loading = false
      }
    },
  },
}
</script>

<style scoped>
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
  background: #16a34a;
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
