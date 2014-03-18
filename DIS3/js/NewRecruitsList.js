$(function () {
    $(".newslist_classify_content").hide();
    $(".newslist_classify_label").toggle(function () {
        $(this).parent().parent().children('.newslist_classify_content').show();
        $(this).children(".jia").html("-");
    }, function () {
        $(this).parent().parent().children('.newslist_classify_content').hide();
        $(this).children(".jia").html("+");
    }
    );

    $(".NewRecruitsContent").hide();
    $(".NewRecruitsDuty").toggle(function () {
        $(this).parent().parent().children('.NewRecruitsContent').show();
        $(this).children(".jia").html("-");
    }, function () {
        $(this).parent().parent().children('.NewRecruitsContent').hide();
        $(this).children(".jia").html("+");
    }
    );
    $(".NewRecruitsCategory").click(function () {
        $(".NewRecruitsCategory").css({ "color":"black" });
        $(this).css({ "color":"red" });
    });  


});