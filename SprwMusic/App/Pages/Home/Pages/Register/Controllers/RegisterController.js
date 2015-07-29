sparrowApp.controller('RegisterController', function($scope, AuthService) {
    $scope.newUser = {
        FirstName: "",
        LastName: "",
        Email: "",
        Password: ""
    };


    $scope.CreateUser = function(user) {
        AuthService.CreateUser(user).then(function(data) {
            console.log(JSON.stringify(data));
            if (data.Authenticated) {
                alert("you're in");

            } else {
                alert("you are out");
            }
        });
    };
});