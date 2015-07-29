var sparrowApp = angular.module('sparrowApp', [
    'ui.router', 'angularFileUpload', 'ImageCropper', 'ngMaterial', 'ngCookies'])
.config(function($mdThemingProvider) {
    $mdThemingProvider.theme('default')
        .primaryPalette('orange')
        .accentPalette('pink');

});
