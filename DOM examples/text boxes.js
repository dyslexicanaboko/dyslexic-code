function vjsSetText() {
    document.getElementById("txt").value = "Vanilla JS set this text";
}

function vjsGetText() {
    var span = document.getElementById("spanForGet");

    span.innerHTML = document.getElementById("txt").value + " read by VanillaJS, displayed with .innerHTML";
}

function vjsGetText2() {
    var span = document.getElementById("spanForGet2");

    span.innerText = document.getElementById("txt").value + " read by VanillaJS, displayed with .innerText";
}

function jqSetText() {
    $("#txt").val("jQuery set this text");
}

function jqGetText() {
    var span = $("#spanForGet");

    span.html($("#txt").val() + " read by jQuery, displayed with .html() function");
}

function jqGetText2() {
    var span = $("#spanForGet2");

    span.text($("#txt").val() + " read by jQuery, displayed with .text() function");
}

