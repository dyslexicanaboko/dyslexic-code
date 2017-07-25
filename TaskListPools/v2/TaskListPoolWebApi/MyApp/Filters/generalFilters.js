(function (angular) {
    'use strict';
    
    angular.module('MyApp.Filters')
		.filter('percentage', ['$filter', function ($filter) {
		    return function (input, decimals) {
		        return $filter('number')(input * 100, decimals) + '%';
		    };
		}])
        .filter('zpad', function () {
            return function (input, n) {
                if (input === undefined)
                    input = ""
                if (input.length >= n)
                    return input
                var zeros = "0".repeat(n);
                return (zeros + input).slice(-1 * n)
            };
        });
})(angular);