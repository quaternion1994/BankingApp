var app = angular.module("BankingApp", []);

app.controller("BankingAppCtrl", function ($scope, $http) {
        $http.get('api/BankAccount/AccountList').success(function(data) {
            $scope.accountlist = data;
        });
});