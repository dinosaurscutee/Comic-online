const newPasswordInput = document.getElementById("newPasswordInput");
function showNewPassword() {
    const btnNewPassword = document.getElementById("btnNewPassword");
    if (newPasswordInput.type === "password") {
        newPasswordInput.type = "text";
        btnNewPassword.innerHTML = '<i class="icon ion-ios-eye-off icon-custom-password"></i>';
    } else {
        newPasswordInput.type = "password";
        btnNewPassword.innerHTML = '<i class="icon ion-ios-eye icon-custom-password"></i>';
    }
}