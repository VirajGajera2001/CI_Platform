function check(){
    var p=document.getElementsByClassName("password");
    var cp=document.getElementsByClassName("confpassword");
    if(p[0].value!==cp[0].value){
        alert("mkdfd")
        return false;
    }
    else{
        return true;
    }
}