$(function () {
    //$(".newslist_classify_content").hide();
    $(".newslist_classify_label").click(function () {
        if ($(this).children(".jia").html() == "-") {
            $(this).parent().parent().children('.newslist_classify_content').hide(100);
            $(this).children(".jia").html("+");
        }
        else {
            $(this).parent().parent().children('.newslist_classify_content').show(100);
            $(this).children(".jia").html("-");
        }
    })}
    );
// 
//     $(".newslist_classify1").hide();
//     $(".newsCategory").toggle(function (e) {
//         e.preventDefault();
//         $(this).html("- 新闻分类");
//         $(".newslist_classify1").show();
//     }, function (e) {
//         e.preventDefault();
//         $(this).html("+新闻分类");
//         $(".newslist_classify1").hide();
//     }
//     );

  /*  $(".newslist_classify").show();*/
//     $(".newsArchive").toggle(function (e) {
//         e.preventDefault();
//         $(".newslist_classify").show();
//         $(this).html("- 新闻归档");
//     }, function (e) {
//         e.preventDefault();
//         $(".newslist_classify").hide();
//         $(this).html("+新闻归档");
//     }
//     );
//     $(".newslist_classify_label1").click(function () {
//         $(".newslist_classify_label1").css({ "color": "black" });
//         $(this).css({ "color": "red" });
//     });
// 

// function showsubMenu(){
//     $(".newslist_classify_content").show();
//     $(".newslist_classify").show();
//     $(".newslist_classify_label").toggle(function () {
//         $(this).parent().parent().children('.newslist_classify_content').show();
//         $(this).children(".jia").html("-");
//     },
//      function () {
//          $(this).parent().parent().children('.newslist_classify_content').hide();
//          $(this).children(".jia").html("+");
//      });
//     alert(1);
//     var url = document.location.href;
/*
    var year, month, year_id, month_id;
    year = url.substring(url.indexOf("=") + 1, url.indexOf("=") + 5);
    month = url.substring(url.lastIndexOf("=") + 1, url.length);
    $("#year_" + year).children(".jia").html("-");
    $("#year_" + year).parent().parent().show();
    $("#year_" + year + "month_" + month).show();*/



 //}
 $(function () {
     /* $(".newslist_classify_content").();*/
     $(".newslist_classify").show();
     var url = document.location.href;
     var year, month;
     year = url.substring(url.indexOf("=") + 1, url.indexOf("=") + 5);
     month = url.substring(url.lastIndexOf("=") + 1, url.length);
     $("#year_" + year).children(".jia").html("-");
     $("#year_" + year).parent().parent().children('.newslist_classify_content').show();
     $("#year_" + year + "_month_" + month).css({ "color": "red" });
 });