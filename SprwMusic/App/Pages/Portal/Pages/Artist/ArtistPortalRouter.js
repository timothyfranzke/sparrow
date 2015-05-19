sparrowApp.config(function ($stateProvider) {
    $stateProvider
        .state('portal.artist.artist', {
            url: '/artist',
            templateUrl: 'App/Pages/Portal/Pages/Artist/Pages/Create/Partials/Artist.html',
            controller: 'ArtistController'
        })
        .state('portal.artist.album', {
            url: '/album',
            templateUrl: 'App/Pages/Portal/Pages/Artist/Pages/Create/Partials/Album.html',
            controller: 'ArtistController'
        })
        .state('portal.artist.track', {
            url: '/track',
            templateUrl: 'App/Pages/Portal/Pages/Artist/Pages/Create/Partials/Track.html',
            controller: 'ArtistController'
        });
});