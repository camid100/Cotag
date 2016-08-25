function wait(divName) {
    var obj = document.getElementById(divName);
    obj.style.display = 'block';
}
function stopWait(divName) {
    var obj = document.getElementById(divName);
    obj.style.display = 'none';
}

function ValidatePage() {

    if (typeof (Page_ClientValidate) == 'function') {
        Page_ClientValidate();
    }

    if (Page_IsValid) {
        wait('waitingDiv');
        cl.show();
    }
    else {
        stopWait('waitingDiv');
    }
}