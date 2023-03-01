function check() {
    var p = document.getElementsByClassName("password");
    var cp = document.getElementsByClassName("confpassword");
    var m = document.getElementsByClassName("checkmail")
    var ph=document.getElementsByClassName("phonenumber");
    var regex = /^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$/;
    if (!regex.test(m[0].value)) {
        alert("email is not valid")
        return false;
    }
     else if (p[0].value !== cp[0].value) {
        alert("please enter same password");
        console.log("plesse");
        return false;
    }
    else {
        return true;
    }
}