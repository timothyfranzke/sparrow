sparrowApp.config(function ($stateProvider) {
    $stateProvider
        .state('portal.artist', {
            url: '/artist',
            templateUrl: 'App/Pages/Portal/Pages/Artist/Partials/Artist.html',
            controller: 'ArtistController'
        })
        .state('portal.user', {
            url: '/user',
            templateUrl: 'App/Pages/Portal/Pages/User/Partials/User.html',
            controller: 'UserController'
        });
});