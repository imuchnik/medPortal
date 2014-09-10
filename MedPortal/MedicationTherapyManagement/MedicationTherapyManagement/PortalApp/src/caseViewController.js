(function (mtmPortal, _) {
    'use strict';
    mtmPortal.controller('caseViewController',['userService','providerService', '$scope', '$routeParams', '$http', '$location',
        function (userService, providerService, $scope, $routeParams, $http, $location) {
            $scope.caseId = $routeParams.id;
            $scope.case = {};
            $scope.caseClaims = {};
            $scope.notes = [];
            $scope.userRoles = [];

            //TODO refactor into a service
            $scope.isUserInRole = function (role) {
                return $scope.userRoles.indexOf(role) > -1;
            };

            
            userService.getUserRoles()
                .then(function (response) {
                 $scope.userRoles = response.data;
                });

            providerService.fetchProviders()
                .then(function(response) {
                    $scope.providers = response.data;
                    viewCase();
                });
           

            function viewCase() {
                $http({ method: 'GET', url: '/Cases/details/' + $scope.caseId }).
                    success(function(data) {
                        $scope.case = data;
                        $scope.assignedProvider = _.findWhere($scope.providers, { "Id": $scope.case.AssignedProvider });
                    });

                $http({ method: 'GET', url: '/Cases/ClaimDetails/' + $scope.caseId }).
                    success(function(data) {
                        $scope.caseClaims = data;
                    });

                $http({ method: 'GET', url: '/Notes/Index/' + $scope.caseId }).
                    success(function(data) {
                        $scope.notes = data;
                    });

            };
           

            $scope.updateCaseStatus = function(status) {

                $http({ method: 'POST', url: '/Cases/Update', data: { "field": "status", "value": status, "caseId": $scope.caseId } }).
                    success(function() {
                        $scope.case.Status = status;
                        if (status === 'Declined' || status === 'Completed')
                            $location.path('cases');
                    });
            };

            $scope.closeCase = function() {
                $location.path('cases');
            };

            $scope.assignProvider = function () {
                $http({ method: 'POST', url: '/Cases/Update', data: { "field": "AssignedProvider", "value": $scope.case.AssignedProvider.Id, "caseId": $scope.caseId } }).
                    success(function() {
                        $scope.successMessage = "Provider assigned successfully";
                        console.log($scope.successMessage);
                        $scope.case.Status = "Assigned-Pending Acceptance";
                        $scope.assignedProvider = $scope.case.AssignedProvider;
                    

                    });
            };
            $scope.addNote = function() {
                $http({ method: 'POST', url: '/Notes/Save', data: { "noteText": $scope.noteText, "caseId": $scope.caseId } }).
               success(function () {
                   $scope.notes.push({ "id": -1, "NoteText": $scope.noteText, "caseId": $scope.caseId, "CreateTime": new Date(), "Author": "You" });
                   $scope.noteText = "";

               });
            };

        }]);
})(window.mtmPortal, window._);