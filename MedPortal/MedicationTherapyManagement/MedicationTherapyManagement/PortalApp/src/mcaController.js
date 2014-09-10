(function (mtmPportal, _) {
    'use strict';
    mtmPortal.controller('mcaController',
        function($scope, $http, ngDialog) {
            $scope.mcaData = angular.fromJson($scope.ngDialogData);
            $scope.mcaParams = {};
            $scope.providers = [];
            $scope.userRoles = [];
            $scope.case = {};

            function fetchProviders() {
                $http.get('/provider/Index').success(function(data) {
                    $scope.providers = data;
                    getCase();
                 
                });
            }

            function getUserRoles() {
                $http.get('/sessionuser/sessionuserroles').success(function(data) {
                    $scope.userRoles = data;

                });
            };

            

            function getCase() {
                if ($scope.mcaData.Value[0].CaseId !== 'null') {
                    var caseId = $scope.mcaData.Value[0].CaseId;
                    $http.get('/Cases/Details/' + caseId).success(function(data) {
                        $scope.case = data;
                        $scope.assignedCaseProvider = _.where($scope.providers, { Id: data.AssignedProvider });
                      
                    });
                }
            }


            $scope.close = function() {
                _.each($scope.$parent.triggerReport, function(num) {

                    if (num.Key === $scope.mcaData.Key) {
                        num.Value[0].mcaCreated = true;
                        num.Value[0].Reviewed = true;
                        // $scope.$apply();
                    }
                });
                ngDialog.close();
            };
            $scope.saveMca = function() {
                constructParams();

                $http({ method: 'POST', url: '/Cases/Save', data: { "mcaParams": $scope.mcaParams, "mcaClaims": $scope.mcaData } }).
                    success(function() {
                        $scope.close();

                        //  $location.path('cases');
                    });
            };
            $scope.displayDetail = function(e) {

                confirm(e);
            };

            function constructParams() {
                $scope.mcaParams.assignedProvider = $scope.assignedProvider;
                $scope.mcaParams.DOB = $scope.dob;
                $scope.mcaParams.problemList = $scope.problemList;
                $scope.mcaParams.recomendationList = $scope.recomendationList;
                $scope.mcaParams.mcaData = $scope.mcaData;
                $scope.mcaParams.client = $scope.$parent.currentClient;
                $scope.mcaParams.reportId = $scope.$parent.ReportId;
                $scope.mcaData.Value[0].Reviewed = true;
            };
            fetchProviders();
            getUserRoles();
           
          

        });
})(window.mtmPortal, window._);