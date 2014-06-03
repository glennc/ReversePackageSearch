
angular.module("ReversePackageSearch", [])
       .controller('SearchController',
            function ($scope, $http) {

                $scope.results = [];

                $scope.search = function (searchTerms) {
                    console.log("searching...");
                    $http.get("/Search/?searchTerm=" + searchTerms)
                         .then(function success(results) { $scope.results = results.data; });
                }
            }
);