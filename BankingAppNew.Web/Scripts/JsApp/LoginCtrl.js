app.controller("LoginCtrl", function ($scope, $http, $location) {
    $scope.login = function() {
        var req = {
            method: "POST",
            url: "/Token",
            headers: {
                "Content-Type":  "application/x-www-form-urlencoded; charset=UTF-8"
            },
            data: "grant_type=password" + "&username=" + $scope.username + "&password=" + $scope.password
        }
        $scope.reg = function() {
            $location.path("/registration");
        }
        $scope.errormessage = "";
        $http(req).then(function successCallback(response) {
            alert(JSON.stringify(response));
            sessionStorage.setItem("tokenKey", response.data.access_token);
            console.log("TOKEN: "+response.access_token);
            $location.path("/bank");
        },
        function errorCallback(response) {
            $scope.errormessage = "Wrong either password or username";
        });
    }
    $scope.reg = function () {
        $location.path("/registration");
    }
});