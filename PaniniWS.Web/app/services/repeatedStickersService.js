'use strict';
app.factory('repeatedStickersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var repeatedStickersServiceFactory = {};

    var _getRepeatedStickers = function (albumID) {

        return $http.get(serviceBase + 'api/albums/' + albumID + '/GetRepeatedStickersList').then(function (results) {
            return results;
        });
    };

    repeatedStickersServiceFactory.getRepeatedStickers = _getRepeatedStickers;

    return repeatedStickersServiceFactory;

}]);