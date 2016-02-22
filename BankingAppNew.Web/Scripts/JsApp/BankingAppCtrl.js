app.controller("BankingAppCtrl", function ($scope, $http, $location) {
    $scope.balance = 0;
    $scope.authenticated = sessionStorage.getItem("tokenKey");

    $scope.logout = function() {
        sessionStorage.removeItem("tokenKey");
        $location.path("/login");
    }

    $scope.withdraw = function() {
        var config = {
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("tokenKey")
            }
        };
        $http.post('api/BankAccount/Withdraw', $scope.withdrawAmount, config).success(function (data) {
            $scope.update();
        });
    }


    $scope.refill = function () {
        var config = {
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("tokenKey")
            }
        };
        $http.post('api/BankAccount/Deposit',  $scope.refillAmount, config).success(function (data) {
            $scope.update();
        });
    }


    $scope.transfer = function () {
        var config = {
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("tokenKey")
            }
        };
        var data = {
            Amount: $scope.transferAmount,
            DestanationId : $scope.anotherClientId
        };
        $http.post('api/BankAccount/Transfer', data, config).success(function (data) {
            $scope.update();
        });
    }

    $scope.update = function () {
            var config = {
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem("tokenKey")
                }
            };
            console.log(JSON.stringify(config.headers));
            $http.get('api/BankAccount/AccountList').success(function (data) {
                $scope.accountlist = data;
            });
            $http.get('api/BankAccount/Userstatements', config).success(function (data) {
                $scope.transactions = data;
            });
            $http.get('api/BankAccount/Balance', config).success(function (data) {
                $scope.balance = data;
            });
            $http.get('api/BankAccount/UserInfo', config).success(function (data) {
                $scope.userinfo = data;
            });
    }
    $scope.update();
});