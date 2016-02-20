
app.controller("BankingAppCtrl", function ($scope, $http) {
    $scope.balance = 0;

    $http.get('api/BankAccount/AccountList').success(function (data) {
        $scope.accountlist = data;
    });
    $http.get('api/BankAccount/GetUserStatements').success(function (data) {
        $scope.transactions = data;
    });
    $http.get('api/BankAccount/GetBalance').success(function (data) {
        $scope.balance = data;
    });
});