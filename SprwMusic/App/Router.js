sparrowApp.config(function ($urlRouterProvider, $stateProvider) {
    $urlRouterProvider.otherwise("/home/login");
    $stateProvider
        .state('home', {
            url: '/home',
            controller: 'HomeController',
            templateUrl: 'App/Partials/Index.html'
        })
        .state('home.login', {
            url: '/login',
            templateUrl: 'App/Partials/Login.html'
        })
        .state('home.register', {
            url: '/register',
            templateUrl: 'App/Partials/Register.html'
        })
        .state('portal', {
            url: '/portal',
            templateUrl: 'App/Partials/Portal.html',
            controller: 'PortalController'
        })
        .state('portal.discover', {
            url: '/discover',
            templateUrl: 'App/Partials/Discover.html'
        })
        .state('portal.mymusic', {
            url: '/mymusic',
            templateUrl: 'App/Partials/PortalMyMusic.html'
        })
        .state('create', {
            url: '/create',
            templateUrl: 'App/Partials/Create.html'
        })
        .state('create.artist', {
            url: '/artist',
            templateUrl: 'App/Partials/CreateArtist.html'
        })
        .state('create.album', {
            url: '/album',
            templateUrl: 'App/Partials/CreateAlbum.html'
        })
        .state('player', {
            url: '/player',
            templateUrl: 'App/Partials/Player.html',
            controller: 'PlayerController'
        })
        .state('player.discover', {
            url: '/discover',
            templateUrl: 'App/Partials/Discover.html',
            controller: 'PlayerController'
        })
        .state('mymusic', {
            url: '/mymusic',
            templateUrl: 'App/Partials/MyMusic.html',
            controller: 'mymusicController'
        })
    ;
});