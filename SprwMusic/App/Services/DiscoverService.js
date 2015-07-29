sparrowApp.factory('Discover', function ($http, $q) {
    var playListItme = {

    };
    var playList = [];
    return {
        GetPlaylist: function(page) {
            var defer = $q.defer();
            var req = {
                url: "Discover/GetPlaylist?page=" + page,
                method: "GET"
            };
            $http(req)
                .success(function(data) {
                    defer.resolve(data);
                })
                .error(function(data, status) {
                    defer.reject(status);
                });
            return defer.promise;
        },
        GetEvents: function(email) {
            var defer = $q.defer();
            var req = {
                url: "Discover/GetEvents?email=" + email,
                method: "GET"
            };
            $http(req)
                .success(function (data) {
                    defer.resolve(data);
                })
                .error(function (data, status) {
                    defer.reject(status);
                });
            return defer.promise;
        } 
    };
});