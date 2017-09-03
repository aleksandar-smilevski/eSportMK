(function () {
    'use strict';

    angular.module('admin')
        .config(function ($routeProvider) {
            $routeProvider
                .when("/users",
                { templateUrl: "adminapp/views/users/list.html" })
                .when("/users/details/:id",
                { templateUrl: "adminapp/views/users/detail.html" })
                .when("/users/about/",
                { template: "<p>about</p>" });
        });
})();