sparrowApp.factory('Artist', function ($http, $q) {
    var selectedArtist = {};
    return {
        SetArtist: function(artist) {
            selectedArtist = artist;
        },
        GetSelectedArtist: function() {
            return selectedArtist;
        },
        GetGenres : function() {
            var defer = $q.defer();
            var req = {
                url: "Artist/GetGenres",
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
        GetArtist: function (artistId) {
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
        },
        GetArtists: function (email, token) {
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
        CreateArtist: function(userData) {
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
                .success(function(data) {
                    defer.resolve(data);
                })
                .error(function(data, status) {
                    defer.reject(status);
                });
            return defer.promise;
        },
        CreateImage: function(imageData) {
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
        CreateAssociation: function(associationData) {
            var defer = $q.defer();
            var req = {
                url: "Artist/CreateAssociation",
                method: "POST",
                data: JSON.stringify(associationData),
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
        CreateEvent: function(event) {
            var defer = $q.defer();
            var req = {
                url: "Artist/CreateEvent",
                method: "POST",
                data: JSON.stringify(event),
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
        AddGenre: function (artistId, genreId) {
            var defer = $q.defer();
            var req = {
                url: "Artist/AddGenre?artistId=" + artistId+ "&genreId=" +genreId,
                method: "POST",
                data: JSON.stringify(event),
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
        RemoveGenre: function (artistId, genreId) {
            var defer = $q.defer();
            var req = {
                url: "Artist/RemoveGenre?artistId=" + artistId+ "&genreId=" +genreId,
                method: "POST",
                data: JSON.stringify(event),
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
        }
    };
})