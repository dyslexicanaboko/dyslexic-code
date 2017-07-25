(function (angular) {

    "use strict";

    //This is a better solution: http://fairwaytech.com/2014/03/making-c-enums-available-within-javascript/
    function enumsServiceWrapper() {
        var enums = Object.freeze({
            //This is a literal copy of this: FSRIntranetServiceWcfLib.Business.TaskTracker.DevTaskSession.cs :: Operation enumeration
            DevTaskSession: {
                Operation: { 
                    None: 0,
                    Add: 1,
                    Edit: 2,
                    Delete: 3
                }
            }
        });

        return enums;
    }

    angular
        .module("MyApp.Services")
        .factory("EnumSvc", enumsServiceWrapper);

})(angular);