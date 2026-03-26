// app.js

if (!localStorage.getItem('authToken')) {
  window.location.href = 'login.html';
}


      const productSelect = document.getElementById('productSelect');
const quantityInput = document.getElementById('quantityInput');
const sellBtn = document.getElementById('sellBtn');
const messageDiv = document.getElementById('message');

let products = [];

// Load products from API
async function loadProducts() {
  try {
    const response = await fetch('https://localhost:7180/api/Product/GetAllProducts'); // عدلي البورت حسب الباكيند// عدلي البورت حسب backend
    const result=await response.json();
    console.log('api response:',result);
    if (!response.ok) throw new Error('Failed to fetch products');

    products =result.data;

    productSelect.innerHTML = '<option value="">-- Select a product --</option>';
    products.forEach(p => {
      const option = document.createElement('option');
      option.value = p.id;
      option.textContent = `${p.name} (Available: ${p.quantity})`;
      productSelect.appendChild(option);
    });

  } catch (error) {
    console.error(error);
    messageDiv.textContent = "Error loading products from server!";
     messageDiv.className = "show-err";
 
  }
}

// Sell product
sellBtn.addEventListener('click', async () => {
  const productId = productSelect.value;
  const quantity = parseInt(quantityInput.value);

  if (!productId) {
    messageDiv.textContent = "Please select a product!";
    messageDiv.className = "show-err";
    return;
  }
  if (!quantity || quantity <= 0) {
    messageDiv.textContent = "Enter a valid quantity!";
   messageDiv.className = "show-err";
    return;
  }

  try {
 const response =await fetch('https://localhost:7180/api/Sales', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ productId, quantity })
    });

    if (!response.ok) {
      const errorData = await response.json();
      messageDiv.textContent = errorData.message || "Sale failed!";
       messageDiv.className = "show-err";
    } else {
      messageDiv.textContent = "Sale completed successfully";
     messageDiv.className = "show-ok";
      quantityInput.value = '';
      productSelect.value = '';
      loadProducts(); // refresh dropdown

      // Confetti effect
      confetti({
        particleCount: 100,
        spread: 70,
        origin: { y: 0.6 }
      });
    }

  } catch (error) {
    console.error(error);
    messageDiv.textContent = "Error connecting to server!";
    messageDiv.className = "text-red-500 mt-4 font-semibold";
  }
});

// Initial load
loadProducts();