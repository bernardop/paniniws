'use strict';
app.factory('missingStickersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var missingStickersServiceFactory = {};

    var _getMissingStickers = function (albumID) {

        return $http.get(serviceBase + 'api/albums/' + albumID + '/GetMissingStickersList').then(function (results) {
            return results;
        });
    };

    missingStickersServiceFactory.getMissingStickers = _getMissingStickers;

    return missingStickersServiceFactory;

}]);