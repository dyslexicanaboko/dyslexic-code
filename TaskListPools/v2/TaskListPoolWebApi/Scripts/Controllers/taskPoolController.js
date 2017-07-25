function initComponent() {
    var v = new Vue({
        el: '#inputAddTask',
        components: {
            'v-add-task': {
                template: '<div style="float:left;"><input type="text" id="txtBody" v-model="newTask.Body" style="width:100%;" />&nbsp;</div><div style="float:left;"><button id="btnAddTask" v-on:click="newTask.TaskId = 0; btnAddTask_click(newTask);" class="btn btn-info btn-md"><span class="glyphicon glyphicon-plus-sign"></span></button></div><div style="clear:both;" />'
            }
        }
    });
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

            loadGrid(arr);
        })
        .catch(function (response) {
            toastMessages.errorHttp(response);
        });
}

function btnAddTask_click(task) {
    //console.log("btnTaskAdd_click");
    //console.dir(task);

    saveTask(task, function (objTask) {
        //console.dir(objTask);

        //_members.$scope.isExistingTask = true;

        //First time around
        //setupDevTaskSessionModelUsingDevTaskModel();

        loadTasks();

        successMessage("Task " + objTask.TaskId + " created successfully");
    });
}

function btnTaskEdit_click(task) {
    //console.log("btnTaskEdit_click");
    //console.dir(task);

    saveTask(task, function (objTask) {
        successMessage("Task updated successfully");
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
            //result.data;

            loadTasks();
            //_members.$state.go("myTasks");
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

//function loadTaskGroupGrid(taskGroupId) {
//    getTaskPoolService()
//        .tasks.getAll(taskGroupId)
//        .then(function (result) {
//            _members.$scope.taskGroupGridData = _members.ServerResponseHelper.getData(result);

//            //console.log("loadGrid.promise");
//            //console.dir(scope.plots);
//        })
//        .catch(function (response) {
//            //Not sure how to handle errors yet
//            console.log(response);
//        });
//}

