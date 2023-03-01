function checkpass(){
    var p1=document.getElementsByClassName("pass1");
    var p2=document.getElementsByClassName("cnpass1")
    if(p1[0].value!==p2[0].value){
        alert("please enter same password");
        return false;
    }
    else{
        return true;
    }

}