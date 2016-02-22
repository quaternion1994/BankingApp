app.controller("RegCtrl", function ($scope, $http, $location) {
    $scope.validmessages = [];
    $scope.message = "";

    $scope.reg = function () {

            var formdata = {
                UserName: $scope.username,
                Password: $scope.password,
                ConfirmPassword: $scope.confpassword
            }

            $http.post("api/BankAccount/CreateBankAccount", JSON.stringify(formdata)).then(function successCallback(response) {
                $scope.message = "Registration is succeeded";
                $scope.validmessages = [];
                $location.path("/login");
            },
            function errorCallback(response) {
                $scope.message = "";
                $scope.validmessages = [];
                var modelState = response.data.ModelState;

                for (var key in modelState) {
                    if (modelState.hasOwnProperty(key)) {
                        for (var i = 0; i < modelState[key].length; i++) {
                            $scope.validmessages.push(modelState[key][i]);
                        }
                    }
                }
            });
        }
});