$(document).ready(function () {
    $("[id*='ancViewTen']").click(function (e) {
        if ($(this).text() == "Hide Last 10 Transactions") {
            $(this).next().next().next().next().css("display", "none")
            $(this).text("Last 10 Transactions");
        }
        else {
            $(this).next().next().next().next().css("display","inline-block")
            $(this).text("Hide Last 10 Transactions");
        }
    });

    $("[id*='ancViewPie']").click(function (e) {

        if ($(this).text() == "Close Visualization") {
            $(this).next().next().next().css("display", "none")
            $(this).text("Visualize Account Data");
        }
        else {
            $(this).next().next().next().css("display", "inline-block")
            $(this).text("Close Visualization");
        }
    });

});

