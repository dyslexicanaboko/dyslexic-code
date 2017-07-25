(function (angular) {

    "use strict";

    controllerDefinition.$inject = ["$scope", "$uibModal", "$log", "$document", "$attrs"];

    var _members = null;

    function controllerDefinition($scope, $uibModal, $log, $document, $attrs) {
        console.log("modal control");

        $scope.bodyTemplateUrl = $attrs.bodyTemplateUrl;
        $scope.bodyController = $attrs.bodyController;

        $scope.animationsEnabled = true;
        $scope.open = open;
        $scope.toggleAnimation = function () {
            $scope.animationsEnabled = !$scope.animationsEnabled;
        };

        $scope.$on("openModalWindow", openModalWindow_handler);
        $scope.btnOk_Click = btnOk_Click;
        $scope.btnCancel_Click = btnCancel_Click;
        //$scope.testFunction = function () { alert("Yaaaaay"); };

        //alert($scope.bodyTemplateUrl);
        console.dir($attrs);

        _members = {
            $scope: $scope,
            $uibModal: $uibModal,
            $document: $document
        };
    }

    function openModalWindow_handler(e, args) {
        console.log("openModalWindow_handler");
        //alert("Did it work?");
        _members.$scope.openModalWindowArgs = args;
        _members.$scope.isFromOpenModalWindow = true;
        _modalInstance = open();
    }

    var _modalInstance;

    function open(size, parentSelector) {
        var $scope = _members.$scope;
        var $uibModal = _members.$uibModal;
        var $document = _members.$document;

        var parentElem = parentSelector ?
          angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: $scope.bodyTemplateUrl, //'MyApp/Templates/test.html',
            controller: $scope.bodyController,
            //controllerAs: '$scope',
            scope: $scope,
            size: size,
            appendTo: parentElem
        });

        //Get a private reference to this
        _modalInstance = modalInstance;

        modalInstance.result.then(function () {
            //$scope.selected = selectedItem;
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
        });

        return modalInstance;
    }

    function btnOk_Click() {
        _modalInstance.close();
    }

    function btnCancel_Click() {
        _modalInstance.dismiss('cancel');
    }

    function directiveDefinition() {
        return {
            //require: "ngModel",
            restrict: "E",
            template: '', //You want this to be an empty string because the goal here isn't the directive, but the modal
            scope: {
                bodyTemplateUrl: '@' , //The @ symbol is one way binding
                headerTitle: '@'
            },
            link: function (scope, element, attrs) {
                //The attributes are available here, but the scope won't be available in the controller...
                //You have to access to the attributes service in the controller

                //scope.bodyTemplateUrl = attrs.bodyTemplateUrl; //Just an equal sign is short hand for using the same property name as the binding name
                //scope.wtf = attrs.wtf;

                //scope.$watch('bodyTemplateUrl', function (newVal) {
                //    console.log('bodyTemplateUrl', newVal);
                //});
            },
            controller: controllerDefinition
            //templateUrl: "MyApp/Templates/test.html"
            //link: function ($scope, elem, attrs, controller) {
            //    //$scope.addTask = function () {
            //    //    console.log("Linker");

            //    //    //$scope.onTaskSaved();
            //    //};
            //}
        };
    }

    angular
        .module("MyApp.Directives")
        .controller("modalController", controllerDefinition)
        .directive("tlpModal", directiveDefinition);
})(angular);