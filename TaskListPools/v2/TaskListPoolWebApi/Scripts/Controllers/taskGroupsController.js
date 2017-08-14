function btnEditToggle_click(taskGroupId, editMode) {
    var r = $("#divReg" + taskGroupId);
    var e = $("#divEdit" + taskGroupId);
    var m = getJQueryObjects(taskGroupId);

    if (editMode === true) {
        r.hide();
        e.show();
        m.Name.label.hide();
        m.Name.text.show();
        m.Description.label.hide();
        m.Description.text.show();
    }
    else {
        r.show();
        e.hide();
        m.Name.label.show();
        m.Name.text.hide();
        m.Description.label.show();
        m.Description.text.hide();
    }
}

function getJQueryObjects(taskGroupId) {
    return {
        Name: { 
            label: $("#lblName" + taskGroupId),
            text: $("#txtName" + taskGroupId) 
        },
        Description: {
            label: $("#lblDescription" + taskGroupId),
            text: $("#txtDescription" + taskGroupId)
        },
        CreatedOn: $("#lblCreatedOn" + taskGroupId),
        TaskCount: $("#lblTaskCount" + taskGroupId)
    };
}

function getModel(taskGroupId) {
    var m = getJQueryObjects(taskGroupId);

    return {
        TaskGroupId: taskGroupId,
        Name: m.Name.text.val(),
        Description: m.Description.text.val(),
        CreatedOn: m.CreatedOn.text(),
        TaskCount: parseInt(m.TaskCount.text())
    };
}

function setModelHtml(taskGroup) {
    var m = getJQueryObjects(taskGroup.TaskGroupId);

    m.Name.label.text(taskGroup.Name);
    m.Description.label.text(taskGroup.Description);
    m.Name.text.val(taskGroup.Name);
    m.Description.text.val(taskGroup.Description);
    m.CreatedOn.text(taskGroup.CreatedOn);
    m.TaskCount.text(taskGroup.TaskCount);
}

function page_load() {
    getTaskPoolService()
        .taskGroups
        .getAll()
        .then(function (response) {
            var arr = response.data;

            addModelsToTable(arr);
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });

    preLoadModal();
}

function addModelsToTable(models) {
    var templateRow = $("#row_id0");

    _.forEach(models, function (model) {
        //Clone the template row
        var tr = templateRow.clone();

        //Replace the id attribute accordingly
        tr.attr("id", "row" + model.TaskGroupId);

        //Mass replace all of the ids with the new id
        tr.html(tr.html().replace(/_id0/g, model.TaskGroupId));

        //Add the cloned row to the table
        $("#tblModel tbody").append(tr);

        //Update the html normally
        setModelHtml(model);

        //console.dir(tr);
    });
}

function btnTaskGroupAdd_click() {
    var task = getModel(0);

    saveTaskGroup(task, function (objTaskGroup) {
        toastMessages.success("Group " + objTaskGroup.TaskGroupId + " created successfully");

        //Add the new model to the table
        addModelsToTable([objTaskGroup]);

        //Clear out the input form
        $("#txtName0").val("");
        $("#txtDescription0").val("");
    });
}

function btnTaskGroupEdit_click(taskGroupId) {
    var task = getModel(taskGroupId);

    saveTaskGroup(task, function (objTaskGroup) {
        toastMessages.success("Group updated successfully");

        btnEditToggle_click(taskGroupId, false);

        setModelHtml(task);
    });
}

function btnTaskGroupDelete_click(taskGroupId) {
    if (!confirm("Are you sure you want to delete this group?")) {
        return;
    }

    getTaskPoolService()
        .taskGroups
        .delete(taskGroupId)
        .then(function (result) {
            $("#row" + taskGroupId).remove();
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });
}

function btnAddTasksToGroup_click(rowId) {
    console.log("click: " + rowId);

    dialog.dialog("open");
}

function btnShowTaskAddModal_click(taskGroupId) {
    /* 1. Show existing tasks if any
     * 2. All 
     */
}

function saveTaskGroup(task, onSuccess) {
    getTaskPoolService()
        .taskGroups
        .save(task)
        .then(function (result) {
            var objTaskGroup = result.data;

            //console.log("getData.promise");
            //console.dir(objTaskGroup);

            //_members.$scope.devTaskGroupId = objTaskGroup.DevTaskGroupId;

            //_members.$scope.newTaskGroup = objTaskGroup;

            if (onSuccess) {
                onSuccess(objTaskGroup);
            }
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });
}

var dialog, form;

function preLoadModal() {
    dialog = $( "#dialog-form" ).dialog({
        autoOpen: false,
        height: 400,
        width: 350,
        modal: true,
        buttons: {
            "Create an account": addUser,
            Cancel: function() {
                dialog.dialog( "close" );
            }
        },
        close: function() {
            form[ 0 ].reset();
            //allFields.removeClass( "ui-state-error" );
        }
    });
 
    form = dialog.find( "form" ).on( "submit", function( event ) {
        event.preventDefault();
        addUser();
    });   
}

function addUser() {
    allFields.removeClass("ui-state-error");

    $("#users tbody").append("<tr>" +
    "<td>" + name.val() + "</td>" +
    "<td>" + email.val() + "</td>" +
    "<td>" + password.val() + "</td>" +
    "</tr>");
    dialog.dialog("close");

    return valid;
}
