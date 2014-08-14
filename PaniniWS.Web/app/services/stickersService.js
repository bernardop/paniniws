'use strict';
app.factory('stickersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var stickersServiceFactory = {};

    var _getStickers = function (albumID) {

        return $http.post(serviceBase + 'api/albums/' + albumID, { }).then(function (results) {
            return results;
        });
    };

    stickersServiceFactory.getStickers = _getStickers;

    return stickersServiceFactory;

}]);