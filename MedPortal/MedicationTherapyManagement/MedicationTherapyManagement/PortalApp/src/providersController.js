(function (mtmPortal, _) {
    'use strict';
    mtmPortal.controller('providersController',
    function ($scope) {
        
        
        $scope.currentViewedProvider = {};

       

        $scope.addNewProvider = false;

        $scope.isView = true;
        
        $scope.save = function (provider) {
            $scope.providers.push({
                "npi": provider.npi,
                "firstName": provider.firstName,
                "lastName": provider.lastName,
                "city": provider.city,
                "state": provider.state,
                "open": 0,
                "completed": 0,
                "declined": 0
            });
            $scope.newProvider = null;
        };

        $scope.assignCurrentProvider = function (provider) {
            $scope.currentViewedProvider = provider;
            console.log($scope.currentViewedProvider);
        };

        $scope.edit = function (provider) {
            $scope.isView = true;
            $scope.providers.pop(_.find($scope.providers, function(prov) {
                return prov.id === provider.id;
            }));
            $scope.save(provider);
            
        };


    });
}(window.mtmPortal, window._));
