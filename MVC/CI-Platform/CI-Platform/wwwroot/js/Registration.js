function check() {
    var fname = document.getElementsByClassName("fname");
    var lname = document.getElementsByClassName("lname");
    var p = document.getElementsByClassName("password");
    var cp = document.getElementsByClassName("confpassword");
    var m = document.getElementsByClassName("checkmail")
    var ph = document.getElementsByClassName("pnumber");
    var regex = /^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$/;
    const fnameRegex = /^[a-zA-Z]+$/;
    var phoneno = /^[0-9]+$/;
    var phlength = /^\d{10}$/;
    const passwordRegex = /^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$/;
    if (p[0].value !== cp[0].value) {
        alert("please enter same password");
        return false;
    }
    if (!fnameRegex.test(fname[0].value)) {
        alert("Please enter only alphabats in firstname");
        return false;
    }
    if (!fnameRegex.test(lname[0].value)) {
        alert("Please enter only alphabats in last name");
        return false;
    }
    if (!phoneno.test(ph[0].value)) {
        alert("Please enter only digits in phone number");
        return false;
    }
    if (!phlength.test(ph[0].value)) {
        console.log(ph[0].toString().length);
        alert("Please enter only 10 digits");
        return false;
    }
    if (!regex.test(m[0].value)) {
        alert("email is not valid")
        return false;
    }
    if (!passwordRegex.test(p[0].value)) {
        alert("Please enter valid password");
        return false;
    }
}