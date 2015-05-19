sparrowApp.config(function ($urlRouterProvider, $stateProvider) {
    $urlRouterProvider.otherwise("/home");
    $stateProvider
        .state('home', {
            url: '/home',
            templateUrl: 'App/Pages/Home/Partials/Home.html',
            controller: 'HomeController'
        })
        .state('portal', {
            url: '/portal',
            templateUrl: 'App/Pages/Portal/Partials/Portal.html',
            controller: 'PortalController'
        });
});