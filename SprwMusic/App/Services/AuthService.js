sparrowApp.factory('AuthService', function ($http, $q) {
    var user = {
        email: "",
        token: "",
        authenticated: ""
    };
    return {
        Login: function(userData) {
            var defer = $q.defer();
            var req = {
                url: "Sparrow/AuthenticateUser",
                method: "POST",
                data: JSON.stringify(userData),
                headers: {
                    'Content-Type': 'application/json'
                }
            };
            $http(req)
                .success(function (data) {
                    user.authenticated = data.Authenticated;
                    user.token = data.Token;
                    user.email = userData.Email;
                    console.log(user);
                    defer.resolve(user);
                })
                .error(function(data, status) {
                    defer.reject(status);
                });
            return defer.promise;
        },

        CreateUser: function(userData) {
            var defer = $q.defer();
            var req = {
                url: "Sparrow/CreateUser",
                method: "POST",
                data: JSON.stringify(userData),
                headers: {
                    'Content-Type': 'application/json'
                }
            };
            $http(req)
                .success(function (data) {
                    user.authenticated = data.Authenticated;
                    user.token = data.Token;
                    user.email = userData.email;
                    defer.resolve(user);
                })
                .error(function(data, status) {
                    defer.reject(status);
                });
            return defer.promise;
        },

        GetUserInfo : function() {
            return user;
        }
    }
});