sparrowApp.factory('Track', function ($http, $q) {
    var uploader = new FileUploader({
        url: 'Track/Create'
    });
    return {
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
        GetUploader: function() {
            
        }
    };
});