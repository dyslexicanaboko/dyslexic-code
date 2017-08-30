var taskController = {};

(function (context) { 
    context.btnEditToggle_click = function btnEditToggle_click(taskId, editMode) {
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

    context.page_load = function page_load(tableTemplateId, tableModelId, taskGroupId) {
        console.log("Load tasks for Group: " + taskGroupId);

        var svc = getTaskPoolService().tasks;

        var f = null;

        if(taskGroupId === 0) {
            f = function() { return svc.getAll(); };
        }
        else {
            f = function() { return svc.getByTaskGroupId(taskGroupId); };
        }
    
        f()
        .then(function (response) {
            var arr = response.data;

            addModelsToTable(tableTemplateId, tableModelId, arr);
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });
    }

    function addModelsToTable(tableTemplateId, tableModelId, models) {
        var templateRow = $("#" + tableTemplateId + " #row_id0");

        _.forEach(models, function (model) {
            //Clone the template row
            var tr = templateRow.clone();

            //Replace the id attribute accordingly
            tr.attr("id", "row" + model.TaskId);

            //Mass replace all of the ids with the new id
            tr.html(tr.html().replace(/_id0/g, model.TaskId));

            //Add the cloned row to the table
            $("#" + tableModelId + " tbody").append(tr);

            //Update the html normally
            setModelHtml(model);

            //console.dir(tr);
        });
    }

    context.btnTaskAdd_click = function btnTaskAdd_click() {
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

    context.btnTaskEdit_click = function btnTaskEdit_click(taskId) {
        //console.log("btnTaskEdit_click");
        //console.dir(task);
        var task = getModel(taskId);

        saveTask(task, function (objTask) {
            toastMessages.success("Task updated successfully");

            context.btnEditToggle_click(taskId, false);

            setModelHtml(task);
        });
    }

    context.btnTaskDelete_click = function btnTaskDelete_click(taskId) {
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
})(taskController);