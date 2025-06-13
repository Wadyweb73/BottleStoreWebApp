function loadPageEvents() {
    AddChangeViewModeEvent();
    AddBlockProductButtonEvent();
    AddCloseBlockProductModalEvent();
}

function AddChangeViewModeEvent() {
    const selectInput = document.querySelector('.js-view-mode-select-input');
    const tableView = document.querySelector('.js-table-view');
    const cardView = document.querySelector('.js-card-view');

    selectInput.addEventListener('change', (event) => {
        const selectedOption = event.target.value;

        if (selectedOption == '1') {
            cardView.classList.add('display-none');
            tableView.classList.remove('display-none');
        }
        else if (selectedOption == '2') {
            tableView.classList.add('display-none');
            cardView.classList.remove('display-none');
        }
    });
}

function AddBlockProductButtonEvent() {
    const blockProductButtons = document.querySelectorAll('.js-block-product-button');
    const blockProductModal   = document.querySelector('.js-block-product-modal');
    const blockProductIdInput = document.querySelector('.js-block-product-id');
    const productNameElement  = document.querySelector('.js-block-product-name');

    blockProductButtons.forEach(button => {
        button.addEventListener('click', () => {
            const productName = button.dataset.productName;
            const productId = button.dataset.productId;

            productNameElement.textContent = productName;
            blockProductIdInput.value = productId;
            
            blockProductModal.classList.add('active');
        });
    });
}

function AddCloseBlockProductModalEvent() {
    const closeBlockProductModalButton = document.querySelector('.js-close-block-product-modal');
    const blockProductModal = document.querySelector('.js-block-product-modal');

    closeBlockProductModalButton.addEventListener('click', () => {
        blockProductModal.classList.remove('active');
    });
}

function main() {
    loadPageEvents();
}

document.addEventListener('DOMContentLoaded', main());
