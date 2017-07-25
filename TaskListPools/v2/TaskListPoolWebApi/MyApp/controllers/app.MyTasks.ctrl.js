(function ($, angular) {

    "use strict";

    myTasksCtrl.$inject = ["$scope", "$filter", "toaster", "TaskPoolSvc", "ServerResponseHelper"];

    var _members = null;

    function myTasksCtrl($scope, $filter, toaster, TaskPoolSvc, ServerResponseHelper) {
        console.log("myTasksCtrl");
        
        //Storing all of the important stuff as a globally accessible object
        _members = {
            $scope: $scope,
            $filter: $filter,
            //$state: $state,
            toaster: toaster,
            TaskPoolSvc: TaskPoolSvc,
            ServerResponseHelper: ServerResponseHelper,
        }

        $scope.message = "This is just a test value";
        $scope.btnAddTask_click = btnAddTask_click;
        $scope.btnTaskEdit_click = btnTaskEdit_click;
        $scope.btnTaskDelete_click = btnTaskDelete_click;

        var fnLoad = null;
        var args = null;

        if ($scope.isFromOpenModalWindow != undefined && $scope.isFromOpenModalWindow === true) {
            fnLoad = loadTaskGroupGrid;
            args = $scope.openModalWindowArgs;
        }
        else {
            fnLoad = loadGrid;
        }

        $scope.refreshGrid = fnLoad;

        fnLoad(args);
    }

    function btnAddTask_click(task) {
        //console.log("btnTaskAdd_click");
        //console.dir(task);

        saveTask(task, function (objTask) {
            //console.dir(objTask);

            //_members.$scope.isExistingTask = true;

            //First time around
            //setupDevTaskSessionModelUsingDevTaskModel();

            loadGrid();

            successMessage("Task " + objTask.TaskId + " created successfully.");
        });
    }

    function btnTaskEdit_click(task) {
        //console.log("btnTaskEdit_click");
        //console.dir(task);
        
        saveTask(task, function (objTask) {
            successMessage("Task updated successfully.");
        });
    }

    function btnTaskDelete_click(taskId) {
        if (!confirm("Are you sure you want to delete this task?")) {
            return;
        }

        _members.TaskPoolSvc
                .tasks.delete(taskId)
                .then(function (result) {
                    _members.ServerResponseHelper.getData(result);

                    loadGrid();
                    //_members.$state.go("myTasks");
                })
                .catch(function (response) {
                    errorMessage(response);
                });
    }

    function saveTask(task, onSuccess) {
        _members.TaskPoolSvc
                .tasks.save(task)
                .then(function (result) {
                    var objTask = _members.ServerResponseHelper.getData(result);

                    //console.log("getData.promise");
                    //console.dir(objTask);

                    //_members.$scope.devTaskId = objTask.DevTaskId;

                    //_members.$scope.newTask = objTask;

                    if (onSuccess) {
                        onSuccess(objTask);
                    }
                })
                .catch(function (response) {
                    errorMessage(response);
                });
    }

    function successMessage(message) {
        _members.toaster.success({ title: "Success", body: message });
    }

    function errorMessage(response) {
        console.dir(response);

        _members.toaster.error("Error", response.data.ExceptionMessage);
    }

    function loadGrid(argNotUsed) {
        _members.TaskPoolSvc
            .tasks.getAll()
            .then(function (result) {
                _members.$scope.taskGridData = _members.ServerResponseHelper.getData(result);

                //console.log("loadGrid.promise");
                //console.dir(scope.plots);
            })
            .catch(function (response) {
                //Not sure how to handle errors yet
                console.log(response);
            });
    }

    function loadTaskGroupGrid(taskGroupId) {
        _members.TaskPoolSvc
            .tasks.getAll(taskGroupId)
            .then(function (result) {
                _members.$scope.taskGroupGridData = _members.ServerResponseHelper.getData(result);

                //console.log("loadGrid.promise");
                //console.dir(scope.plots);
            })
            .catch(function (response) {
                //Not sure how to handle errors yet
                console.log(response);
            });
    }

    angular
        .module("MyApp.Controllers")
        .controller("MyTasksCtrl", myTasksCtrl);

})(jQuery, angular);