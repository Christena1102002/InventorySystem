 const API_BASE = 'https://localhost:7180';

    async function handleLogin() {
      const phoneNumber = document.getElementById('phoneNumber').value.trim();
      const password    = document.getElementById('password').value;
      const msg = document.getElementById('message');
      const btn = document.getElementById('loginBtn');

      msg.className = ''; msg.textContent = '';

      if (!phoneNumber || !password) {
        msg.className = 'error';
        msg.textContent = 'Please fill in all fields.';
        return;
      }

      btn.disabled = true; btn.textContent = 'Logging in…';

     try {
  const res = await fetch(`${API_BASE}/api/auth/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ phoneNumber, password })
  });

  const text = await res.text();
  const data = text ? JSON.parse(text) : {};
console.log('Response status:', res.status);
console.log('Response data:', data); 
 if (res.ok) {
    if (data.data.data.token) localStorage.setItem('authToken', data.data.data.token);
    msg.className = 'success';
    msg.textContent = '✓ Login successful! Redirecting…';
    setTimeout(() => { window.location.href = 'index.html'; }, 1000);
} else {
    throw new Error(data.message || data.title || `Error ${res.status}`);
  }
} catch (err) {
  msg.className = 'error';
  msg.textContent =err.message;
} finally {
  btn.disabled = false; btn.textContent = 'Login';
}
    }

    document.addEventListener('keydown', e => { if (e.key === 'Enter') handleLogin(); });