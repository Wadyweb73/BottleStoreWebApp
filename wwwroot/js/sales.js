let cart = [];
let cartTotal = 0;

document.addEventListener('DOMContentLoaded', function() {
    document.querySelectorAll('.add-button').forEach(button => {
        button.addEventListener('click', function() {
            const productId = parseInt(this.dataset.id);
            const productName = this.dataset.name;
            const unitPrice = parseFloat(this.dataset.price);
            const quantityInput = this.parentElement.querySelector('.quantity-input');
            const quantity = parseInt(quantityInput.value);

            addToCart(productId, productName, unitPrice, quantity);
        });
    });

    document.getElementById('limparCarrinho').addEventListener('click', function() {
        cart = [];
        updateCartDisplay();
        updateHiddenInputs();
    });

    document.querySelector('.finalizarCompra').addEventListener('click', function() {
        if (cart.length === 0) {
            e.preventDefault();
            alert('Carrinho está vazio! Adicione produtos antes de finalizar a compra.');
            return false;
        }
        
        const answer = confirm("Confirmar venda!");

        if (answer) {
            document.getElementById('salesForm').submit();
            updateHiddenInputs();
            return true;
        }
        else {
            return false;
        }
    });

    const searchInput = document.querySelector('.search-input');
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const searchTerm = this.value.toLowerCase();
            const products = document.querySelectorAll('.product-item');

            products.forEach(product => {
                const productName = product.querySelector('.product-name').textContent.toLowerCase();
                product.style.display = productName.includes(searchTerm) ? 'block' : 'none';
            });
        });
    }
});

function addToCart(productId, productName, unitPrice, quantity) {
    const existingItemIndex = cart.findIndex(item => item.productId === productId);

    if (existingItemIndex > -1) {
        cart[existingItemIndex].amount += quantity;
        cart[existingItemIndex].totalAmount = cart[existingItemIndex].amount * cart[existingItemIndex].unitPrice;
    } else {
        cart.push({
            productId: productId,
            productName: productName,
            amount: quantity,
            unitPrice: unitPrice,
            totalAmount: quantity * unitPrice
        });
    }

    updateCartDisplay();
    updateHiddenInputs();
}

function removeFromCart(productId) {
    cart = cart.filter(item => item.productId !== productId);
    updateCartDisplay();
    updateHiddenInputs();
}

function updateCartDisplay() {
    const cartItems = document.getElementById('cart-items');

    if (cart.length === 0) {
        cartItems.innerHTML = `
            <div class="empty-cart">
                <i class="fas fa-shopping-cart" style="font-size: 3rem; color: #e2e8f0; margin-bottom: 1rem;"></i>
                <h3>Carrinho Vazio</h3>
                <p>Os produtos adicionados aparecerão aqui!</p>
            </div>
        `;
    } else {
        cartItems.innerHTML = cart.map(item => `
            <div class="cart-item">
                <div class="cart-item-details">
                    <div class="item-name">${item.productName}</div>
                    <div class="item-quantity">Qtd: ${item.amount}</div>
                    <div class="item-price">${item.totalAmount.toFixed(2)} MT</div>
                </div>
                <button type="button" class="remove-item" onclick="removeFromCart(${item.productId})">
                    <i class="fas fa-times"></i>
                </button>
            </div>
        `).join('');
    }

    updateSummary();
}

function updateSummary() {
    const subtotal = cart.reduce((sum, item) => sum + item.totalAmount, 0);
    const total = subtotal;

    document.getElementById('subtotal').textContent = subtotal.toFixed(2) + ' MT';
    document.getElementById('total').textContent = total.toFixed(2) + ' MT';
}

function updateHiddenInputs() {
    const hiddenInputsContainer = document.getElementById('hidden-sales-inputs');
    hiddenInputsContainer.innerHTML = '';

    cart.forEach((item, index) => {
        const fields = [
            { name: `Sales[${index}].ProductId`, value: item.productId },
            { name: `Sales[${index}].Amount`, value: item.amount },
            { name: `Sales[${index}].UnitPrice`, value: item.unitPrice.toFixed(2) },
            { name: `Sales[${index}].TotalAmount`, value: item.totalAmount.toFixed(2) },
            { name: `Sales[${index}].UserId`, value: 1 }
        ];

        fields.forEach(field => {
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = field.name;
            input.value = field.value;
            hiddenInputsContainer.appendChild(input);
        });
    });
}




// // Funcionalidade básica do carrinho
// let cart = [];
// let cartTotal = 0;

// document.querySelectorAll('.add-button').forEach(button => {
//     button.addEventListener('click', function() {
//         const productItem = this.closest('.product-item');
//         const productName = productItem.querySelector('.product-name').textContent;
//         const productPrice = parseFloat(productItem.querySelector('.product-price').textContent.replace('MT', '').trim());
//         const quantity = parseInt(productItem.querySelector('.quantity-input').value);

//         addToCart(productName, productPrice, quantity);
//     });
// });

// function addToCart(name, price, quantity) {
//     const existingItem = cart.find(item => item.name === name);
    
//     if (existingItem) {
//         existingItem.quantity += quantity;
//     } else {
//         cart.push({ name, price, quantity });
//     }
    
//     updateCartDisplay();
// }

// function updateCartDisplay() {
//     const cartItems = document.getElementById('cart-items');
    
