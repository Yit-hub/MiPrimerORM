// Obtener todos los usuarios
function getAllUsers() {
    fetch('https://your-api-endpoint/api/user') // Cambiar a la URL de tu API
        .then(response => response.json())
        .then(data => {
            displayUsers(data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

// Agregar un nuevo usuario
function addUser() {
    const newUser = {
        name: 'Nuevo Usuario',
        email: 'newuser@example.com'
    };

    fetch('https://your-api-endpoint/api/user', { // Cambiar a la URL de tu API
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newUser)
    })
        .then(response => response.json())
        .then(data => {
            console.log('Usuario agregado:', data);
            getAllUsers(); // Refresh the user list
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

// Función para mostrar los usuarios en la página
function displayUsers(users) {
    const usersDiv = document.getElementById('users');
    usersDiv.innerHTML = ''; // Clear previous content

    users.forEach(user => {
        const userDiv = document.createElement('div');
        userDiv.textContent = `ID: ${user.id}, Name: ${user.name}, Email: ${user.email}`;
        usersDiv.appendChild(userDiv);
    });
}