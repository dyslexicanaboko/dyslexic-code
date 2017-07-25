(function (angular) {

    "use strict";

    //This service exists only to expose the underscore.js library which attaches itself to the window object
    //http://stackoverflow.com/questions/14968297/use-underscore-inside-angular-controllers
    function underscoreServiceWrapper() {
        //console.log("underscoreServiceWrapper");

        //This is what underscore does, so effectively the whole purpose of this service 
        //is to change the syntax BACK to just "_.*"
        return window._;
    }

    angular
        .module("MyApp.Services")
        .factory("_", underscoreServiceWrapper);

})(angular);