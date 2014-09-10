(function (mtmPortal, _) {
    'use strict';
    mtmPortal.controller('customTriggerController',
        function($scope, $http, $filter) {
            $scope.query = [];

            function logicalBlock() {
                this.block = {
                    lblock:
                    {
                        "ColumnName": "",
                        "predicate": '',//<<gt, lt, eq, eg, el>>,
                        "value": "0"
                    },
                    "blockType": ""
                };
            }

       
            $scope.createNewLogicalBlock = function () {
               
                var bloc = new logicalBlock();
                $scope.query.push(bloc.block);
                console.log($scope.query);
            };
        });
  


 
})(window.mtmPortal, window._);