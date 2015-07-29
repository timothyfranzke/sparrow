sparrowApp.controller('mymusicController', function ($scope, $state, Artist, Album, Auth,$mdBottomSheet, $mdDialog, $rootScope) {
    $scope.user = {};
    $scope.user.email = "timothyfranzke@gmail.com";
    $scope.user.token = '37ebbaf367ca2b97f680deb3315fe4ebe62337b5e5d8f618e6b0324f72ec5fb5';
    //$scope.user = Auth.GetUserInfo();
    //if (!$scope.user.authenticated) {
     //   $state.go("home.login");
    //}
    $scope.selectedArtist = {};
    $scope.showArtistImage = false;
    $scope.noArtists = true;
    $scope.artist = {};
    $scope.artists = [];
    $scope.album = {};
    $scope.albums = [];
    $scope.tracks = [];
    $scope.selectedGenres = [];
    Artist.GetArtists($scope.user.email, $scope.user.token).then(function (data) {
        $scope.artists = data;
        $scope.noArtists = true;
    });
    Artist.GetGenres().then(function(data) {
        $scope.genres = data.Genres;
    });
    $scope.selectedArtist.selectedArtist = {};
    $scope.showArtists = false;
    //$scope.artists = Artist.GetArtists(1);
    $scope.maxId = 1;
   
    var createArtist = function (artist)
    {
        $scope.artists.push(artist);
        $scope.noArtists = false;
        $scope.showArtists = true;
        $scope.showImageCropper = true;
        artist.Token = $scope.user.token;
        artist.UserEmail = $scope.user.email;
        Artist.CreateArtist(artist).then(function (data) {
            if (data.Status.Success) {
                $scope.selectedArtist.ArtistName = artist.Name;
                $scope.selectedArtist.Description = artist.Description;
                $scope.selectedArtist.ArtistId = data.Id;
                alert(data.Id);
                artist.Id = data.Id;
                var imageModel = {
                    'UserEmail': $scope.user.email,
                    'Token': $scope.user.token,
                    'ArtistId': data.Id,
                    'TrackingId': data.Id,
                    'Image64': artist.Image,
                    'Primary': true
                };
                if (artist.Image !== undefined) {
                    Artist.CreateImage(imageModel).then(function (data) {});
                }
            } else {
            }

        });

        $scope.selectedArtist.name = artist.name;
        $scope.selectedArtist.description = artist.description;
    };
    var createAlbum = function (album) {
        alert(JSON.stringify(album));
        var newAlbum = {
            'UserEmail': $scope.user.email,
            'Token': $scope.user.token,
            'ArtistId': $scope.selectedArtist.ArtistId,
            'AlbumName': album.AlbumName,
            'Description': album.Description
        };

        $scope.showImageCropper = true;
        Album.CreateAlbum(newAlbum).then(function(data) {
            newAlbum.AlbumId = data.Id;
            $scope.album.name = "";
            $scope.album.description = "";
            $scope.albumImage = null;
            $scope.imageCropResult = null;
            album.Tracks = [];
            $scope.selectedArtist.Albums.push(album);
            var imageModel = {
                'UserEmail': $scope.user.email,
                'Token': $scope.user.token,
                'ArtistId': Artist.GetSelectedArtist,
                'AlbumId' : data.Id,
                'TrackingId': data.Id,
                'Image64': artist.Image,
                'Primary': true
            };
            if (artist.Image !== undefined) {
                Artist.CreateImage(imageModel).then(function (data) {

                });
            }
        });
    };
    var createEvent = function(event) {
        alert(JSON.stringify(event));
        event.UserEmail = email;
        event.Token = token;
        event.ArtistId = $scope.selectedArtist.ArtistId;
        Artist.CreateEvent(event).then(function (data) {
            var newEvent = {
                "EventName": event.Name,
                "EventDate": event.Date,
                "EventId": data.EventId
            };
            if ($scope.artists[event.ArtistId].events == undefined)
                $scope.artists[event.ArtistId].events = [];
            $scope.artists[event.ArtistId].events.push(newEvent);
        });
    };

    $scope.showAdvanced = function (ev) {
        $mdDialog.show({
            controller: 'dialogController',
            templateUrl: 'App/Partials/CreateArtistDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
        })
        .then(function (artist) {
            alert(JSON.stringify(artist));
            createArtist(artist);
        }, function () {
            $scope.alert = 'You cancelled the dialog.';
        });
    };
    $scope.showAlbumDialog = function (ev) {
        $mdDialog.show({
                controller: 'dialogController',
                templateUrl: 'App/Partials/CreateAlbumDialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
            })
            .then(function (albumModel) {
                alert(JSON.stringify(albumModel));
                albumModel.ArtistId = $scope.selectedArtist.ArtistId;
                albumModel.UserEmail = $scope.user.email;
                albumModel.Token = $scope.user.token;

                createAlbum(albumModel);
        }, function() {
                $scope.alert = 'You cancelled the dialog.';
            });
    };

    
    var album = {
        id: 0,
        name: "bad album",
        description: ""
    };
    var singlesAlbum = {
        id: 0,
        name: "singles",
        description: "",
        tracks: []
    };
    $scope.albums.push(singlesAlbum);
    //alert(JSON.stringify($scope.artists));
    if ($scope.artists.length > 0) {
        $scope.showArtists = true;
        $scope.selectedArtist.name = $scope.artists[0].Name;
    }
    $scope.selectedAlbum =
    {
        name: "--Select Album--"
    };

    $scope.image = {};
    $scope.switchArtist = function (artist) {
        Artist.GetArtist(artist).then(function(data) {
            Artist.SetArtist(data.Artist);
            $scope.selectedArtist = data.Artist;
        });
        $scope.selectedArtistImage = "Track/GetImageFile?artistId=" + artist + "&albumId=0&trackId=0";
    };

    console.log($scope);

    $scope.changed = function() {
        alert('poop');
    };
    email = 'timothyfranzke@gmail.com';
    token = '37ebbaf367ca2b97f680deb3315fe4ebe62337b5e5d8f618e6b0324f72ec5fb5';
    $scope.showImageCropper = true;
    $scope.imageCropResult = null;
    $scope.$watch('albums', function (newVal) {
        //alert("changed");
    });
    $scope.$watch('artistImage', function (newVal) {
        if (newVal) {
            //console.log('imageCropResult', newVal);
            $scope.showImageCropper = false;
            $scope.showArtistImage = true;
        }
    });
    $scope.$watch('albumImage', function (newVal) {
        if (newVal) {
            $scope.showImageCropper = false;
        }
    });
    $scope.$watch('trackImage', function (newVal) {
        if (newVal) {
            $scope.showImageCropper = false;
        }
    });
    $scope.createArtist = function (artist) {
        $scope.artists.push(artist);
        $scope.noArtists = false;
        $scope.showArtists = true;
        $scope.showImageCropper = true;
        $scope.artist.Token = token;
        $scope.artist.UserEmail = email;
        Artist.CreateArtist(artist).then(function (data) {
            if (data.Status.Success) {
                console.log($scope);
                $scope.selectedArtist.ArtistName = artist.Name;
                $scope.selectedArtist.Description = artist.Description;
                $scope.selectedArtist.ArtistId = data.Id;
                alert("Getting image model");
                var imageModel = {
                    'UserEmail': email,
                    'Token': token,
                    'ArtistId': data.Id,
                    'TrackingId': data.Id,
                    'Image64': artist.Image,
                    'Primary': true
                };
                alert("image model!!!");
                alert(JSON.stringify("This is the imageModel: " + imageModel));
                if (artist.Image !== undefined) {
                    Artist.CreateImage(imageModel).then(function (data) {
                        $mdDialog.hide();
                    });
                }
                $mdDialog.hide();
            } else {
                $mdDialog.hide('failed');
            }

        });

        $scope.selectedArtist.name = artist.name;
        $scope.selectedArtist.description = artist.description;
    };


    $scope.createAlbum = function (album) {
        alert($scope.selectedArtist.ArtistId);
        var newAlbum = {
            'UserEmail': email,
            'Token': token,
            'ArtistId': $scope.$parent.selectedArtist.ArtistId,
            'AlbumName': album.name,
            'AlbumDescription': album.description
        };
        $scope.album.name = "";
        $scope.album.description = "";
        $scope.albumImage = null;
        $scope.imageCropResult = null;

        $scope.showImageCropper = true;
        Album.CreateAlbum(newAlbum).then(function (data) {
            newAlbum.AlbumId = data.Id;
            $scope.albums.push(newAlbum);
        });
    };
    $scope.changeAlbum = function(album) {
        $scope.selectedAlbum = album;
    };
    $scope.createTrack = function (track) {
        var newTrack = {
            "name": track.name,
            "albumId": $scope.selectedAlbum.id
        };
        $scope.tracks.push(newTrack);
        $scope.albums[$scope.selectedAlbum.id].tracks.push(newTrack);
    };
    $scope.selectAlbum = function(album) {
        $scope.fullAlbum = album;
    };
    $scope.addGenre = function (genre) {
        alert(JSON.stringify(genre));
        var genres = JSON.parse(genre);
        alert(JSON.stringify(genre));
        $scope.selectedGenres.push(genre);
        Artist.AddGenre($scope.selectedArtist.ArtistId, genres.GenreId).then(function (data) {

        });
    };

    $scope.removeGenre = function(genre) {
        alert(genre);
    };


   
    $scope.showAlbumDialog = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'App/Partials/Dialog/CreateAlbumDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
        })
        .then(function (data) {
            createAlbum(data);
        }, function () {
            $scope.alert = 'You cancelled the dialog.';
        });
    };

    $scope.showTrackDialog = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'App/Partials/Dialog/CreateTrackDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
        })
            .then(function (data) {
                alert(JSON.stringify(data));
            }, function () {
                $scope.alert = 'You cancelled the dialog.';
            });
    };

    $scope.showEventDialog = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'App/Partials/Dialog/CreateEventDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
        })
        .then(function (data) {
            createEvent(data);
            
        }, function () {
            $scope.alert = 'You cancelled the dialog.';
        });
    };
});

function DialogController($scope, $mdDialog, FileUploader, Artist) {
    var email = 'timothyfranzke@gmail.com';
    var token = '37ebbaf367ca2b97f680deb3315fe4ebe62337b5e5d8f618e6b0324f72ec5fb5';
    $scope.artist = Artist.GetSelectedArtist();
    $scope.selectedAlbum = null;
    $scope.showImageCropper = true;
    $scope.imageCropResult = null;
    var uploader = $scope.uploader = new FileUploader({
        url: 'Track/Create'
    });
    $scope.uploadTrack = function (track) {
        $scope.track = track;
        $scope.uploader.queue[0].formData = [{ TrackName: track.TrackName }, { ReleaseDate: track.ReleaseDate }, { ArtistId: $scope.artist.ArtistId }, { AlbumId: track.AlbumId }, { UserEmail: email }, { Token: token }];
        $scope.uploader.queue[0].alias = "Track";
        $scope.uploader.queue[0].upload();
    };
    uploader.onSuccessItem = function (data) {
        $mdDialog.hide($scope.track);
    };
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.create = function (data) {
        alert(JSON.stringify(data));
        $mdDialog.hide(data);
    };
    $scope.selectAlbum = function(album) {
        $scope.selectedAlbum = album;
    };

}