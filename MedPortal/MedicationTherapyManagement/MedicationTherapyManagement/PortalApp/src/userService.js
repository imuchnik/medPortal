(function(mtmPortal) {
    mtmPortal.service('userService',['$http', function ($http) {
       
        this.getUserRoles = function () {
            var userRoles;
           return $http.get('/sessionuser/sessionuserroles').success(function (data) {
               userRoles = data;

            });
            
        };
    }]);

})(window.mtmPortal)