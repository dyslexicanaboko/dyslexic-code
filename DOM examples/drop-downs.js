function vjsPrint(text) {
    var span = document.getElementById("spanForGet");

    span.innerHTML = text;
}

function jqPrint(text) {
    var span = $("#spanForGet2");

    span.html(text);
}

function vjsGetSelectedValue() {
    var ddl = document.getElementById("ddl");

    vjsPrint("value: " + ddl.value);
}

function jqGetSelectedValue() {
    var ddl = $("#ddl");

    jqPrint("value: " + ddl.val());
}

//https://stackoverflow.com/questions/14976495/get-selected-option-text-with-javascript
function vjsGetSelectedText() {
    var ddl = document.getElementById("ddl");

    //ddl.innerText - don't use .innerText, it will output all of the items' text separated by spaces. Not useful.
    var text = ddl.options[ddl.selectedIndex].text;

    vjsPrint("text: " + text);
}

//https://learn.jquery.com/using-jquery-core/faq/how-do-i-get-the-text-value-of-a-selected-option/
function jqGetSelectedText() {
    var ddl = $("#ddl");

    //ddl.text() - don't use .text(), it will output all of the items' text separated by spaces. Not useful.
    
    //If you want to get everything in one shot specify the id and selector
    //$("#ddl option:selected").text();

    //Using a combination of the children and filter functions, you can take the jQuery
    //object reference, access the options (items) and filter the items using the 
    //":selected" selector
    var text = ddl.children("option").filter(":selected").text();

    //Don't do this, it will just return blank
    //var text = ddl.filter("option:selected").text();

    jqPrint("text: " + text);
}

function vjsSetSelectedByIndex() {
    var ddl = document.getElementById("ddl");

    //This code is here to just select what isn't selected already.
    var i = getNextIndex(ddl.selectedIndex);

    //Index is not synonymous with value, however many times the value and the
    //index are equal, but it's a coincidence.
    ddl.selectedIndex = i;

    vjsPrint("Setting drop down index to: " + i + "<br /><i>look to your left</i>");
}

function jqSetSelectedByIndex() {
    var ddl = $("#ddl");

    //https://stackoverflow.com/questions/13556941/get-index-of-selected-option-with-jquery
    //There are too many ways to get the index, choosing .prop() function
    var i = getNextIndex(ddl.prop("selectedIndex"));

    ddl.prop("selectedIndex", i);

    jqPrint("Setting drop down index to: " + i + "<br /><i>look to your left</i>");
}

function getNextIndex(selectedIndex) {
    var i = 0;

    switch(selectedIndex)
    {
        case 0:
            i = 1;
            break;
        case 1:
            i = 2;
            break;
        case 2:
            i = 0;
            break;
    }

    return i;
}

function vjsSetSelectedByValue() {
    var ddl = document.getElementById("ddl");
 
    var v = getNextValue(ddl.value);

    ddl.value = v;

    vjsPrint("Setting drop down value to: " + v + "<br /><i>look to your left</i>");
}

function jqSetSelectedByValue() {
    var ddl = $("#ddl");

    var v = getNextValue(ddl.val());

    ddl.val(v);
 
    jqPrint("Setting drop down value to: " + v + "<br /><i>look to your left</i>");
}

function getNextValue(selectedValue) {
    var v = "1";

    switch(selectedValue)
    {
        case "1":
            v = "2";
            break;
        case "2":
            v = "3";
            break;
        case "3":
            v = "1";
            break;
    }

    return v;
}

function vjsSetSelectedByText() {
    var ddl = document.getElementById("ddl");
 
    var text = ddl.options[ddl.selectedIndex].text;

    var t = getNextText(text);

    //You cannot set your drop down selection by text, you can however do a lookup
    //of your selected item by text to get the index.
    //ddl.options[ddl.selectedIndex].text = t; //If you do this, you are modifying the text of the item which is not the goal here

    //https://stackoverflow.com/questions/3989324/javascript-set-dropdown-selected-item-based-on-option-text
    for (var i = 0; i < ddl.options.length; i++) {
        if (ddl.options[i].text === t) {
            ddl.selectedIndex = i;
            break;
        }
    }

    vjsPrint("Setting drop down index to: " + i + " by looking up text \"" + t + "\"<br /><i>look to your left</i>");
}

function jqSetSelectedByText() {
    var ddl = $("#ddl");

    var options = ddl.children("option");
    
    var text = options.filter(":selected").text();

    var i;
    var t = getNextText(text);

    //For demonstration purposes, jQuery has a .each() function (for each).
    //Using a regular for loop is okay too.
    //https://api.jquery.com/jquery.each/
    $.each(options, function(index, option) {
        if (option.text === t) { //Pay attention, this is the text property not function
            i = index;

            ddl.prop("selectedIndex", i);
            return;
        }
    });

    jqPrint("Setting drop down index to: " + i + " by looking up text \"" + t + "\"<br /><i>look to your left</i>");
}

function getNextText(selectedText) {
    var t = "Item A";

    switch(selectedText)
    {
        case "Item A":
            t = "Item B";
            break;
        case "Item B":
            t = "Item C";
            break;
        case "Item C":
            t = "Item A";
            break;
    }

    return t;
}

function vjsClearSelection() {

}

function jqClearSelection() {

}

function vjsDisableDropDown() {
    //https://www.w3schools.com/jsref/prop_pushbutton_disabled.asp
    document.getElementById("ddl").disabled = true;
}

function jqDisableDropDown() {
    //https://api.jquery.com/prop/#prop-propertyName
    $("#ddl").prop("disabled", true);
}

function vjsEnableDropDown() {
    //https://www.w3schools.com/jsref/prop_pushbutton_disabled.asp
    document.getElementById("ddl").disabled = false;
}

function jqEnableDropDown() {
    //https://api.jquery.com/prop/#prop-propertyName
    $("#ddl").prop("disabled", false);
}

function vjsInsertItem() {

}

function jqInsertItem() {

}

function vjsUpdateItem() {
    //https://www.w3schools.com/jsref/prop_option_text.asp
}

function jqUpdateItem() {

}

function vjsRemoveItem() {

}

function jqRemoveItem() {

}

function vjsClearList() {

}

function jqClearList() {

}

function vjsLoadList() {

}

function jqLoadList() {

}

//https://stackoverflow.com/questions/9251837/how-to-remove-all-listeners-in-an-element
//For demo purposes, need to unbind the events before binding again
function vjsBindOnChange() {
    unbindEvents();

    var ddl = document.getElementById("ddl");

    //https://www.w3schools.com/jsref/event_onclick.asp
    ddl.addEventListener("click", vjsPrintToSpan);
}

function jqBindOnChange() {
    unbindEvents();
    
    var ddl = $("#ddl");

    //https://api.jquery.com/off/
    //Would use .off() but can't, it will only remove what was attached with .on()


    //https://api.jquery.com/click/
    ddl.click(jqPrintToSpan);
}

function unbindEvents() {
    var ddl = document.getElementById("ddl");

    //Have to unbind before binding again
    //https://www.geeksforgeeks.org/javascript-removeeventlistener-method-with-examples/
    ddl.removeEventListener("click", vjsPrintToSpan);
    ddl.removeEventListener("click", jqPrintToSpan);

    $("#spanForGet").html("");
    $("#spanForGet2").html("");
}