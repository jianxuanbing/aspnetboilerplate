//XW.AdminUI
var resizeCount = 0;
var tabCount = 17;
!window.jQuery && document.write("<script src=\"js/jquery-1.11.3.min.js\"><\/script>");
$(document).ready(function () {
    $("#side-menu").metisMenu();
    tabInit();
    $(document).click(function () {
        var showState;
        if ($("#user").is(":focus")) {
            showState = $("#userbox").is(":visible");
            if (showState) { $("#userbox").hide().parent().css("background", ""); }
            else { $("#userbox").show().parent().css("background", "#367FA9"); }
        }
        else { $("#userbox").hide().parent().css("background", ""); }
        if ($("#tabconfig").is(":focus")) {
            showState = $(".configbox").is(":visible");
            if (showState) { $(".configbox").hide(); }
            else { $(".configbox").show(); }
        }
        else { $(".configbox").hide(); }

        if ($("#languageconfig").is(":focus")) {
            showState = $(".configbox[id='language']").is(":visible");
            if (showState) { $(".configbox[id='language']").hide(); }
            else { $(".configbox[id='language']").show(); }
        }
        else { $(".configbox[id='language']").hide(); }
    });
    $(window).on("load resize", function () {
        $("#message").css("width", $(".right").width() - $("#menu").width() - 123);
        var pageHeight = $(window).height() - 50;
        $(".page").css("height", pageHeight);
        if (!$.support.leadingWhitespace) { $("#contents").css("height", "100%").css("height", "-=40px"); }
        $(".accordion").slimScroll({ width: "230px", height: pageHeight, railVisible: true, railOpacity: .4, wheelStep: 10, alwaysVisible: false });
    });
    $("#message").marquee();
    indexLoad();
});
function layerLoading() {
    var tar = arguments[0] || null;
    layer.load(0, { shade: 0 });
    if (tar) { tar.attr("src", tar.attr("url")); } 
    $(".contents iframe:visible").load(function () { layer.closeAll() });
}
function indexLoad() {
    var wtar = $(".contents iframe:first");
    setTimeout(function () { layerLoading(wtar); }, 1500);
}