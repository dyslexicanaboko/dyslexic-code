(function ($, angular) {

    configureRoutes.$inject = ["$stateProvider", "$urlRouterProvider", "$locationProvider"];

    function configureRoutes($stateProvider, $urlRouterProvider, $locationProvider) {
        //console.log("configureRoutes");

        $urlRouterProvider.otherwise("/Home"); //Kicks it back to MVC, this will tell me there was a goof
        $locationProvider.html5Mode(true);

        //Don't get confused with the MyTasks.cshtml view from MVC, you should ignore that from this point forward
        $stateProvider
            .state("myTasks", {
                url: "/MillionThings/index", //This has to match the MVC routes to a point, at least the first one
                controller: "MyTasksCtrl",
                templateUrl: "MyApp/partials/app.myTasks.html",
                resolve: {
                    //Nothing to resolve for now
                }
            })
            .state("taskGroups", {
                url: "/MillionThings/taskGroups",
                controller: "TaskGroupsCtrl",
                templateUrl: "MyApp/partials/app.taskGroups.html",
                resolve: {
                    //Nothing to resolve for now
                }
            });
    }

    configureRun.$inject = ["$rootScope", "$cookies"];

    function configureRun($rootScope, $cookies) {
        //console.log("configureRun");

        // Retrieving the UserId that was placed here by the MVC controller TasksController.cs
        $rootScope.userId = $cookies.get('UserId');

        //console.dir($rootScope.userId);
    }

    angular.module("MyApp")
           .config(configureRoutes)
           .run(configureRun);

})(jQuery, angular)