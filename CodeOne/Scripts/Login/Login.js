$(document).ready(function () {

});


function OpenLoginDialog() {
    $("#divLoginOpen").dialog({
        appendTo: "form",
        modal: true,
        dialogClass: "no-close",
        width: 350,
        height: 230,
        title: "Login Please",
        show: { effect: "size", duration: 800 },
        hide: { effect: "clip", duration: 800 },
        overlay: { backgroundColor: "#000", opacity: 0.5 },
        closeOnEscape: false
    }).css('z-index', '1005');
    return false;
}

//function OpenLoginDialogFast() {
//    $("#divLoginOpen").dialog({
//        //autoOpen: true,
//        modal: true,
//        width: 650,
//        height: 400,
//        title: "Login Please",
//        hide: {
//            effect: "clip",
//            duration: 800
//        },
//        closeOnEscape: false
//    }).css('z-index', '1005');
//    return false;
//}


function OpenRegister() {
    $("#divRegisterControl").dialog({
        //autoOpen: true,
        appendTo: "form",
        //modal: true,
        width: 800,
        height: 450,
        title: "Register",
        show: {
            effect: "clip",
            duration: 800
        },
        hide: {
            effect: "clip",
            duration: 800
        },
        closeOnEscape: false
    }).css('z-index', '1005');
    return false;
}
