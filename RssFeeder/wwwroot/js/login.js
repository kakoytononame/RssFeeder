function loginin() {
    var login = document.getElementById("login").value;
    var password = document.getElementById("password").value;
    var data = {
        Login: login,
        Password: password
    };
    fetch('/admin/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.text())
        .then(result => {
            // Обработка результата
            window.location.href = "/Home/MainPage";
        })
        .catch(error => {
            // Обработка ошибки
            console.error('Error:', error);
        });
}
function toregistration(){
    window.location.href = "/Home/Register";
}