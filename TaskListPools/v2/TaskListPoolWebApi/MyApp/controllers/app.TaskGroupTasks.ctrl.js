(function ($, angular) {

    "use strict";
    
    controllerDefinition.$inject = ["$scope", "$filter", "toaster", "TaskPoolSvc", "ServerResponseHelper"];

    var _members = null;

    function controllerDefinition($scope, $filter, toaster, TaskPoolSvc, ServerResponseHelper) {
        console.log("Task group tasks");
        
        //Storing all of the important stuff as a globally accessible object
        _members = {
            //$broadcast: $broadcast,
            $scope: $scope,
            $filter: $filter,
            //$state: $state,
            toaster: toaster,
            TaskPoolSvc: TaskPoolSvc,
            ServerResponseHelper: ServerResponseHelper,
        }

        //$scope.dateValues = getDateValues();
        //$scope.dateFilter = $filter('date');
        //$scope.message = "This is just a test value";
        //$scope.btnAddTaskGroup_click = btnAddTaskGroup_click;
        //$scope.btnTaskGroupEdit_click = btnTaskGroupEdit_click;
        //$scope.btnTaskGroupDelete_click = btnTaskGroupDelete_click;
        //$scope.btnTaskGroupLists_click = btnTaskGroupLists_click;
        ////$scope.testFunction = function () { alert("Is this working?"); };
        
        //loadGrid();
    }

    function btnTaskGroupLists_click() {
        //alert("Modal should show here.");
        //_members.$scope.showModal = true;
        //console.dir($ctrl);
        _members.$scope.$broadcast("openModalWindow");
    }

    function btnAddTaskGroup_click(taskGroup) {
        //console.log("btnTaskAdd_click");
        //console.dir(taskGroup);

        saveTask(taskGroup, function (objTaskGroup) {
            //console.dir(objTaskGroup);

            //_members.$scope.isExistingTask = true;

            //First time around
            //setupDevTaskSessionModelUsingDevTaskModel();

            loadGrid();

            successMessage("Task Group " + objTaskGroup.TaskGroupId + " created successfully.");
        });
    }

    function btnTaskGroupEdit_click(taskGroup) {
        //console.log("btnTaskGroupEdit_click");
        //console.dir(taskGroup);
        
        saveTask(taskGroup, function (objTaskGroup) {
            successMessage("Task updated successfully.");
        });
    }

    function btnTaskGroupDelete_click(taskGroupId) {
        if (!confirm("Are you sure you want to delete this task?")) {
            return;
        }

        _members.TaskPoolSvc
                .taskGroups.delete(taskGroupId)
                .then(function (result) {
                    _members.ServerResponseHelper.getData(result);

                    loadGrid();
                    //_members.$state.go("myTasks");
                })
                .catch(function (response) {
                    errorMessage(response);
                });
    }

    function saveTask(taskGroup, onSuccess) {
        _members.TaskPoolSvc
                .taskGroups.save(taskGroup)
                .then(function (result) {
                    var objTaskGroup = _members.ServerResponseHelper.getData(result);

                    //console.log("getData.promise");
                    //console.dir(objTaskGroup);

                    //_members.$scope.devTaskId = objTaskGroup.DevTaskId;

                    //_members.$scope.newTask = objTaskGroup;

                    if (onSuccess) {
                        onSuccess(objTaskGroup);
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

    function loadGrid() {
        _members.TaskPoolSvc
            .taskGroups.getAll()
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

    function getDateValues() {
        var today = new Date();

        var start = new Date(today.getFullYear(), today.getMonth(), 1);

        //Setting the day to zero will get you the last day of the month, because javascript
        var end = new Date(today.getFullYear(), today.getMonth() + 1, 0);

        //console.dir(end);

        return {
            start: start,
            end: end
        };
    }

    angular
        .module("MyApp.Controllers")
        .controller("TaskGroupTasks", controllerDefinition);

})(jQuery, angular);