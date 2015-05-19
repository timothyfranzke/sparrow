sparrowApp.factory('ArtistService', function($http, $q) {
    return {
        CreateArtist: function (userData) {
            var defer = $q.defer();
            var req = {
                url: "Artist/CreateArtist",
                method: "POST",
                data: JSON.stringify(userData),
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
        CreateAlbum: function (userData) {
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
                .success(function (data) {
                    defer.resolve(data);
                })
                .error(function (data, status) {
                    defer.reject(status);
                });
            return defer.promise;
        },
        CreateTrack: function (userData) {
            var defer = $q.defer();
            var req = {
                url: "Track/Create",
                method: "POST",
                data: JSON.stringify(userData),
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
        GetArtists: function(email, token) {
            var defer = $q.defer();
            var req = {
                url: "Artist/GetArtists?email=" + email + "&token=" + token,
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
        },
        GetArtistById: function(artistId) {
            var defer = $q.defer();
            var req = {
                url: "Artist/GetArtist?artistId=" + artistId,
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
    }
})