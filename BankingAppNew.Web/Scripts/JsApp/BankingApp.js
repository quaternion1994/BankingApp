var app = angular.module("BankingApp", ["ngRoute"]);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/bank', {
            templateUrl: 'Home/Index',
            controller: 'BankingAppCtrl'
        }).
        when('/login', {
            templateUrl: 'Home/Login',
            controller: 'LoginCtrl'
        }).
        when('/registration', {
            templateUrl: 'Home/Registration',
            controller: 'RegCtrl'
        }).
        otherwise({
            redirectTo: '/login'
        });
}]);
