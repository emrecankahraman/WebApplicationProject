document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('paymentForm');

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        const emailInput = document.getElementById('Email').value;
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!emailPattern.test(Email)) {
            alert('Lütfen ge�erli bir e-posta adresi girin.');
            return false;
        } 
    });
});