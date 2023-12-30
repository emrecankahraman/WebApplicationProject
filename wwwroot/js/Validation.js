document.getElementById('creditNumberInput').addEventListener('input', function () {

    let creditNumber = this.value.replace(/\s|-/g, '').slice(0, 16);

    creditNumber = creditNumber.replace(/\D/g, '').replace(/(^\d{4}|\d{4})(?=\d)/g, '$1-');


    this.value = creditNumber;
});
document.getElementById('nameInput').addEventListener('input', function () {

    this.value = this.value.replace(/[^A-Za-z]/g, '').slice(0, 20);
});
document.getElementById('surnameInput').addEventListener('input', function () {

    this.value = this.value.replace(/[^A-Za-z]/g, '').slice(0, 20);
});


document.getElementById('cvcInput').addEventListener('input', function () {

    this.value = this.value.replace(/[^\d]/g, '').slice(0, 3);
});
document.getElementById('expirationInput').addEventListener('input', function () {

    let expiration = this.value.replace(/\s/g, '');

    expiration = expiration.slice(0, 5);


    expiration = expiration.replace(/\D/g, '').replace(/(^\d{2}|\d{2})(?=\d)/g, '$1/');


    this.value = expiration;
});

document.getElementById('paymentForm').addEventListener('submit', function (event) {

    event.preventDefault();


    const name = document.getElementById('nameInput').value;
    const surname = document.getElementById('surnameInput').value;
    const creditNumber = document.getElementById('creditNumberInput').value;
    const expiration = document.getElementById('expirationInput').value;
    const cvc = document.getElementById('cvcInput').value;


    if (creditNumber.length !== 16) {
        alert('Invalid credit card number!');
        return;
    }


    if (cvc.length !== 3) {
        alert('Invalid CVC!');
        return;
    }


    alert('Form is valid! Submitting...');
});
