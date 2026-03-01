<template>
  <div class="app">
    <header>
      <h1>Rockstar</h1>
    </header>

    <main>
      <!-- ── Authenticated view ── -->
      <!-- v-if shows this block only when currentUser is not null -->
      <div v-if="currentUser" class="welcome-card">
        <p class="welcome-text">
          Welcome, <strong>{{ currentUser.firstName }}</strong>!
        </p>
        <button class="logout-btn" @click="logout">Logout</button>
      </div>

      <!-- ── Auth forms view ── -->
      <!-- v-else is shown when currentUser IS null -->
      <div v-else class="auth-card">
        <!-- Tab buttons to switch between login and register -->
        <div class="tabs">
          <button
            :class="{ active: activeTab === 'login' }"
            @click="activeTab = 'login'"
          >
            Login
          </button>
          <button
            :class="{ active: activeTab === 'register' }"
            @click="activeTab = 'register'"
          >
            Register
          </button>
        </div>

        <!-- Only the currently active form is rendered (v-if / v-else-if). -->
        <!-- @success listens for the 'success' event emitted by each form   -->
        <!-- and calls our onAuthSuccess method with the returned user object. -->
        <LoginForm
          v-if="activeTab === 'login'"
          @success="onAuthSuccess"
        />
        <RegisterForm
          v-else-if="activeTab === 'register'"
          @success="onAuthSuccess"
        />
      </div>
    </main>
  </div>
</template>

<script>
import LoginForm from './components/LoginForm.vue'
import RegisterForm from './components/RegisterForm.vue'

// Key used to persist the user object in localStorage so the session
// survives a page refresh.
const STORAGE_KEY = 'rockstar_user'

export default {
  name: 'App',

  components: { LoginForm, RegisterForm },

  data() {
    return {
      // Which tab is shown: 'login' | 'register'
      activeTab: 'login',
      // The authenticated user object, or null when logged out.
      currentUser: null,
    }
  },

  // created() runs once when the component is first mounted.
  // We use it to restore a previous session from localStorage so the user
  // does not have to log in again after a page refresh.
  created() {
    const stored = localStorage.getItem(STORAGE_KEY)
    if (stored) {
      try {
        this.currentUser = JSON.parse(stored)
      } catch {
        // Corrupted storage — start fresh.
        localStorage.removeItem(STORAGE_KEY)
      }
    }
  },

  methods: {
    /**
     * Called when either LoginForm or RegisterForm emits 'success'.
     *
     * @param {object} user - The user object returned by the API
     */
    onAuthSuccess(user) {
      // Persist the user so a page refresh keeps the session alive.
      localStorage.setItem(STORAGE_KEY, JSON.stringify(user))
      this.currentUser = user
    },

    /**
     * Clear the session and return to the login tab.
     */
    logout() {
      localStorage.removeItem(STORAGE_KEY)
      this.currentUser = null
      this.activeTab = 'login'
    },
  },
}
</script>

<style>
/* Global reset */
*, *::before, *::after {
  box-sizing: border-box;
}

body {
  margin: 0;
  font-family: system-ui, -apple-system, sans-serif;
  background: #f3f4f6;
  color: #111827;
}
</style>

<style scoped>
.app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

header {
  background: #1e3a5f;
  color: #fff;
  padding: 16px 24px;
}

header h1 {
  margin: 0;
  font-size: 1.5rem;
}

main {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 24px;
}

/* ── Auth card ── */
.auth-card {
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 400px;
  overflow: hidden;
}

.tabs {
  display: flex;
  border-bottom: 1px solid #e5e7eb;
}

.tabs button {
  flex: 1;
  padding: 12px;
  background: none;
  border: none;
  font-size: 1rem;
  cursor: pointer;
  color: #6b7280;
  border-bottom: 3px solid transparent;
  transition: color 0.15s, border-color 0.15s;
}

.tabs button.active {
  color: #2563eb;
  border-bottom-color: #2563eb;
  font-weight: 600;
}

/* The form inside the card gets padding via a wrapper rule.       */
/* LoginForm / RegisterForm render a <form> as root, so we target  */
/* the immediate child of .auth-card after .tabs.                  */
.auth-card > :not(.tabs) {
  padding: 24px;
}

/* ── Welcome card ── */
.welcome-card {
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  padding: 32px;
  text-align: center;
  min-width: 280px;
}

.welcome-text {
  font-size: 1.25rem;
  margin: 0 0 20px;
}

.logout-btn {
  padding: 10px 24px;
  background: #dc2626;
  color: #fff;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  cursor: pointer;
}

.logout-btn:hover {
  background: #b91c1c;
}
</style>
