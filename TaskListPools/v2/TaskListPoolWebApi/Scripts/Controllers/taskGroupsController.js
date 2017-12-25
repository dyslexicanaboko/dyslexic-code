var taskGroupController = {};

(function (context) {
    var _tableTemplateId = null;
    var _tableModelId = null;

    context.btnEditToggle_click = function btnEditToggle_click(taskGroupId, editMode) {
        var r = $("#taskGroup_divReg" + taskGroupId);
        var e = $("#taskGroup_divEdit" + taskGroupId);
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
    };

    function getJQueryObjects(taskGroupId) {
        return {
            Name: { 
                label: $("#taskGroup_lblName" + taskGroupId),
                text: $("#taskGroup_txtName" + taskGroupId)
            },
            Description: {
                label: $("#taskGroup_lblDescription" + taskGroupId),
                text: $("#taskGroup_txtDescription" + taskGroupId)
            },
            CreatedOn: $("#taskGroup_lblCreatedOn" + taskGroupId),
            TaskCount: $("#taskGroup_lblTaskCount" + taskGroupId)
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

    context.page_load = function page_load(tableTemplateId, tableModelId) {
        _tableTemplateId = tableTemplateId;
        _tableModelId = tableModelId;

        getTaskPoolService()
            .taskGroups
            .getAll()
            .then(function (response) {
                var arr = response.data;

                addModelsToTable(tableTemplateId, tableModelId, arr);
            })
            .catch(function (response) {
                toastMessages.errorHttp(response);
            });

        preLoadModal();
    };

    function addModelsToTable(tableTemplateId, tableModelId, models) {
        var templateRow = $("#" + tableTemplateId + " #taskGroup_row_id0");

        _.forEach(models, function (model) {
            //Clone the template row
            var tr = templateRow.clone();

            //Replace the id attribute accordingly
            tr.attr("id", "taskGroup_row" + model.TaskGroupId);

            //Mass replace all of the ids with the new id
            var str = tr.html().replace(/_id0/g, model.TaskGroupId);

            str = str.replace("?TaskGroupId=0", "?TaskGroupId=" + model.TaskGroupId);

            tr.html(str);

            //Add the cloned row to the table
            $("#" + tableModelId + " tbody").append(tr);

            //Update the html normally
            setModelHtml(model);

            //console.dir(tr);
        });
    }

    context.btnTaskGroupAdd_click = function btnTaskGroupAdd_click() {
        var task = getModel(0);

        saveTaskGroup(task, function (objTaskGroup) {
            toastMessages.success("Group " + objTaskGroup.TaskGroupId + " created successfully");

            //Add the new model to the table
            addModelsToTable(_tableTemplateId, _tableModelId, [objTaskGroup]);

            //Clear out the input form
            $("#taskGroup_txtName0").val("");
            $("#taskGroup_txtDescription0").val("");
        });
    };

    context.btnTaskGroupEdit_click = function btnTaskGroupEdit_click(taskGroupId) {
        var task = getModel(taskGroupId);

        saveTaskGroup(task, function (objTaskGroup) {
            toastMessages.success("Group updated successfully");

            context.btnEditToggle_click(taskGroupId, false);

            setModelHtml(task);
        });
    };

    context.btnTaskGroupDelete_click = function btnTaskGroupDelete_click(taskGroupId) {
        if (!confirm("Are you sure you want to delete this group?")) {
            return;
        }

        getTaskPoolService()
            .taskGroups
            .delete(taskGroupId)
            .then(function (result) {
                $("#taskGroup_row" + taskGroupId).remove();
            })
            .catch(function (response) {
                toastMessages.errorHttp(response);
            });
    };

    context.btnShowTaskAddModal_click = function btnShowTaskAddModal_click(taskGroupId, url) {
        var div = $("#divTasksForGroup");

        var m = getModel(taskGroupId);

        //_dialog.attr("title", m.Name).dialog();

        //Every time I enable this it doesn't allow for the div to be populated
        //$("#lblTaskGroupDescription").text(m.Description);

        $.get(url, function (data) {
            div.replaceWith(data);
        });

        showModal();
    };

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

    var _dialog, _form;

    function preLoadModal() {
        _dialog = $( "#dialog-form" ).dialog({
            autoOpen: false,
            height: 400,
            width: "auto",
            modal: true,
            close: function() {
                _dialog.dialog("close");
            }
        });
 
        _form = _dialog.find( "form" ).on( "submit", function( event ) {
            event.preventDefault();
        });   
    }

    function showModal() {
        _dialog.dialog("open");
    }
})(taskGroupController);