window.addEventListener('load', () => {
    if (localStorage.getItem("email")) {
        const mail = localStorage.getItem('email');
        const pswd = localStorage.getItem('password');

        document.getElementById("user-email").innerHTML = mail;
        //document.getElementById("user-pswd").innerHTML = pswd;
    }
});

function deleteLocalUser() {
    localStorage.removeItem("email");
    localStorage.removeItem("password");
    alert("Logged Out");
    location.reload();
}