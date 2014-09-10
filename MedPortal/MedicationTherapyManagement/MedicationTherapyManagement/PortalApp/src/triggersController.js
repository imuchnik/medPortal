(function(mtmPortal, _) {
    mtmPortal.controller('triggersController',
        function($scope, $http, $filter) {
            $scope.triggers = [];
            getReports();

            function getReports() {
                $http({ method: 'GET', url: '/Reports/list' }).
                    success(function(data) {
                        $scope.triggers = data;
                    });

            }

            ;
            $scope.parseParams = function(paramsString) {
                var summary = angular.fromJson(paramsString);

                return "template:" + summary.selectedTemplate + " dates: " + $filter('date')(summary.fromDate, "MM/dd/yyyy") + "-" + $filter('date')(summary.toDate, "MM/dd/yyyy");
            };
            $scope.deleteTriggerReport = function(id) {
                $http({ method: 'POST', url: 'Reports/deleteTriggerReport/' + id }).
                    success(function(data) {
                        $scope.triggers = _.without($scope.triggers, _.findWhere($scope.triggers, { "ReportId": id }));
                    });
            };


          
        });

}(window.mtmPortal, window._));