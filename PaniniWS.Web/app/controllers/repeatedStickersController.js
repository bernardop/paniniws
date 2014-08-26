'use strict';
app.controller('repeatedStickersController', ['$scope', '$routeParams', 'repeatedStickersService', function ($scope, $routeParams, repeatedStickersService) {

    $scope.message = '';
    $scope.stickers = [];

    repeatedStickersService.getRepeatedStickers($routeParams.albumID).then(function (results) {
        var stickers = results.data;

        $scope.stickers = stickers;
    },
    function (err) {
        $scope.message = err.error_description;
    });

}]);