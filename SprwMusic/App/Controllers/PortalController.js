sparrowApp.controller('PortalController', function ($scope, $mdDialog, Auth, Artist) {

    $scope.alert = '';
    $scope.showAlert = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app
        // Modal dialogs should fully cover application
        // to prevent interaction outside of dialog
        $mdDialog.show(
          $mdDialog.alert()
            .title('Create An Artist')
            .content('You can specify some description text in here.')
            .ariaLabel('Alert Dialog Demo')
            .ok('Create')
            .targetEvent(ev)
        );
    };

    $scope.showArtist = function (ev) {
        $mdDialog.show({
            controller: 'ArtistController',
            templateUrl: 'App/Partials/CreateArtistForm.html',
            parent: angular.element(document.body),
            targetEvent: ev,
        })
        .then(function (artist) {
            alert(artist.id);
        }, function () {
            $scope.alert = 'You cancelled the dialog.';
        });
    };

    $scope.showImageCrop = function (ev) {
        $mdDialog.show({
            controller: 'ArtistController',
            templateUrl: 'App/Partials/ImageCrop.html',
            parent: angular.element(document.body),
            targetEvent: ev,
        })
        .then(function (artist) {
            alert(artist.id);
        }, function () {
            $scope.alert = 'You cancelled the dialog.';
        });
    };
});