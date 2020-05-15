function login() {
    var userEmail = document.getElementById("uname").value;
    var userPassword = document.getElementById("pword").value;
    firebase.auth().signInWithEmailAndPassword(userEmail, userPassword).catch(function(error) {
        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        window.alert("Error: " + errorMessage);
        // ...
    });
    firebase.auth().onAuthStateChanged(function(user) {
        if (user) {
            window.location = "/index.html"
        } else {
            window.location = "/login.html"
        }
    });
}