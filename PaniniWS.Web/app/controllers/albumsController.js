'use strict';
app.controller('albumsController', ['$scope', 'albumsService', function ($scope, albumsService) {

    $scope.message = "Sup";
    $scope.albums = [];
    
    albumsService.getAlbums().then(function (results) {
        $scope.albums = results.data;
    },
    function (err) {
        $scope.message = err.error_description;
    });

}]);