function saveInLocal() {
    const mail = document.getElementById("mail").value;
    const password = document.getElementById("pswd").value;
    localStorage.setItem("email", mail);
    localStorage.setItem("password", password);
    return;
}