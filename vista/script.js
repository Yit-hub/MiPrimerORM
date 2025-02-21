const API_URL = "http://localhost:5288/api/User"; // Cambia esto según tu API

// 🚀 Obtener todos los usuarios (GET)
async function getUsers() {
    try {
        let response = await fetch(API_URL);
        let users = await response.json();
        let userList = document.getElementById("userList");
        userList.innerHTML = "";
        users.forEach(user => {
            let li = document.createElement("li");
            li.textContent = `${user.id}: ${user.nombre} - ${user.email}`;
            userList.appendChild(li);
        });
    } catch (error) {
        console.error("Error obteniendo usuarios:", error);
    }
}

// 🚀 Agregar un nuevo usuario (POST)
async function addUser() {
    try {
        let nombre = document.getElementById("nombreAdd").value;
        let email = document.getElementById("emailAdd").value;
        let password = document.getElementById("passwordAdd").value;
        let rol = document.getElementById("rolAdd").value;

        let userData = { nombre, email, password, rol };

        let response = await fetch(API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(userData)
        });
        let newUser = await response.json();
        console.log("Usuario agregado:", newUser);
        getUsers(); // Actualizar la lista de usuarios
        clearAddForm(); // Limpiar el formulario de agregar
    } catch (error) {
        console.error("Error agregando usuario:", error);
    }
}

// Limpiar el formulario de agregar
function clearAddForm() {
    document.getElementById("nombreAdd").value = "";
    document.getElementById("emailAdd").value = "";
    document.getElementById("passwordAdd").value = "";
    document.getElementById("rolAdd").value = "";
}

// 🚀 Actualizar usuario por ID (PUT)
async function updateUser() {
    try {
        let userId = parseInt(document.getElementById("userIdUpdate").value, 10);
        let nombre = document.getElementById("nombreUpdate").value;
        let email = document.getElementById("emailUpdate").value;
        let password = document.getElementById("passwordUpdate").value;
        let rol = document.getElementById("rolUpdate").value;

        if (!userId || !nombre || !email) {
            alert("Por favor, complete todos los campos obligatorios.");
            return;
        }

        let updatedData = { id: userId, nombre, email, password, rol };

        let response = await fetch(`${API_URL}/${userId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updatedData)
        });

        if (response.ok) {
            console.log("Usuario actualizado correctamente");
            getUsers(); // Actualizar la lista de usuarios
            clearUpdateForm(); // Limpiar el formulario de actualizar
        } else {
            console.error("Error actualizando usuario");
            alert("Error actualizando usuario. Por favor, revise los datos ingresados.");
        }
    } catch (error) {
        console.error("Error actualizando usuario:", error);
        alert("Error actualizando usuario: " + error.message);
    }
}

// Limpiar el formulario de actualizar
function clearUpdateForm() {
    document.getElementById("userIdUpdate").value = "";
    document.getElementById("nombreUpdate").value = "";
    document.getElementById("emailUpdate").value = "";
    document.getElementById("passwordUpdate").value = "";
    document.getElementById("rolUpdate").value = "";
}

// 🚀 Eliminar usuario por ID (DELETE)
async function deleteUser() {
    try {
        let userId = parseInt(document.getElementById("userIdDelete").value, 10);
        let response = await fetch(`${API_URL}/${userId}`, { method: "DELETE" });
        if (response.ok) {
            console.log("Usuario eliminado correctamente");
            getUsers(); // Actualizar la lista de usuarios
        } else {
            console.error("Error eliminando usuario");
            alert("Error eliminando usuario. Por favor, revise el ID ingresado.");
        }
    } catch (error) {
        console.error("Error eliminando usuario:", error);
        alert("Error eliminando usuario: " + error.message);
    }
}

// Cargar la lista de usuarios al inicio
window.onload = getUsers;