(function (angular) {

    "use strict";

    function serverResponseHelper() {

        return {

            getData: function (response) {

                var data;

                //If you didn't return anything, then data will be null for sure
                if (!response.data || response.data === null) {
                    return null;
                }

                if (response.data.d !== undefined) {
                    // if we have web methods on the server
                    data = response.data.d;
                }
                else {
                    // if we use Web API or $httpBackend
                    data = response.data;
                }

                if (data instanceof Object) {
                    // if we use Web API or $httpBackend
                    return data;
                }
                else {
                    // if we have web methods on the server then the reponse will be a string in Json format
                    return JSON.parse(data);
                }
            },

            getError: function (response) {

                var error;

                if (response.data === undefined && response.message !== undefined) {
                    // internal javascript error
                    error = response.message;
                }
                else if (response.data !== undefined && response.data.Message !== undefined) {
                    // if we have web methods on the server
                    error = response.data.Message;
                }
                else if (response.data !== undefined && response.data.error !== undefined) {
                    // if we use Web API or $httpBackend
                    error = response.data.error;
                }
                else {
                    error = "An internal script error has ocurred. Please contact your administrator.";
                }

                // return an error object
                return { error: true, errorMessage: error };
            }
        };
    }

    angular
        .module("MyApp.Services")
        .factory("ServerResponseHelper", serverResponseHelper);

})(angular);