
$(function () {
    var a = $(".item a");
    var url = document.location.href;
    var i,j;
    i = url.substring(url.lastIndexOf("=") + 1, url.length)
    $.each(a, function (key, value) {
       j=$(value).attr("href").substring($(value).attr("href").lastIndexOf("=")+1,$(value).attr("href").length);
       if (j == i) $(value).css({ "color": "#E65D06" });
    });
     
});


