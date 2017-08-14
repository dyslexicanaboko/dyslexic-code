function btnEditToggle_click(taskId, editMode) {
    var r = $("#divReg" + taskId);
    var e = $("#divEdit" + taskId);
    var m = getJQueryObjects(taskId);


    if (editMode === true) {
        r.hide();
        e.show();
        m.Body.label.hide();
        m.Body.text.show();
    }
    else {
        r.show();
        e.hide();
        m.Body.label.show();
        m.Body.text.hide();
    }
}

function getJQueryObjects(taskId) {
    return {
        Body: { 
            label: $("#lblBody" + taskId),
            text: $("#txtBody" + taskId) 
        },
        CreatedOn: $("#lblCreatedOn" + taskId)
    };
}

function getModel(taskId) {
    var m = getJQueryObjects(taskId);

    return {
        TaskId: taskId,
        Body: m.Body.text.val(),
        CreatedOn: m.CreatedOn.text()
    };
}

function setModelHtml(task) {
    var m = getJQueryObjects(task.TaskId);

    m.Body.label.text(task.Body);
    m.Body.text.val(task.Body);
    m.CreatedOn.text(task.CreatedOn);
}

function page_load() {
    getTaskPoolService()
        .tasks
        .getAll()
        .then(function (response) {
            var arr = response.data;

            addModelsToTable(arr);
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });
}

function addModelsToTable(models) {
    var templateRow = $("#row_id0");

    _.forEach(models, function (model) {
        //Clone the template row
        var tr = templateRow.clone();

        //Replace the id attribute accordingly
        tr.attr("id", "row" + model.TaskId);

        //Mass replace all of the ids with the new id
        tr.html(tr.html().replace(/_id0/g, model.TaskId));

        //Add the cloned row to the table
        $("#tblModel tbody").append(tr);

        //Update the html normally
        setModelHtml(model);

        //console.dir(tr);
    });
}

function btnTaskAdd_click() {
    //console.log("btnTaskAdd_click");
    //console.dir(task);
    var task = getModel(0);

    saveTask(task, function (objTask) {
        toastMessages.success("Task " + objTask.TaskId + " created successfully");

        //Add the new model to the table
        addModelsToTable([objTask]);

        $("#txtBody0").val("");
    });
}

function btnTaskEdit_click(taskId) {
    //console.log("btnTaskEdit_click");
    //console.dir(task);
    var task = getModel(taskId);

    saveTask(task, function (objTask) {
        toastMessages.success("Task updated successfully");

        btnEditToggle_click(taskId, false);

        setModelHtml(task);
    });
}

function btnTaskDelete_click(taskId) {
    if (!confirm("Are you sure you want to delete this task?")) {
        return;
    }

    getTaskPoolService()
        .tasks
        .delete(taskId)
        .then(function (result) {
            $("#row" + taskId).remove();
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });
}

function saveTask(task, onSuccess) {
    getTaskPoolService()
        .tasks
        .save(task)
        .then(function (result) {
            var objTask = result.data;

            if (onSuccess) {
                onSuccess(objTask);
            }
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });
}
