function vjsPrint(text) {
    var span = document.getElementById("spanForGet");

    span.innerHTML = text;
}

function jqPrint(text) {
    var span = $("#spanForGet2");

    span.html(text);
}

//https://www.w3schools.com/js/js_random.asp
function getRandomNumberInclusive(min, max) {
    var n = Math.floor(Math.random() * (max - min + 1) ) + min;

    return n;
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
        default:
            i = getRandomNumberInclusive(0, 2);
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
        default:
            v = getRandomNumberInclusive(1, 3) + "";
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
        default:
            var ddl = $("#ddl");

            var i = getNextIndex(100); //Force default

            ddl.prop("selectedIndex", i);
            break;    
    }

    return t;
}

//https://stackoverflow.com/questions/12802739/deselect-selected-options-in-select-menu-with-multiple-and-optgroups
function vjsClearSelection() {
    vjsPrint('<span style="color:red;">Select an item for full effect.</span><br /><i>look to your left</i>');

    var ddl = document.getElementById("ddl");

    //There are too many ways to do this, this is the most logical in my opinion
    ddl.selectedIndex = -1;

    //Looping is not necessary
}

function jqClearSelection() {
    jqPrint('<span style="color:red;">Select an item for full effect.</span><br /><i>look to your left</i>');

    var ddl = $("#ddl");

    ddl.prop("selectedIndex", -1);
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

//Global variable just for generating item ids
var _itemId = 0;

//https://www.w3schools.com/jsref/met_select_add.asp
function vjsInsertItem() {
    var ddl = document.getElementById("ddl");

    var option = document.createElement("option");
    
    option.value = 3;
    option.text = "Item " + _itemId;
    
    ddl.add(option);

    _itemId++;
 
    vjsPrint('<span style="color:red;">Open drop down.</span> Added: ' + option.text);
}

function jqInsertItem() {
    var ddl = $("#ddl");

    var text = "Item " + _itemId;

    ddl.append(
        $('<option></option>').val(4).html(text)
    );

    _itemId++;

    jqPrint('<span style="color:red;">Open drop down.</span> Added: ' + text);
}

//https://www.w3schools.com/jsref/prop_option_text.asp
function vjsUpdateItem() {
    var ddl = document.getElementById("ddl");

    var option = ddl.options[ddl.selectedIndex];
    
    option.text = option.text + "`";

    vjsPrint('<span style="color:red;">Added back tick to selected item.</span> Added: ' + option.text);
}

function jqUpdateItem() {
    var ddl = $("#ddl");

    var option = ddl.children("option").filter(":selected");

    option.text(option.text() + "`");

    jqPrint('<span style="color:red;">Added back tick to selected item.</span> Added: ' + option.text());
}

//https://stackoverflow.com/questions/7601691/remove-item-from-dropdown-list-on-page-load-no-jquery
function vjsRemoveItem() {
    var ddl = document.getElementById("ddl");

    var i = ddl.selectedIndex;

    //https://www.w3schools.com/jsref/prop_select_length.asp
    if(i > -1 && ddl.length > 0) {
        ddl.remove(i);

        vjsPrint('Removed item. Remainder: ' + ddl.length);
    } else {
        vjsPrint('<span style="color:red;">No items to remove.</span>');
    }
}

//https://stackoverflow.com/questions/1982449/jquery-to-remove-an-option-from-drop-down-list-given-options-text-value
function jqRemoveItem() {
    var ddl = $("#ddl");

    var i = ddl.prop("selectedIndex");

    //https://www.w3schools.com/jsref/prop_select_length.asp
    if(i > -1 && ddl.length > 0) {
        var option = ddl.children("option").filter(":selected");

        option.remove();

        jqPrint('Removed item. Remainder: ' + ddl.prop("length"));
    } else {
        jqPrint('<span style="color:red;">No items to remove.</span>');
    }
}

//https://stackoverflow.com/questions/3364493/how-do-i-clear-all-options-in-a-dropdown-box
function vjsClearList() {
    var ddl = document.getElementById("ddl");

    //https://www.w3schools.com/jsref/prop_select_length.asp
    if(ddl.length > 0) {
        var l = ddl.length - 1;

        for(var i = l; i >= 0; i--) {
            ddl.remove(i);
        }

        vjsPrint('Removed all items.');
    } else {
        vjsPrint('<span style="color:red;">No items to remove.</span>');
    }
}

function jqClearList() {
    var ddl = $("#ddl");

    if(ddl.prop("length") > 0) {
        var options = ddl.children("option");

        //Trying to use a for loop here won't work because the objects being access are jquery objects
        //If you attempt options[i], it returns undefined. Hence jQuery .each() has to be used
        $.each(options, function(index, option) {
            option.remove();
        });

        jqPrint('Removed all items.');
    } else {
        jqPrint('<span style="color:red;">No items to remove.</span>');
    }
}

function vjsLoadList() {
    var ddl = document.getElementById("ddl");

    for(var i = 0; i < 3; i++) {
        var option = document.createElement("option");
        
        option.value = 3;
        option.text = "Item " + _itemId;
        
        ddl.add(option);
    
        _itemId++;
    }
 
    vjsPrint('<span style="color:red;">Added three items</span>');
}

function jqLoadList() {
    var ddl = $("#ddl");

    for(var i = 0; i < 3; i++) {
        var text = "Item " + _itemId;

        ddl.append(
            $('<option></option>').val(4).html(text)
        );

        _itemId++;
    }

    jqPrint('<span style="color:red;">Added three items.</span>');
}

function vjsBindOnChange() {
    unbindEvents();

    var ddl = document.getElementById("ddl");

    //https://www.w3schools.com/jsref/event_onchange.asp
    ddl.addEventListener("change", vjsOnChange);

    vjsPrint("Event listener bound. Select an item.");
}

function jqBindOnChange() {
    unbindEvents();
    
    var ddl = $("#ddl");

    //https://api.jquery.com/change
    ddl.change(jqOnChange);
    
    jqPrint("Event listener bound. Select an item.");
}

function vjsOnChange() {
    var ddl = document.getElementById("ddl");

    vjsPrint("Selected index: " + ddl.selectedIndex);
}

function jqOnChange() {
    var ddl = $("#ddl");

    jqPrint("Selected index: " + ddl.prop("selectedIndex"));
}

function unbindEvents() {
    var ddl = document.getElementById("ddl");

    //Have to unbind before binding again
    //https://www.geeksforgeeks.org/javascript-removeeventlistener-method-with-examples/
    ddl.removeEventListener("change", vjsOnChange);
    ddl.removeEventListener("change", jqOnChange);

    $("#spanForGet").html("");
    $("#spanForGet2").html("");
}