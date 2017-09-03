(function () {
    var app = angular.module("admin");

    app.controller("UsersListController", ListController);
    app.controller("UsersDetailsController", DetailsController);

    function ListController($http) {
        var vm = this;
        vm.message = "list";
        vm.entities = {};
        $http.get("api/users")
            .then(function (response) {
                vm.entities = response.data;
            });
    }

    function DetailsController($http,$routeParams) {
        var vm = this;
        vm.message = "details";
        vm.entity = [];
        $http.get("api/users/" + $routeParams.id)
            .then(function (response) {
                console.log(response.data);
                vm.entity = response.data;
            });
    }
}());