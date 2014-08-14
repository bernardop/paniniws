'use strict';
app.factory('albumsService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    //var serviceBase = 'http://localhost:5349/';
    var albumsServiceFactory = {};

    var _getAlbums = function () {
        
        return $http.get(serviceBase + 'api/albums').then(function (results) {
            return results;
        });
    };

    albumsServiceFactory.getAlbums = _getAlbums;

    return albumsServiceFactory;

}]);