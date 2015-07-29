sparrowApp.factory('player', function (Audio, $rootScope) {
    var player,
        index = 0,
        playlist = [],
        paused = false,
        current = {
            ArtistId: 0,
            AlbumId: 0,
            TrackId: 0,
        };

    player = {
        playlist: playlist,

        current: current,

        playing: false,

        play: function (item) {
            console.log('playlist length: ' + playlist.length);

            if (!playlist.length) return;

            if (angular.isDefined(item)) current = item;

            if (!paused) {
                console.log('not paused');
                var source = 'Track/GetImageFile?artistId=' + current.ArtistId + '&albumId=' + current.AlbumId + '&trackId=' + current.TrackId;
                console.log('source: ' + source);
                Audio.src = 'Track/GetAudioFile?artistId=' + current.ArtistId + '&albumId=' + current.AlbumId + '&trackId=' + current.TrackId;
            }
                
            Audio.play();
            player.playing = true;
            paused = false;
        },

        pause: function () {
            if (player.playing) {
                Audio.pause();
                player.playing = false;
                paused = true;
            }
        },

        next: function () {
            if (!playlist.length) return;
            paused = false;
            if (playlist.length > index) {
                index++;
                current = playlist[index];
            } else {
                index = 0;
            }
            if (player.playing) player.play();
        }
    };

    playlist.add = function (item) {
        if (playlist.indexOf(item) != -1)
            return;

        playlist.push(item);
        if (playlist.length == 1) {
            current = playlist[0];
        }
    };

    Audio.addEventListener('ended', function () {
        $rootScope.$apply(player.next);
    }, false);

    return player;
});