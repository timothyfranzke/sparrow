sparrowApp.controller('dialogController', function($scope, Artist, Album, $mdDialog, $rootScope, FileUploader) {
    var email = 'timothyfranzke@gmail.com';
    var token = '37ebbaf367ca2b97f680deb3315fe4ebe62337b5e5d8f618e6b0324f72ec5fb5';
    $scope.artist = {};
    $scope.showImageCropper = true;
    $scope.imageCropResult = null;
    var uploader = $scope.uploader = new FileUploader({
        url: 'Track/Create'
    });
    $scope.uploadTrack = function(track) {
        //alert($scope.selectedArtist.ArtistId);
        $scope.track = track;
        $scope.uploader.queue[0].formData = [{ TrackName: track.TrackName }, { ReleaseDate: track.ReleaseDate }, { ArtistId: $scope.selectedArtist.ArtistId }, { AlbumId: track.AlbumId }, { UserEmail: email }, { Token: token }];
        $scope.uploader.queue[0].alias = "Track";
        $scope.uploader.queue[0].upload();
    };
    uploader.onSuccessItem = function(data) {
        $mdDialog.hide($scope.track);
    };
    $scope.$watch('albums', function(newVal) {
        //alert("changed");
    });
    $scope.$watch('artistImage', function(newVal) {
        if (newVal) {

            $scope.artist.Image = newVal;

            //console.log('imageCropResult', newVal);
            $scope.showImageCropper = false;
            $scope.showArtistImage = true;
        }
    });
    $scope.$watch('albumImage', function(newVal) {
        if (newVal) {
            $scope.showImageCropper = false;
        }
    });
    $scope.$watch('trackImage', function(newVal) {
        if (newVal) {
            $scope.showImageCropper = false;
        }
    });
    $scope.createArtist = function(artist) {

        artist.Image = $scope.artist.Image;
        alert(JSON.stringify(artist));
        $mdDialog.cancel(artist);

    };
    $scope.createAlbum = function(album) {
        $mdDialog.cancel(album);
    };
});
