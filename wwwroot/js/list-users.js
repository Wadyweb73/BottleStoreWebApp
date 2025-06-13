function loadEvents() {
    addEditUserButtonEvents();
    addCloseModalButtonEvent();
    addBlockUserButtonEvents();
    addCloseBlockUserModalEvent();
}

function addEditUserButtonEvents() {
    const editUserButtons = document.querySelectorAll('.js-edit-btn');
    const modal = document.querySelector('.js-edit-user-modal');

    editUserButtons.forEach(button => {
        button.addEventListener('click', () => {
            const userId = button.dataset.userId; 
            const userName = button.dataset.userName;
            const userEmail = button.dataset.userEmail;
            const userPhoneNumber = button.dataset.userPhoneNumber;
            const password = button.dataset.password;
            const userType = button.dataset.userType;

            console.log(`Editing user: ${userId}, Name: ${userName}, Email: ${userEmail}, Phone: ${userPhoneNumber}, Type: ${userType}`);

            const editUserForm = document.querySelector('.js-edit-user-form');
            editUserForm.querySelector('#editUserId').value = userId;
            editUserForm.querySelector('#editUserName').value = userName;
            editUserForm.querySelector('#editUserEmail').value = userEmail;
            editUserForm.querySelector('#editUserPhoneNumber').value = userPhoneNumber;

            if (userType === 'admin') {
                editUserForm.querySelector('.option-admin').selected = true
            }
            else {
                editUserForm.querySelector('.option-funcionario').selected = true;
            }

            if (!modal.classList.contains('active')) {
                modal.classList.add('active');
            }
        });
    })
}

function addBlockUserButtonEvents() {
    const blockUserButtons = document.querySelectorAll('.js-block-user-btn');
    const modal = document.querySelector('#blockModal');

    blockUserButtons.forEach(button => {
        button.addEventListener('click', () => {
            const userId = button.dataset.userId; 
            const userName = button.dataset.userName;
            const userEmail = button.dataset.userEmail;
            const userType = button.dataset.userType;

            modal.querySelector('.js-block-user-id-input').value = userId;
            modal.querySelector('#blockUserName').textContent = userName;
            modal.querySelector('#blockUserEmail').textContent = userEmail;
            modal.querySelector('#blockUserType').textContent = userType;

            if (!modal.classList.contains('active')) {
                modal.classList.add('active');
            }
        });
    });
}

function addCloseModalButtonEvent() {
    const closeModalButton = document.querySelector('.js-modal-close');
    const cancelUpdateButton = document.querySelector('.js-modal-cancel');
    const modal = document.querySelector('.js-edit-user-modal');

    closeModalButton.addEventListener('click', () => {
        modal.classList.remove('active');
    });

    cancelUpdateButton.addEventListener('click', () => {
        modal.classList.remove('active');
    });
}

function addCloseBlockUserModalEvent() {
    const cancelBlockButton = document.querySelector('.js-close-block-user-modal');
    const modal = document.querySelector('#blockModal');

    cancelBlockButton.addEventListener('click', () => {
        modal.classList.remove('active');
    });
}

function togglePassword(fieldId) {
    const field = document.getElementById(fieldId);
    const button = field.nextElementSibling;
    
    if (field.type === 'password') {
        field.type = 'text';
        button.textContent = '🙈';
    } else {
        field.type = 'password';
        button.textContent = '👁️';
    }
}

function main() {
    loadEvents();
}

document.addEventListener('DOMContentLoaded', main());
