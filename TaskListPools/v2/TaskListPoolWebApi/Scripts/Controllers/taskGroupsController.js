function btnEditToggle_click(taskId, editMode) {
    var r = $("#divReg" + taskId);
    var e = $("#divEdit" + taskId);
    var l = $("#lblBody" + taskId);
    var t = $("#txtBody" + taskId);


    if (editMode === true) {
        r.hide();
        e.show();
        l.hide();
        t.show();
    }
    else {
        r.show();
        e.hide();
        l.show();
        t.hide();
    }
}

function getTaskObject(taskId) {
    var tb = $("#txtBody" + taskId);
    var tc = $("#lblCreatedOn" + taskId);

    return {
        TaskId: taskId,
        Body: tb.val(),
        CreatedOn: tc.text()
    };
}

function setTaskHtml(task) {
    var tl = $("#lblBody" + task.TaskId);
    var tc = $("#lblCreatedOn" + task.TaskId);

    tl.text(task.Body);
    tc.text(task.CreatedOn);
}

function loadGrid(tasks) {
    var v = new Vue({
        el: '#tblData',
        data: {
            tasks: tasks
        }
    });
}

function appendPropertyToObject(item) {
    item.taskRowIsBeingEdited = false;
}

function loadTasks() {
    getTaskPoolService()
        .tasks
        .getAll()
        .then(function (response) {
            var arr = response.data;

            _.forEach(arr, appendPropertyToObject);

            //loadGrid(arr);
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });
}

function btnTaskAdd_click() {
    //console.log("btnTaskAdd_click");
    //console.dir(task);
    var task = getTaskObject(0);

    saveTask(task, function (objTask) {
        toastMessages.success("Task " + objTask.TaskId + " created successfully");

        var addRow = $("#tblTasks tr:last");

        //Attempt to get the previous row, however in the edge case of an empty table this might not work
        var lastRow = addRow.prev();

        //If the table was empty, then just reload the page this first time
        if (lastRow === undefined) {
            location.reload();
        }

        //Clone the last row
        var tr = lastRow.clone();

        //Find the existing id and remove the text part to make it a number
        var find_id = tr.attr("id").replace("row", "");
        var new_id = objTask.TaskId;

        //Mass replace all of the ids with the new id
        tr.html(tr.html().replace(find_id, new_id));

        //Add the cloned row before the last row (which is strictly the add row)
        lastRow.after(tr);

        //Update the html normally
        setTaskHtml(objTask);

        $("#txtBody0").val("");
    });
}

function btnTaskEdit_click(taskId) {
    //console.log("btnTaskEdit_click");
    //console.dir(task);
    var task = getTaskObject(taskId);

    saveTask(task, function (objTask) {
        toastMessages.success("Task updated successfully");

        btnEditToggle_click(taskId, false);

        setTaskHtml(task);
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

            //console.log("getData.promise");
            //console.dir(objTask);

            //_members.$scope.devTaskId = objTask.DevTaskId;

            //_members.$scope.newTask = objTask;

            if (onSuccess) {
                onSuccess(objTask);
            }
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });
}

