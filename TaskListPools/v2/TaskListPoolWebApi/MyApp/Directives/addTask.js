(function (angular) {

	"use strict";

    //ServerResponseHelper

	controllerDefinition.$inject = ["$scope", "$filter", "toaster", "TaskPoolSvc", "ServerResponseHelper"];

	var _members = null;

	function controllerDefinition($scope, $filter, toaster, TaskPoolSvc, ServerResponseHelper) {
	    //$scope.onEnterKeyPress = function (event) {
	    //    if (event.charCode == 13) //if enter then hit the search button
	    //        $scope.goToTaskByWorkItemId();
	    //}

        //Testing data
	    //$scope.addTaskModel = { workItemId: 16315 };
	    _members = {
	        $scope: $scope,
	        $filter: $filter,
	        //$state: $state,
	        toaster: toaster,
	        TaskPoolSvc: TaskPoolSvc,
	        ServerResponseHelper: ServerResponseHelper,
	    }

	    $scope.btnAddTask_click = btnAddTask_click;
	};

	function btnAddTask_click(task) {
	    saveTask(task, function (result) {
	        _members.$scope.onTaskSaved();

	        successMessage("Task " + result.TaskId + " created successfully.");
	    });
	}

	function successMessage(message) {
	    _members.toaster.success({ title: "Success", body: message });
	}

	function errorMessage(response) {
	    console.dir(response);

	    _members.toaster.error("Error", response.data.ExceptionMessage);
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

	function directiveDefinition() {
		return {
			//require: "ngModel",
			restrict: "E",
			scope: {
                onTaskSaved: '='
			},
			controller: controllerDefinition,
			templateUrl: "MyApp/Templates/addTask.html",
			link: function ($scope, elem, attrs, controller) {
			    $scope.addTask = function () {
			        //console.log("Linker");

			        //$scope.onTaskSaved();
			    };
			}
			//link: function (scope, element, attrs, ctrl) {

			//    function validate(value) {
			//        // To Do 2. add code to set validity here
			//        if (value > 0) {
			//            ctrl.$setValidity("task-jumper", true);

			//            scope.showError = true;
			//        }
			//        else {
			//            ctrl.$setValidity("task-jumper", false);

			//            scope.showError = false;
			//        }

			//        return value;
			//    }

			//    // To Do 3. add code to update format and parse arrays here
			//    ctrl.$formatters.push(validate);
			//    ctrl.$parsers.push(validate);
			//}
		};
	}
	
	angular.module("MyApp.Directives")
		   .directive("addTask", directiveDefinition);
})(angular);