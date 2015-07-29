sparrowApp.controller('HomeController', function ($scope, $cookies, $state, $interval, Auth) {
    $scope.showSpinner = false;
    $scope.user = {
        Email: "",
        Password: ""
    };
    //alert($cookies.sprwUser.Email);
    if ($cookies.sprwUser !== undefined && $cookies.sprwToken !== undefined) {
        Auth.SetUserInfo($scope.user);
        $state.go('player.discover');
    }

    $scope.newUser = {
        FirstName: "",
        LastName: "",
        Email: "",
        Password: ""
    };
    $interval(function () {
        $scope.determinateValue += 1;
        if ($scope.determinateValue > 100) {
            $scope.determinateValue = 30;
        }
    }, 100, 0, true);
    $scope.login = function (user) {
        $scope.showSpinner = true;
        console.log(JSON.stringify(user));
        Auth.Login(user).then(function (data) {
            console.log(JSON.stringify(data));
            if (data.authenticated) {
                if (user.remember) {
                    $cookies.sprwUser = data.email;
                    $cookies.sprwToken = data.token;
                }
                $state.go('player.discover');
                
            } else {
                alert("you are out");
            }
            $scope.showSpinner = false;
        });
    };

    $scope.createUser = function (user, confirm) {
        $scope.showSpinner = true;
        var pass = false;
        if (user.Email === confirm.Email) {
            pass = true;
        } else {
            pass = false;
            $scope.showSpinner = false;
        }
        if (user.Password === confirm.Password) {
            pass = true;
        } else {
            pass = false;
        }
        if (pass) {
            $scope.showSpinner = true;
            Auth.CreateUser(user).then(function (data) {
                console.log(JSON.stringify(data));
                if (data.authenticated) {
                    if (user.remember) {
                        $cookies.sprwUser = data.email;
                        $cookies.sprwToken = data.token;
                    }
                    $scope.showSpinner = false;
                    $state.go('player.discover');

                } else {
                    $scope.showSpinner = false;
                    alert("you are out");
                }
                
            });
        } else {
            $scope.showSpinner = false;
        }

    };
});