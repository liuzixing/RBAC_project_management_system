$(function () {
    $(".ul_level1").hide();
    $(".a_level1").children(".jia").html("+");
    $(".a_level1").toggle(function (e) {
        e.preventDefault();
        $(this).next().show();
        $(this).children(".jia").html("-");
    }, function (e) {
        e.preventDefault();
        $(this).next().hide();
        $(this).children(".jia").html("+");
    });
    $(".a_level2").toggle(function (e) {
        e.preventDefault();
        $(this).next().show();
    }, function (e) {
        e.preventDefault();
        $(this).next().hide();
    });
});