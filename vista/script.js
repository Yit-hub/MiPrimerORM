const API_URL = "http://localhost:5288/api/User"; // Cambia esto según tu API

// 🚀 Obtener todos los usuarios (GET)
async function getUsers() {
    try {
        let response = await fetch(API_URL);
        let users = await response.json();
        console.log("Usuarios obtenidos:", users);
    } catch (error) {
        console.error("Error obteniendo usuarios:", error);
    }
}

// 🚀 Obtener un usuario por ID (GET)
async function getUserById(userId) {
    try {
        let response = await fetch(`${API_URL}/${userId}`);
        if (!response.ok) throw new Error("Usuario no encontrado");
        let user = await response.json();
        console.log("Usuario obtenido:", user);
    } catch (error) {
        console.error("Error obteniendo usuario:", error);
    }
}

// 🚀 Agregar un nuevo usuario (POST)
async function addUser(userData) {
    try {
        let response = await fetch(API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(userData)
        });
        let newUser = await response.json();
        console.log("Usuario agregado:", newUser);
    } catch (error) {
        console.error("Error agregando usuario:", error);
    }
}

// 🚀 Actualizar usuario por ID (PUT)
async function updateUser(userId, updatedData) {
    try {
        let response = await fetch(`${API_URL}/${userId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updatedData)
        });
        if (response.ok) console.log("Usuario actualizado correctamente");
        else console.error("Error actualizando usuario");
    } catch (error) {
        console.error("Error actualizando usuario:", error);
    }
}

// 🚀 Eliminar usuario por ID (DELETE)
async function deleteUser(userId) {
    try {
        let response = await fetch(`${API_URL}/${userId}`, { method: "DELETE" });
        if (response.ok) console.log("Usuario eliminado correctamente");
        else console.error("Error eliminando usuario");
    } catch (error) {
        console.error("Error eliminando usuario:", error);
    }
}

async function getUsers() {
    try {
        let response = await fetch("http://localhost:5288/api/User");
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
// 🔹 Pruebas en consola:
//getUsers(); // Obtener todos los usuarios
// getUserById(1); // Obtener usuario con ID 1
// addUser({ nombre: "Juan", email: "juan@example.com" }); // Agregar usuario
// updateUser(1, { nombre: "Juan Pérez", email: "juanperez@example.com" }); // Actualizar usuario con ID 1
// deleteUser(1); // Eliminar usuario con ID 1
