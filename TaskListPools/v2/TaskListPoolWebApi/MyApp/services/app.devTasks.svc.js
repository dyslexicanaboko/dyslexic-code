(function ($, angular) {

    devTasksFactory.$inject = ["$rootScope", "$http"];

    //Need to learn how to pass in a custom object as parameters
    //For now the parameters are hard coded
    function devTasksFactory($rootScope, $http) {
        //console.log("devTasksFactory");
        
        return {
            getGraphData: function (dateRange) {
                //console.log("devTasksFactory.getGraphData.userId:" + $rootScope.userId);

                return $http.get(getBasePath("DevTaskHoursPerDayGraphData") + getFilterQsp($rootScope.userId, dateRange));
            },
            getSummaryGridData: function (dateRange) {
                return $http.get(getBasePath("GetTaskSummary") + getFilterQsp($rootScope.userId, dateRange));
            },
            getTaskIdByTfsDevelopmentTaskId: function (tfsDevTaskId) {
                return $http.get(getBasePath("GetTaskIdByTfsDevelopmentTaskId") + "?UserId=" + $rootScope.userId + "&tfsDevTaskId=" + tfsDevTaskId);
            },
            getTaskLoaded: function (devTaskId) {
                return $http.get(singleArgument("GetTaskLoaded", devTaskId));
            },
            taskSave: function (devTask) {
                devTask.UserId = $rootScope.userId;

                return $http.put(getBasePath("TaskSave"), devTask);
            },
            sessionSave: function (devTaskSession) {
                return $http.put(getBasePath("SessionSave"), devTaskSession);
            },
            getTaskSessions: function (devTaskId) {
                return $http.get(singleArgument("GetTaskSessions", devTaskId));
            },
            taskDelete: function (devTaskId) {
                return $http.delete(singleArgument("TaskDelete", devTaskId));
            }
        };
    }

    function getFilterQsp(userId, dateRange) {
        //"?UserId=" + $rootScope.userId + "&IsActive=&DateStart=2015-10-01&DateEnd=2015-10-31"
        //console.log("getFilterQsp");
        //console.dir(dateRange);

        return new String().concat(
            "?UserId=" + userId,
            "&IsActive=",
            "&DateStart=" + dateRange.start.toISOString(),
            "&DateEnd=" + dateRange.end.toISOString());
    }

    function getResult(q, promise) {
        var deferred = q.defer();

        setTimeout(function () {
            deferred.resolve(promise);
        }, 3000);

        return deferred.promise;
    }

    function singleArgument(action, argument) {
        return getBasePath(action) + "/" + argument;
    }

    function getBasePath(action) {
        return "/api/DevTask/" + action;
    }

    angular.module("MyApp.Services")
        .factory("DevTasks", devTasksFactory);

})(jQuery, angular);