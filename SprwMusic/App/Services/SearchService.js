sparrowApp.factory('Search', function($http, $q) {
    return {
        UserSearch: function (email) {
            var defer = $q.defer();
            var req = {
                url: "Search/UserSearch?email=" + email,
                method: "GET"
            };
            $http(req)
                .success(function(data) {
                    defer.resolve(data);
                })
                .errorr(function(data, status) {
                    defer.reject(status);
                });
            return defer.promise();
        },
        MultiSearch: function (name) {
            var defer = $q.defer();
            var req = {
                url: "Search/MultiSearch?name=" + name,
                method: "GET"
            };
            $http(req)
                .success(function (data) {
                    defer.resolve(data);
                })
                .errorr(function (data, status) {
                    defer.reject(status);
                });
            return defer.promise();
        }
    };
});