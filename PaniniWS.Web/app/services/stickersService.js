'use strict';
app.factory('stickersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var stickersServiceFactory = {};

    var _getStickers = function (albumID) {

        return $http.post(serviceBase + 'api/albums/' + albumID, { }).then(function (results) {
            return results;
        });
    };

    var _updateSticker = function (userAlbumStickerID, have, haveRepeated) {
        
        var data = {
            UserAlbumStickerID: userAlbumStickerID,
            Have: have,
            HaveRepeated: haveRepeated
        };

        return $http.post(serviceBase + 'api/albums/updatesticker', data).then(function (results) {
            return results;
        });
    };

    stickersServiceFactory.getStickers = _getStickers;
    stickersServiceFactory.updateSticker = _updateSticker;

    return stickersServiceFactory;

}]);