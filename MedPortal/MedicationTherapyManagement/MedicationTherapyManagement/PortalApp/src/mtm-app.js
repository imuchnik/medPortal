﻿'use strict';
var mtmPortal;

(function (angular, $) {
    'use strict';
    mtmPortal = angular.module('mtmPortal', ['ngRoute', 'kendo.directives', 'ngDialog', 'ui.bootstrap' ])
    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
                .when('/providers', { templateUrl: '../Partials/Providers.html' })
                .when('/triggers', { templateUrl: '../Partials/Triggers.html' })
                .when('/triggerReport/:id', { templateUrl: '../Partials/TriggerReport.html' })
                .when('/newReportTemplate', { templateUrl: '../Partials/CustomReport.html' })
                .when('/cases', { templateUrl: '../Partials/cases.html' })
                .when('/case/:id', { templateUrl: '../Partials/CaseView.html' })
                .when('/newTriggerReport', { templateUrl: '../Partials/TriggerRequestModalWizard.html' })
                .otherwise({ redirectTo: '/' });
        }])
    .value('$strapConfig', {})
    .directive('toggleCollapsibles', function () {
         var EXPAND_TEXT = 'Expand All',
             COLLAPSE_TEXT = 'Collapse All';

         return {
             link: function (scope, element, attributes) {
                 var expanded = false,
                     elementSelector = attributes.elementSelector,

                     doCollapse = function (collapsibles, action) {
                         collapsibles.find('.collapse').collapse(action);
                         collapsibles.collapse(action);
                     };

                 element.click(function () {
                     var collapsibles = angular.element(elementSelector);

                     if (!expanded) {
                         doCollapse(collapsibles, 'show');
                         element.text(COLLAPSE_TEXT);
                     } else {
                         doCollapse(collapsibles, 'hide');
                         element.text(EXPAND_TEXT);
                     }

                     expanded = !expanded;
                 });
             }
         };
     })
     .directive('toggleClaimDetail', function () {
         return {
             restrict: 'A',
             scope: false,
             link: function (scope, element, attr) {

                 element.click(function () {
                 
                   
                     var element = angular.element('#'+attr.detailTarget);
             
                     element.collapse('toggle');
                 });

             }
         };
 })
    

     .directive('bsDatepicker', ['$timeout', '$strapConfig',
             function ($timeout, $strapConfig) {
                 var isAppleTouch = /(iP(a|o)d|iPhone)/g.test(navigator.userAgent);
                 var regexpMap = function regexpMap(language) {
                     language = language || 'en';
                     return {
                         '/': '[\\/]',
                         '-': '[-]',
                         '.': '[.]',
                         ' ': '[\\s]',
                         'dd': '(?:(?:[0-2]?[0-9]{1})|(?:[3][01]{1}))',
                         'd': '(?:(?:[0-2]?[0-9]{1})|(?:[3][01]{1}))',
                         'mm': '(?:[0]?[1-9]|[1][012])',
                         'm': '(?:[0]?[1-9]|[1][012])',
//                         'DD': '(?:' + $.fn.datepicker.dates[language].days.join('|') + ')',
//                         'D': '(?:' + $.fn.datepicker.dates[language].daysShort.join('|') + ')',
//                         'MM': '(?:' + $.fn.datepicker.dates[language].months.join('|') + ')',
//                         'M': '(?:' + $.fn.datepicker.dates[language].monthsShort.join('|') + ')',
                         'yyyy': '(?:(?:[1]{1}[0-9]{1}[0-9]{1}[0-9]{1})|(?:[2]{1}[0-9]{3}))(?![[0-9]])',
                         'yy': '(?:(?:[0-9]{1}[0-9]{1}))(?![[0-9]])'
                     };
                 };
                 var regexpForDateFormat = function regexpForDateFormat(format, language) {
                     var re = format, map = regexpMap(language), i;
                     i = 0;
                     angular.forEach(map, function (v, k) {
                         re = re.split(k).join('${' + i + '}');
                         i++;
                     });
                     i = 0;
                     angular.forEach(map, function (v, k) {
                         re = re.split('${' + i + '}').join(v);
                         i++;
                     });
                     return new RegExp('^' + re + '$', ['i']);
                 };
                 return {
                     restrict: 'A',
                     require: '?ngModel',
                     link: function postLink(scope, element, attrs, controller) {
                         var options = angular.extend({ autoclose: true }, $strapConfig.datepicker || {}), type = attrs.dateType || options.type || 'date';
                         angular.forEach([
                           'format',
                           'weekStart',
                           'calendarWeeks',
                           'startDate',
                           'endDate',
                           'daysOfWeekDisabled',
                           'autoclose',
                           'startView',
                           'minViewMode',
                           'todayBtn',
                           'todayHighlight',
                           'keyboardNavigation',
                           'language',
                           'forceParse'
                         ], function (key) {
                             if (angular.isDefined(attrs[key]))
                                 options[key] = attrs[key];
                         });
                         var language = options.language || 'en', readFormat = attrs.dateFormat || options.format || $.fn.datepicker.dates[language] && $.fn.datepicker.dates[language].format || 'mm/dd/yyyy', format = isAppleTouch ? 'yyyy-mm-dd' : readFormat, dateFormatRegexp = regexpForDateFormat(format, language);
                         if (controller) {
                             controller.$formatters.unshift(function (modelValue) {
                                 return type === 'date' && angular.isString(modelValue) && modelValue ? $.fn.datepicker.DPGlobal.parseDate(modelValue, $.fn.datepicker.DPGlobal.parseFormat(readFormat), language) : modelValue;
                             });
                             controller.$parsers.unshift(function (viewValue) {
                                 if (!viewValue) {
                                     controller.$setValidity('date', true);
                                     return null;
                                 } else if (type === 'date' && angular.isDate(viewValue)) {
                                     controller.$setValidity('date', true);
                                     return viewValue;
                                 } else if (angular.isString(viewValue) && dateFormatRegexp.test(viewValue)) {
                                     controller.$setValidity('date', true);
                                     if (isAppleTouch)
                                         return new Date(viewValue);
                                     return type === 'string' ? viewValue : $.fn.datepicker.DPGlobal.parseDate(viewValue, $.fn.datepicker.DPGlobal.parseFormat(format), language);
                                 } else {
                                     controller.$setValidity('date', false);
                                     return undefined;
                                 }
                             });
                             controller.$render = function ngModelRender() {
                                 if (isAppleTouch) {
                                     var date = controller.$viewValue ? $.fn.datepicker.DPGlobal.formatDate(controller.$viewValue, $.fn.datepicker.DPGlobal.parseFormat(format), language) : '';
                                     element.val(date);
                                     return date;
                                 }
                                 if (!controller.$viewValue)
                                     element.val('');
                                 return element.datepicker('update', controller.$viewValue);
                             };
                         }
                         if (isAppleTouch) {
                             element.prop('type', 'date').css('-webkit-appearance', 'textfield');
                         } else {
                             if (controller) {
                                 element.on('changeDate', function (ev) {
                                     scope.$apply(function () {
                                         controller.$setViewValue(type === 'string' ? element.val() : ev.date);
                                     });
                                 });
                             }
                             element.datepicker(angular.extend(options, {
                                 format: format,
                                 language: language
                             }));
                             scope.$on('$destroy', function () {
                                 var datepicker = element.data('datepicker');
                                 if (datepicker) {
                                     datepicker.picker.remove();
                                     element.data('datepicker', null);
                                 }
                             });
                             attrs.$observe('startDate', function (value) {
                                 element.datepicker('setStartDate', value);
                             });
                             attrs.$observe('endDate', function (value) {
                                 element.datepicker('setEndDate', value);
                             });
                         }
                         var component = element.siblings('[data-toggle="datepicker"]');
                         if (component.length) {
                             component.on('click', function () {
                                 if (!element.prop('disabled')) {
                                     element.trigger('focus');
                                 }
                             });
                         }
                     }
                 };
             }
        ]);
}(window.angular,window.jQuery));