(function () {
    var app = angular.module("admin", ["ngRoute"]);
    var config = function ($routeProvider, $locationProvider) {
        $routeProvider
            .when("/home",
            { templateUrl: "adminapp/views/home.html" })
            //.when("/users",
            //{ templateUrl: "adminapp/views/users/list.html" })
            //.when("/users/details/:id",
            //{ template: "<p>about</p>" })
            //.when("/users/about/",
            //{ template: "<p>about</p>" })
            .when("/teams",
            { templateUrl: "adminapp/views/teams/list.html" })
            .otherwise(
            { redirectTo: "/home" });
    };
    app.config(config);
} ());