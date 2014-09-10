(function(mtmPportal) {
    mtmPortal.controller('triggerReportController',
        function($scope, $routeParams, $http, ngDialog, $rootScope) {
            $scope.ReportId = $routeParams.id;
            $scope.triggerReport = undefined;
            $scope.collapsed = true;
            $scope.numberOfRecords = 0;
            $rootScope.dataItem = {};

            getReportDetail($routeParams.id);


            function getReportDetail(id) {
                $http({ method: 'GET', url: '/Reports/ReportDetail/' + id }).
                    success(function(data) {
                        $scope.triggerReport = new kendo.data.ObservableArray(data);
                        $scope.numberOfRecords = $scope.triggerReport.length;
                    });
            }


            function markReviewed(key, id, reviewed) {
                $http({ method: 'POST', url: '/Reports/markReviewed', data: { "patientId": key, "reportId": id } }).
                    success(function(data) {
                       
                        reviewed.Reviewed = true;
                    });
            }

            $scope.markReviewed = function(key, reportId, reviewed) {
                markReviewed(key, reportId, reviewed);
                
            };


            $scope.showDetails = function(row) {
                ngDialog.open({
                    template: 'Partials/AssignMCA.html',
                    className: 'ngdialog-theme-plain',
                    controller: 'mcaController',
                    data: JSON.stringify(row),
                    scope: $scope
                });
            };
            
            $scope.showMCA = function (row) {
                ngDialog.open({
                    template: 'Partials/ViewMCA.html',
                    className: 'ngdialog-theme-plain',
                    controller: 'mcaController',
                    data: JSON.stringify(row),
                    scope: $scope
                });
            };
        });
})(window.mtmPortal);