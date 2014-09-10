(function(mtmPortal) {
    mtmPortal.controller('triggerWizardController',
        function($scope, $http, $location) {

            $scope.selection = 1;
            $scope.selectedTemplate = "";
            $scope.trigger = {};
            $scope.trigger.addReference = false;
            $scope.triggerResults = [];
            $scope.datepicker = { date: new Date("2012-09-01T00:00:00.000Z") };
            $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'shortDate'];
            $scope.format = $scope.formats[0];
            $scope.wizard = true;
            $scope.params = {};
            $scope.numberOfRecords = 0;
            $scope.gridOptions = {
                sortable: true,
                filterable: {
                    extra: false
                },
                pageSize: 10,
                schema: {
                    data: $scope.triggerResults,
                    total: $scope.numberOfRecords
                },

                columnMenu: {
                    messages: {
                        columns: "Choose columns",
                        filter: "Apply filter",
                        sortAscending: "Sort (asc)",
                        sortDescending: "Sort (desc)"
                    }
                },
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                columns: [
                    { "field": "Key", "title": "Patient" },
                    { "field": "Value[0].DateOfBirth", "title": "Patient DOB", template: "#= kendo.toString(kendo.parseDate(Value[0].DateOfBirth, 'yyyy-MM-dd'), 'MM/dd/yyyy') #" },
                    { "field": "Value[0].SexCode", "title": "Patient Sex" },
                    { "field": "Value[0].RelationshipCode", "title": "Relationship" },
                    { "field": "Value[0].PharmacyCount", "title": "Number of Pharmacies" },
                    { "field": "Value[0].TotalDollars", "title": "Total Dollars", "format": "{0:c}" },
                    { "field": "Value[0].PrescriberCount", "title": "Number of Prescribers" },
                    { "field": "Value[0].RxCount", "title": "Number of Rx" }
                ],
                detailTemplate: 'Claims: <div class="claims"></div>',
                detailInit: function(e) {
                    e.detailRow.find(".claims").kendoGrid({
                        dataSource: e.data.Value,
                        sortable: true,
                        columns: [
                            { "field": "Pharmacy", "title": "Pharmacy Id" },
                            { "field": "PharmacyName", "title": "Pharmacy Name" },
                            { "field": "RefillCode", "title": "Refill" },
                            { "field": "Prescriber", "title": "Prescriber Id" },
                            { "field": "PrescriberLastName", "title": "Prescriber Last Name" },
                            { "field": "DateFilled",  "title": "Date Filled",
                                template: "#= kendo.toString(kendo.parseDate(DateFilled, 'yyyy-MM-dd'), 'MM/dd/yyyy') #"
                            },
                            { "field": "GPIDescription", "title": "GPI Description" },
                            { "field": "MetricQuantity", "title": "Metric Quantity" },
                            { "field": "DaysSupply", "title": "Days Supply" },
                            { "field": "PaidAmt", "title": "Paid Amount", "format": "{0:c}" },
                            { "field": "Copay", "title": "Copay", "format": "{0:c}" }
                        ],
                    });
                }
            };


            $scope.templateTypes = [
                { "id": 1, "name": "Basic trigger panel", "type": "GenericTrigger" }//,
               // { "id": 2, "name": "Greater than X Chronic Med Rxs", "type": "ChronicDrugs" }
            ];

            //TODO: ugly hack to break out of ng-switch scope. Need to find a better way to do it.
            $scope.setTemplateType = function(model) {
                $scope.selectedTemplate = model;
                $scope.params.selectedTemplate = model;

                console.log($scope.selectedTemplate);

            };
            $scope.clientSelected = function(client) {
               return $scope.currentClient.ClientName !== '';

            };


            $scope.generateNumbersArray = function(start, end) {
                var result = [];
                for (var i = start; i < end + 1; i++) {
                    result.push(i);
                }
                return result;
            };

            function constructParams() {
                $scope.params.toDate = $scope.datepicker.toDate;
                $scope.params.fromDate = $scope.datepicker.fromDate;
                $scope.params.displayToDate = $scope.datepicker.displayToDate;
                $scope.params.displayFromDate = $scope.datepicker.displayFromDateromDate;
                $scope.params.client = $scope.currentClient.ClientName || "none";
            }

            ;

            $scope.runTrigger = function() {
                constructParams();
                $http({ method: 'GET', url: '/Reports/runTriggerReport', params: $scope.params }).
                    success(function(data) {
                        $scope.triggerResults = data;
                        $scope.numberOfRecords = data.length;
                        $scope.wizard = false;
                    });
                return $scope.triggerResults;
            };
            $scope.goBack = function () {
                $scope.wizard = true;
            };
            $scope.getTemplateName = function(selectedType) {
                var selTemplate = _.find($scope.templateTypes, function(tmp) {
                    return tmp.type === selectedType;
                });
                return selTemplate === undefined ? "" : selTemplate.name;
            };

            $scope.cancel = function () {
                $scope.params = {};
                $location.path('/triggers');
            };


            $scope.saveTriggerResults = function() {
                $http({ method: 'POST', url: '/Reports/saveTriggerResults', data: { "triggerParams": $scope.params, "data": $scope.triggerResults } }).
                    success(function (data) {
                        $scope.params = {};
                        $location.path('triggers');
                    });
            };
        });
})(window.mtmPortal);