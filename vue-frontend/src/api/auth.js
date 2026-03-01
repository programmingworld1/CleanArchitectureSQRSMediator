// auth.js — thin wrappers around the authentication endpoints.
// The X-Api-Key header value is read from the .env file via Vite's
// import.meta.env mechanism so it never has to be hard-coded in components.

const API_KEY = import.meta.env.VITE_API_KEY

/**
 * Shared helper — performs a POST to `url` with a JSON body and
 * the required authentication headers.
 *
 * @param {string} url  - Relative URL, e.g. "/api/v1/authentication/login"
 * @param {object} body - Plain object that will be JSON-serialised
 * @returns {Promise<object>} Parsed response body on success
 * @throws {Error} With message taken from ProblemDetails.detail on failure
 */
async function post(url, body) {
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'X-Api-Key': API_KEY,
    },
    body: JSON.stringify(body),
  })

  const data = await response.json()

  if (!response.ok) {
    // The backend returns RFC 7807 ProblemDetails — use the detail field
    // when available, otherwise fall back to the HTTP status text.
    throw new Error(data.detail ?? response.statusText)
  }

  return data
}

/**
 * Register a new user account.
 *
 * @param {string} firstName
 * @param {string} lastName
 * @param {string} email
 * @param {string} password
 * @returns {Promise<object>} The created user object (includes token)
 */
export async function register(firstName, lastName, email, password) {
  return post('/api/v1/authentication/create', {
    firstName,
    lastName,
    email,
    password,
  })
}

/**
 * Log in with existing credentials.
 *
 * @param {string} email
 * @param {string} password
 * @returns {Promise<object>} The authenticated user object (includes token)
 */
export async function login(email, password) {
  return post('/api/v1/authentication/login', { email, password })
}
