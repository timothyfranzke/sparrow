sparrowApp.config(function ($stateProvider) {
    $stateProvider
        .state('home.register', {
            url: '/register',
            templateUrl: 'App/Pages/Home/Pages/Register/Partials/Register.html',
            controller: 'RegisterController'
        })
        .state('home.login', {
            url: '/login',
            templateUrl: 'App/Pages/Home/Pages/Login/Partials/Login.html',
            controller: 'LoginController'
        });
});