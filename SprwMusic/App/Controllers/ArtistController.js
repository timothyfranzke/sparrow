sparrowApp.controller('ArtistController', function ($scope, $state, $mdDialog, FileUploader, Auth, Artist, Album, Track) {

    $scope.showManageArtists = false;
    $scope.showAlbumList = false;
    $scope.selectedArtist = {};
    $scope.selectedAlbum = {};
    $scope.selectedTracks = [];
    var user = Auth.GetUserInfo();
    user = {};
    user.token = "37ebbaf367ca2b97f680deb3315fe4ebe62337b5e5d8f618e6b0324f72ec5fb5";
    user.email = "timothyfranzke@gmail.com";
    $scope.track = {};
    $scope.image = {};
    if (user.token == "") {
        $state.go("home.login");
    }


    $scope.imageCropResult = null;
    $scope.showImageCropper = true;

    $scope.$watch('imageCropResult', function (newVal) {
        if (newVal) {
            console.log('imageCropResult', newVal);
        }

    });
    Artist.GetArtists(user.email, user.token).then(function (data) {
        $scope.artists = data;
        if (data.length > 1) {
            $scope.showManageArtists = true;
        } else {
            $scope.selectedArtist = data[0];
        }
    });

    $scope.uploader = new FileUploader({
        url: 'Track/Create'
        //formData: $scope.track
    });

    $scope.artist = {};
    $scope.album = {};
    $scope.track = {};
    $scope.showAlbum = false;

    $scope.uploader.onAfterAddingFile = function() {
        alert("uploaded!");
//        $scope.uploader.queue[0].alias = "Track";
        //       $scope.uploader.queue[0].url = 'Track/Create';
        //     $scope.uploader.queue[0].formData = [{ TrackName:"Track" }];
        //   $scope.uploader.queue[0].upload();
    };

    $scope.uploadTrack = function(track) {
        $scope.uploader.queue[0].formData = [{ TrackName: track.TrackName }, { ReleaseDate: track.ReleaseDate }, { ArtistId: track.ArtistId }, { AlbumId: track.AlbumId }, { UserEmail: user.email }, { Token: user.token }];
        $scope.uploader.queue[0].alias = "Track";
        $scope.uploader.queue[0].upload();
    };

    $scope.createArtist = function(artist) {
        console.log(JSON.stringify(artist));
        artist.UserEmail = user.email;
        artist.Token = user.token;
        //artist.Token = "37ebbaf367ca2b97f680deb3315fe4ebe62337b5e5d8f618e6b0324f72ec5fb5"
        Artist.CreateArtist(artist).then(function (data) {
            $mdDialog.hide(data);
            if (data.Status.Success) {
                var newArtist = {
                    ArtistId: data.Id,
                    ArtistName: artist.Name
                };

                alert(JSON.stringify(newArtist));
                $scope.artists.push(newArtist);
                console.log(JSON.stringify($scope.artists));

            } else {
                alert(data.Status.Message);
            }
        });
    };

    $scope.createAlbum = function(album) {
        album.UserEmail = user.email;
        album.Token = user.token;
        album.ArtistId = $scope.selectedArtist.Artist.ArtistId;
        Artist.CreateAlbum(album).then(function (data) {
            alert(JSON.stringify(data));
            if (data.Status.Success) {
                alert("created artist");
            } else {
                alert(data.Status.Message);
            }
        });
    };

    $scope.createTrack = function(track) {
        track.UserEmail = user.email;
        track.Token = user.token;
        track.AlbumId = $scope.selectedAlbum.AlbumId;
        track.ArtistId = $scope.selectedArtist.ArtistId;
        Track.CreateTrack(track).then(function (data) {

        });
    };

    $scope.createImage = function (img) {
        image = {};
        image.UserEmail = user.email;
        image.Token = user.token;
        image.ArtistId = 7;
        image.TrackingId = 7;
        image.Image64 = img;
        Artist.CreateImage(image).then(function(data) {

        });
    };



    $scope.selectArtist = function (artistId) {
        alert(artistId);
        $scope.showAlbum = true;
        ArtistService.GetArtistById(artistId).then(function (data) {
            $scope.selectedArtist = data;
            if (data.Artist.Albums.length > 0) {
                $scope.showAlbumList = true;
            }
           // $scope.$apply();
        });
    };

    $scope.selectAlbum = function (albumId) {
        $scope.selectedTracks = [];
        $scope.selectedArtist.Artist.Albums.forEach(function (album) {
            if (album.AlbumId === albumId) {
                $scope.selectedAlbum = album;
                $scope.selectedTracks = album.Tracks;
            }
        });
        $scope.selectedTracks.forEach(function (track) {
            var albumId = -1;
            if ($scope.selectedAlbum !== null) {
                albumId = $scope.selectedAlbum.AlbumId;
            }
            //Track/GetAudioFile?artistId=7&albumId=2&trackId=4
            var url = "/Track/GetAudioFile?artistId=" + $scope.selectedArtist.Artist.ArtistId + "&albumId=" + albumId + "&trackId=" + track.TrackId;
            track.URL = url;
        });
    };

    $scope.isOpen = false;


});