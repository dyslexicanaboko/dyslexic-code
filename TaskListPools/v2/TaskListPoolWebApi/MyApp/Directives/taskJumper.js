(function (angular) {

	"use strict";

    //ServerResponseHelper

	taskJumperController.$inject = ["$scope", "$location", "DevTasks", "ServerResponseHelper"];

	function taskJumperController($scope, $location, DevTasks, ServerResponseHelper) {
	    $scope.onEnterKeyPress = function (event) {
	        if (event.charCode == 13) //if enter then hit the search button
	            $scope.goToTaskByWorkItemId();
	    }

        //Testing data
	    //$scope.taskJumperModel = { workItemId: 16315 };

	    $scope.goToTaskByWorkItemId = function () {
	        //console.log("taskJumper");

	        //For debug:
	        //$scope.taskJumperModel.workItemId = 13934;

	        //The incoming TFS Work Item ID needs to be translated into a Dev Task ID
	        var intWorkItemId = $scope.taskJumperModel.workItemId;
	        
            //Call to the DevTasks API which returns a promise
	        DevTasks.getTaskIdByTfsDevelopmentTaskId(intWorkItemId)
                    .then(function (result) {
                        var intDevTaskId = ServerResponseHelper.getData(result);

                        //console.dir(intDevTaskId);

	                    $location.path('/Tasks/mySessions')
                                 .search({ devTaskId: intDevTaskId, tfsWorkItemId: intWorkItemId })
                                 .replace();

	                    //console.log($location.path());
                    })
                    .catch(function (response) {
                        //Not sure how to handle errors yet
                        console.log(response);
                    });
	    }
	};

	function taskJumperDirective() {
		return {
			require: "ngModel",
			restrict: "E",
			scope: {},
			controller: taskJumperController,
			templateUrl: "MyApp/Templates/taskJumper.html"
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
		   .directive("taskJumper", taskJumperDirective);
})(angular);