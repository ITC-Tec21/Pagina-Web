window.addEventListener('load', () => {
    if (localStorage.getItem("email")) {
        const mail = localStorage.getItem('email');
        document.getElementById("user-email").innerHTML = mail;
    }
});

async function deleteLocalUser() {
    const mail = localStorage.getItem('email');
    const password = localStorage.getItem('pswd');
    const values = { 'email': mail, 'pswd': password };
    localStorage.removeItem("email");
    localStorage.removeItem("password");
    let response = await fetch('/deleteLocal', {
        method: 'DELETE',
        headers: { "Content-Type": "application/json;charset=utf-8" },
        body: JSON.stringify(values)
    });
    alert("Logged Out");
    location.reload();
}