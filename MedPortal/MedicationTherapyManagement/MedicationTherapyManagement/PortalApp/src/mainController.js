(function (mtmPortal, _) {
    'use strict';
    mtmPortal.controller('mainController',
        function($scope, $http, $location) {
           $http.get("/Portal/Clients").success(function(data, status) {
               $scope.clients = data;
           });
 
           $scope.currentClient = { "ClientName": " " };

           $scope.clientDisabled = function () {
               return $location.path().indexOf('triggerReport')>0;


           };

        });
}(window.mtmPortal, window._));
