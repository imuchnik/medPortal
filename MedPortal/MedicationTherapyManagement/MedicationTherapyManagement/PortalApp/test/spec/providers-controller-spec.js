    describe("MTM Portal", function () {
    var controller,
        scope;

    beforeEach(module('mtmPortal'));
    beforeEach(inject(function ($controller, $rootScope) {
        scope = $rootScope.$new();
        controller = $controller;
        controller('providersController', { $scope: scope });

    }));
 

    describe("Providers controller", function () {
       
        it("should add provider", function () {
            scope.providers = [];
            scope.save({ "npi": 345, "firstName": "John","lastName": "Doe", "city": "Reno", "state": "NV" });
            expect(scope.providers.length).toEqual(1);
            expect(scope.providers[0].name).toEqual("John Doe");
        });

        it("should edit a provider", function () {
            scope.providers = [{ "id": 1, "npi": 345 }];

            var provider = { "id": 1, "npi": 3 };
            scope.edit(provider);

            expect(scope.providers[0].npi).toEqual(provider.npi);

        });
    });
});

//}());