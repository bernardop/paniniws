'use strict';
app.factory('albumsService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:5349/';
    var albumsServiceFactory = {};

    var _getAlbums = function () {

        return $http.get(serviceBase + 'api/useralbumstickers').then(function (results) {
            return results;
        });
    };

    albumsServiceFactory.getAlbums = _getAlbums;

    return albumsServiceFactory;

}]);