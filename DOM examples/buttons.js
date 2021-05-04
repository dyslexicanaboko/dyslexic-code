function vjsSetText() {
    var btn = document.getElementById("btn");

    //This won't work to set the button text
    //btn.value = "Vanilla JS set this text";
    
    btn.innerHTML = "Vanilla JS set this text";
    
    //This will work too, depends on your goal
    //btn.innerText = "Vanilla JS set this text";
}

function jqSetText() {
    var btn = $("#btn");

    //This won't work to set the button text
    //btn.val("jQuery set this text");
    
    btn.html("jQuery set this text");
    
    //This will work too, depends on your goal
    //btn.text("jQuery set this text");
}

//https://stackoverflow.com/questions/9251837/how-to-remove-all-listeners-in-an-element
//For demo purposes, need to unbind the events before binding again
function vjsSetClickEvent() {
    unbindEvents();

    var btn = document.getElementById("btn");

    //https://www.w3schools.com/jsref/event_onclick.asp
    btn.addEventListener("click", vjsPrintToSpan);
}

function jqSetClickEvent() {
    unbindEvents();
    
    var btn = $("#btn");

    //https://api.jquery.com/off/
    //Would use .off() but can't, it will only remove what was attached with .on()


    //https://api.jquery.com/click/
    btn.click(jqPrintToSpan);
}

function unbindEvents() {
    var btn = document.getElementById("btn");

    //Have to unbind before binding again
    //https://www.geeksforgeeks.org/javascript-removeeventlistener-method-with-examples/
    btn.removeEventListener("click", vjsPrintToSpan);
    btn.removeEventListener("click", jqPrintToSpan);

    $("#spanForGet").html("");
    $("#spanForGet2").html("");
}

function vjsPrintToSpan() {
    var span = document.getElementById("spanForGet");

    span.innerHTML = "Vanilla JS set click event on button";
}

function jqPrintToSpan() {
    var span = $("#spanForGet2");

    span.html("jQuery set click even on button");
}

function vjsDisableButton() {
    //https://www.w3schools.com/jsref/prop_pushbutton_disabled.asp
    document.getElementById("btn").disabled = true;
}

function jqDisableButton() {
    //https://api.jquery.com/prop/#prop-propertyName
    $("#btn").prop("disabled", true);
}

function vjsEnableButton() {
    //https://www.w3schools.com/jsref/prop_pushbutton_disabled.asp
    document.getElementById("btn").disabled = false;
}

function jqEnableButton() {
    //https://api.jquery.com/prop/#prop-propertyName
    $("#btn").prop("disabled", false);
}