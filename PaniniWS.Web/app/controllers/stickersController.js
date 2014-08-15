'use strict';
app.controller('stickersController', ['$scope', '$routeParams', 'stickersService', function ($scope, $routeParams, stickersService) {

    $scope.message = '';
    $scope.stickers = [];
    $scope.albumPages = [];
    $scope.currentPage = 1;

    $scope.filterByPageID = function (element) {
        return element.albumSticker.albumPage.albumPageID == $scope.currentPage ? true : false
    }

    stickersService.getStickers($routeParams.albumID).then(function (results) {
        var stickers = results.data;

        $scope.stickers = stickers;

        var flags = [], output = [], l = stickers.length, i;
        for (i = 0; i < l; i++) {
            if (flags[stickers[i].albumSticker.albumPage.albumPageID]) continue;
            flags[stickers[i].albumSticker.albumPage.albumPageID] = true;
            output.push({ pageID: stickers[i].albumSticker.albumPage.albumPageID, description: stickers[i].albumSticker.albumPage.description });
        }

        $scope.albumPages = output;
    },
    function (err) {
        $scope.message = err.error_description;
    });

}]);