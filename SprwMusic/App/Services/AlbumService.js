sparrowApp.factory('Album', function ($http, $q) {
    return {
        CreateAlbum: function(userData) {
            var defer = $q.defer();
            var req = {
                url: "Album/CreateAlbum",
                method: "POST",
                data: JSON.stringify(userData),
                headers: {
                    'Content-Type': 'application/json'
                }
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
        CreateImage: function (imageData) {
            var defer = $q.defer();
            var req = {
                url: "Artist/CreateImage",
                method: "POST",
                data: JSON.stringify(imageData),
                headers: {
                    'Content-Type': 'application/json'
                }
            };
            $http(req)
                .success(function (data) {
                    defer.resolve(data);
                })
                .error(function (data, status) {
                    defer.reject(status);
                });
            return defer.promise;
        },
        GetAlbums: function(artistId) {
            var defer = $q.defer();
            var req = {
                url: "Album/GetAlbums?artistId=" + artistId,
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