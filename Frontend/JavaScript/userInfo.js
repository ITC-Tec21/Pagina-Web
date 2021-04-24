async function saveInLocal() {
    const mail = document.getElementById("mail").value;
    const password = document.getElementById("pswd").value;
    localStorage.setItem("email", mail);
    localStorage.setItem("password", password);
    const values = { 'email': mail, 'pswd': password };
    let response = await fetch('/uploadLocal', {
        method: 'POST',
        headers: { "Content-Type": "application/json;charset=utf-8" },
        body: JSON.stringify(values)
    });
    return;
}