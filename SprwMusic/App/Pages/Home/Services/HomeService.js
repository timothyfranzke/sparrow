sparrowApp.factory("HomeService", function($http, $q) {
    return {
        Login : function(userData) {
            var defer = $q.defer();
            $http.get("Sparrow/AuthenticateUser", {
                    data: userData
                })
                .success(function(data) {
                    defer.resolve(data);
                })
                .errorr(function(data, status) {
                    defer.reject(status);
                });
            return defer.promise();
        },

        CreateUser : function(userData) {
            var defer = $q.defer();
            $http.get("Sparrow/CreateUser", {
                data: userData
            })
                .success(function (data) {
                    defer.resolve(data);
                })
                .errorr(function (data, status) {
                    defer.reject(status);
                });
            return defer.promise();
        }
    }
});