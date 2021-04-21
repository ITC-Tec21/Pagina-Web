window.addEventListener('load', () => {
    const mail = localStorage.getItem('email');
    const pswd = localStorage.getItem('password');

    document.getElementById("user-email").innerHTML = mail;
    document.getElementById("user-pswd").innerHTML = pswd;

});