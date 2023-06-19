// register.js
function register() {
    var login = document.getElementById("login").value;
    var password = document.getElementById("password").value;
    var data = {
        Login: login,
        Password: password
    };
    fetch('/admin/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.text())
        .then(result => {
            // Обработка результата
            console.log(result);
        })
        .catch(error => {
            // Обработка ошибки
            console.error('Error:', error);
        });
    window.location.href = "/Home/login";
}
