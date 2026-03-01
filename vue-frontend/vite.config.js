import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  plugins: [vue()],
  server: {
    proxy: {
      // Forward all /api/* requests to the .NET backend.
      // secure: false accepts the self-signed dev certificate.
      '/api': {
        target: 'http://localhost:5197',
        changeOrigin: true,
        secure: false,
      },
    },
  },
})
