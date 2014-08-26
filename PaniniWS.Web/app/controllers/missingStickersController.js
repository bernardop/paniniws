'use strict';
app.controller('missingStickersController', ['$scope', '$routeParams', 'missingStickersService', function ($scope, $routeParams, missingStickersService) {

    $scope.message = '';
    $scope.stickers = [];

    missingStickersService.getMissingStickers($routeParams.albumID).then(function (results) {
        var stickers = results.data;

        $scope.stickers = stickers;
    },
    function (err) {
        $scope.message = err.error_description;
    });

}]);