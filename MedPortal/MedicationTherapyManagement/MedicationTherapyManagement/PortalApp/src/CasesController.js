(function (mtmPortal, _) {
    'use strict';
    mtmPortal.controller('casesController',
        function ($scope, $http, $modal) {
            $scope.itemToDeleteId = null;
            $scope.cases = [];
            $scope.userRoles = [];

            getCases();
            getUserRoles();

            //TODO: refactor into user service
            $scope.isUserInRole = function (role) {
                return $scope.userRoles.indexOf(role) > -1;
            };

            function getUserRoles() {
                $http.get('/sessionuser/sessionuserroles').success(function(data) {
                    $scope.userRoles = data;

                });
            }

            function getCases() {
                $http({ method: 'GET', url: '/Cases' }).
                    success(function(data) {
                        $scope.cases = data;
                    });

            }
            $scope.setItemToBeDeleted = function(id) {
                $scope.itemToDeleteId = id;
               
            };

            $scope.deleteCaseConfirmed = function () {
             
                $http({ method: 'POST', url: 'Cases/Delete/' + $scope.itemToDeleteId }).
                    success(function (data) {
                        
                        $scope.cases = _.without($scope.cases, _.findWhere($scope.cases, { "id": $scope.itemToDeleteId }));
                    });
            };

           });
    
}(window.mtmPortal, window._));
