/* JQuery Custom Functions */
var con = 1;
function Looper(a,c)
{
    var bindLeftNav = '';
    var b = a[c].navChild_Attr;
    if (JQ(b).length > 0) {
        bindLeftNav += '<ul class="l'+(con++)+'">';
        JQ(b).each(function (i) { bindLeftNav += '<li><div><i class="' + b[i].faName + '" aria-hidden="true"></i>' + b[i].navName + '</div>' + Looper(b, i) + '</li>' });
        bindLeftNav += '</ul>';
    }
    return bindLeftNav;
}

/* Angular Script */


var universal = angular.module("universal", []);
universal.controller('universalController', ["$scope", "$http", "$window", function ($scope, $http, $window) {
    $scope.username = 'Username';
    $http({
        method: "POST",
        url: "/dash/LeftNavs",
        dataType: 'json'
    }).then(function (response) {
        //$window.document.write(JSON.stringify(response.data));
        $scope.leftnavs = response.data;

        var bindLeftNav = '';
        var a = $scope.leftnavs;
        if (JQ(a).length > 0) {
            bindLeftNav = '<ul>';
            JQ(a).each(function (i) { bindLeftNav += '<li><div><i class="' + a[i].faName + '" aria-hidden="true"></i> ' + a[i].navName + '</div>' + Looper(a, i) + '</li>' });
            bindLeftNav += '</ul>'
        }
        bindLeftNav = bindLeftNav.replace(/<\/div><ul/g, '<i class="fa fa-chevron-right"></i></div><ul')
        JQ('#leftNav').html(bindLeftNav);
    });

}]);

/* jQuery Script */

JQ(document).ready(function () {
    JQ('#loaderInit').delay(300).fadeOut();
    setTimeout(function () { JQ('#loaderInit').remove() }, 600);
});