  const API_BASE = 'https://localhost:7180'; 

    async function handleRegister() {
      const fullName    = document.getElementById('fullName').value.trim();
      const phoneNumber = document.getElementById('phoneNumber').value.trim();
      const password    = document.getElementById('password').value;
      const msg = document.getElementById('message');
      const btn = document.getElementById('registerBtn');

      msg.className = ''; msg.textContent = '';

      if (!fullName || !phoneNumber || !password) {
        msg.className = 'error';
        msg.textContent = 'Please fill in all fields.';
        return;
      }

      btn.disabled = true; btn.textContent = 'Registering…';

     try {
  const res = await fetch(`${API_BASE}/api/auth/register`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ fullName, password, phoneNumber })
  });

  const text = await res.text();
  const data = text ? JSON.parse(text) : {};

  if (res.ok) {
    msg.className = 'success';
    msg.textContent = ' Account created! Redirecting…';
    setTimeout(() => { window.location.href = 'login.html'; }, 1400);
  } else {
    throw new Error(data.message || data.title || `Error ${res.status}`);
  }
} catch (err) {
  msg.className = 'error';
  msg.textContent =  err.message;
} finally {
  btn.disabled = false; btn.textContent = 'Register';
}
    }

    document.addEventListener('keydown', e => { if (e.key === 'Enter') handleRegister(); });