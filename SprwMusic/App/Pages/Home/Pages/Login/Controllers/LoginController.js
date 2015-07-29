sparrowApp.controller('LoginController', function($scope, $state, AuthService) {
    $scope.user = {
        Email: "",
        Password: ""
    };

    $scope.login = function(user) {
        console.log(JSON.stringify(user));
        AuthService.Login(user).then(function(data) {
            console.log(JSON.stringify(data));
            if (data.authenticated) {
                alert("you're in");
                $state.go('portal');

            } else {
                alert("you are out");
            }
        });
    };
});

