'use strict';
app.controller('stickersController', ['$scope', '$routeParams', 'stickersService', function ($scope, $routeParams, stickersService) {

    $scope.message = '';
    $scope.stickers = [];

    stickersService.getStickers($routeParams.albumID).then(function (results) {
        $scope.stickers = results.data;
    },
    function (err) {
        $scope.message = err.error_description;
    });

}]);