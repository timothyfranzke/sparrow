sparrowApp.controller('PlayerController', function($scope,$state,$mdBottomSheet, Discover, player, Artist, Auth, $mdDialog) {

    $scope.image = {
        width: "147",
        height: "147"
    };
    if (typeof window.orientation !== 'undefined') {
        $scope.image = {
            width: "250",
            height: "250"
        };
    }
    $scope.user = Auth.GetUserInfo();
    if (!$scope.user.authenticated) {
       // $state.go("home.login");
    }
    $scope.selectedArtist = {
        ArtistId: -1
    };
    $scope.demo = {
        topDirections: ['left', 'up'],
        bottomDirections: ['down', 'right'],
        isOpen: false,
        availableModes: ['md-fling', 'md-scale'],
        selectedMode: 'md-fling',
        availableDirections: ['up', 'down', 'left', 'right'],
        selectedDirection: 'left'
    };
    $scope.mode = 'query';
    $scope.determinateValue = 30;
    $scope.playing = true;
    $scope.showArtist = false;
    Discover.GetPlaylist(1).then(function (data) {
        $scope.loadingMessage = "Loaing Playlist";
        data.Playlist.forEach(function(artist) {
            var playListItem = {
                ArtistId: artist.ArtistId,
                AlbumId: artist.Tracks[0].AlbumId,
                TrackId: artist.Tracks[0].TrackId
            };
            //alert(JSON.stringify(playListItem));
            player.playlist.add(playListItem);
        });
        $scope.playlist = data.Playlist;
        //$scope.playlist.forEach(function(item) {
            //player.playlist.add(item);
        //});
    });
    $scope.player = player;
    $scope.buttonPlay = function() {
        if ($scope.playing) {
            $scope.playing = false;
            $scope.pause = true;
            $scope.player.play();
        } else {
            $scope.pause = false;
            $scope.playing = true;
            $scope.player.pause();
        }
        
    };
    $scope.play = function(artist) {
        if ($scope.selectedArtist.ArtistId === artist.ArtistId) {
            alert("artist Id");
            $scope.showArtist = true;
        } else {
            $scope.selectedArtist = artist;
            alert("no artist id");
            $scope.player.play();
        }
    };
    console.log($scope);

    $scope.messages = [
        {
            what: "nothing",
            who: "you",
            notes: "stupid shit"
        }
    ];

    $scope.alert = '';
    $scope.showListBottomSheet = function ($event) {
        $scope.alert = '';
        $mdBottomSheet.show({
            templateUrl: 'App/Partials/Bottomsheet/FilterBottomSheet.html',
            controller: 'ListBottomSheetCtrl',
            targetEvent: $event
        }).then(function (clickedItem) {
            $scope.alert = clickedItem.name + ' clicked!';
        });
    };
    $scope.showGridBottomSheet = function ($event) {
        $scope.alert = '';
        $mdBottomSheet.show({
            templateUrl: 'App/Partials/Bottomsheet/FilterBottomSheet.html',
            controller: 'ListBottomSheetCtrl',
            targetEvent: $event
        }).then(function (clickedItem) {
            $scope.alert = clickedItem.name + ' clicked!';
        });
    };
    $scope.imageCropResult = null;
    $scope.showImageCropper = true;
    $scope.showArtistImage = false;
    $scope.artist = {};
    $scope.artists = [];
    $scope.albums = [];
    $scope.tracks = [];
    $scope.selectedArtist = {};
    $scope.showArtists = false;
    $scope.artists = Artist.GetArtists(1);
    $scope.maxId = 1;

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
    if ($scope.artists.length > 0) {
        $scope.showArtists = true;
        $scope.selectedArtist.name = $scope.artists[0].Name;
    }
    $scope.selectedAlbum =
    {
        name: "--Select Album--"
    };

    $scope.image = {};
    $scope.$watch('albums', function (newVal) {
        
    });
    $scope.$watch('artistImage', function (newVal) {

        if (newVal) {
            alert(newVal);
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

    console.log($scope);

    $scope.changed = function() {
        alert('poop');
    };
    $scope.createArtist = function (artist) {
        $scope.artists.push(artist);
        $scope.showArtists = true;
        $scope.showImageCropper = true;
        $scope.selectedArtist.name = artist.name;
        $scope.selectedArtist.description = artist.description;
    };
    $scope.createAlbum = function (album) {
        alert($scope.albumImage);
        var newAlbum = {
            "id": $scope.maxId,
            "name": album.name,
            "description": album.description,
            "image": $scope.albumImage,
            "tracks": []
        };
        $scope.album.id = $scope.maxId;
        $scope.maxId += 1;
        $scope.album.name = "";
        $scope.album.description = "";
        $scope.albumImage = null;
        $scope.imageCropResult = null;
        $scope.albums.push(newAlbum);
        $scope.showImageCropper = true;
    };
    $scope.changeAlbum = function (album) {
        $scope.selectedAlbum = album;
    }
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
    //Discover.GetEvents(email).then(function (data) {
    //  $scope.events = data;
    //});



});

sparrowApp.controller('ListBottomSheetCtrl', function ($scope, $mdBottomSheet) {
    $scope.items = [
      { name: 'Share', icon: 'share-arrow' },
      { name: 'Upload', icon: 'upload' },
      { name: 'Copy', icon: 'copy' },
      { name: 'Print this page', icon: 'print' },
    ];
    $scope.listItemClick = function ($index) {
        var clickedItem = $scope.items[$index];
        $mdBottomSheet.hide(clickedItem);
    };
})