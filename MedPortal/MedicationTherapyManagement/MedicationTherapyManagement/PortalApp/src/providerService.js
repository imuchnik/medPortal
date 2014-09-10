(function (mtmPortal) {
    mtmPortal.service('providerService', ['$http', function ($http) {

        this.fetchProviders = function() {
            var providers;
            return $http.get('/provider/Index').success(function(data) {
                providers = data;


            });
        };
    }]);

})(window.mtmPortal)