//     if (cart.length === 0) {
//         cartItems.innerHTML = `
//             <div class="empty-cart">
//                 <i class="fas fa-shopping-cart" style="font-size: 3rem; color: #e2e8f0; margin-bottom: 1rem;"></i>
//                 <h3>Carrinho Vazio</h3>
//                 <p>Os produtos adicionados aparecerão aqui!</p>
//             </div>
//         `;
//     } else {
//         cartItems.innerHTML = cart.map(item => `
//             <div class="cart-item">
//                 <div class="cart-item-details">
//                     <div class="item-name">${item.name}</div>
//                     <div class="item-quantity">Qtd: ${item.quantity}</div>
//                     <div class="item-price">${(item.price * item.quantity).toFixed(2)} MT</div>
//                 </div>
//                 <button class="remove-item" onclick="removeFromCart('${item.name}')">
//                     <i class="fas fa-times"></i>
//                 </button>
//             </div>
//         `).join('');
//     }

//     updateSummary();
// }

// function removeFromCart(name) {
//     cart = cart.filter(item => item.name !== name);
//     updateCartDisplay();
// }

// function updateSummary() {
//     const subtotal = cart.reduce((sum, item) => sum + (item.price * item.quantity), 0);

//     document.getElementById('subtotal').textContent = subtotal.toFixed(2) + ' MT';
//     document.getElementById('total').textContent = subtotal.toFixed(2) + ' MT';
// }

// document.getElementById('limparCarrinho').addEventListener('click', function() {
//     cart = [];
//     updateCartDisplay();
// });

// document.getElementById('finalizarCompra').addEventListener('click', function() {
//     if (cart.length > 0) {
//         alert('Compra finalizada com sucesso!');
//         cart = [];
//         updateCartDisplay();
//     } else {
//         alert('Carrinho está vazio!');
//     }
// });

// // Funcionalidade de pesquisa
// document.querySelector('.search-input').addEventListener('input', function() {
//     const searchTerm = this.value.toLowerCase();
//     const products = document.querySelectorAll('.product-item');
    
//     products.forEach(product => {
//         const productName = product.querySelector('.product-name').textContent.toLowerCase();
//         if (productName.includes(searchTerm)) {
//             product.style.display = 'block';
//         } else {
//             product.style.display = 'none';
//         }
//     });
// });






// const cart = {};

// function formatMt(value) {
//   return value.toFixed(2).replace('.', ',') + ' Mt';
// }

// function atualizarCarrinho() {
//   const cartItemsDiv = document.getElementById('cart-items');
//   cartItemsDiv.innerHTML = '';

//   let subtotal = 0;

//   Object.values(cart).forEach(item => {
//     const itemTotal = item.preco * item.quantidade;
//     subtotal += itemTotal;

//     const itemDiv = document.createElement('div');
//     itemDiv.classList.add('cart-item');
//     itemDiv.innerHTML = `
//       <div class="cart-item-details">
//         <span class="item-name">${item.nome}</span>
//         <span class="item-quantity">${item.quantidade}x</span>
//         <span class="item-price">${formatMt(itemTotal)}</span>
//       </div>
//       <button class="remove-item" data-id="${item.id}">
//         <i class="fas fa-trash"></i>
//       </button>
//     `;
//     cartItemsDiv.appendChild(itemDiv);

//     itemDiv.querySelector('.remove-item').addEventListener('click', () => {
//       delete cart[item.id];
//       atualizarCarrinho();
//     });
//   });

//   const imposto = subtotal * 0.05;
//   const total = subtotal + imposto;

//   document.getElementById('subtotal').innerText = formatMt(subtotal);
//   document.getElementById('tax').innerText = formatMt(imposto);
//   document.getElementById('total').innerText = formatMt(total);
// }

// document.querySelectorAll('.add-button').forEach(button => {
//   button.addEventListener('click', () => {
//     const id = button.dataset.id;
//     const quantityInput = document.querySelector(`.quantity-input[data-id="${id}"]`);
//     const quantidade = parseInt(quantityInput.value);
//     const nome = button.closest('.product-item').querySelector('.product-name').innerText;
//     const precoText = button.closest('.product-item').querySelector('.product-price').innerText;
//     const preco = parseFloat(precoText.replace('Mt', '').replace('.', '').replace(',', '.'));

//     if (cart[id]) {
//       cart[id].quantidade += quantidade;
//     } else {
//       cart[id] = {
//         id,
//         nome,
//         preco,
//         quantidade
//       };
//     }

//     atualizarCarrinho();
//   });
// });

// document.getElementById('limparCarrinho').addEventListener('click', () => {
//   for (let key in cart) delete cart[key];
//   atualizarCarrinho();
// });

  
//   document.getElementById('finalizarCompra').addEventListener('click', () => {
//   if (Object.keys(cart).length === 0) {
//       alert('O carrinho está vazio!');
//       return;
//   }

//   const itens = Object.values(cart);
//   const total = parseFloat(document.getElementById('total').innerText.replace('Mt', '').replace('.', '').replace(',', '.'));

//   fetch("{{ route('finalizar.compra') }}", {
//       method: 'POST',
//       headers: {
//       'Content-Type': 'application/json',
//       'X-CSRF-TOKEN': '{{ csrf_token() }}'
//       },
//       body: JSON.stringify({ itens, total })
//   })
//   .then(res => res.json())
//   .then(data => {
//       if (data.erro) {
//       alert("Erro: " + data.erro);
//       } else {
//       alert(data.mensagem);
//       window.location.href = `/recibo/${data.recibo_id}`;
//       }
//   })
//   .catch(err => alert("Erro ao finalizar venda: " + err));

//   for (let key in cart) delete cart[key];
//   atualizarCarrinho();
//   });


// atualizarCarrinho(); // Inicializa ao carregar a página
