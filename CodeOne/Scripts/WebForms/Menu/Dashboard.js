$(document).ready(function () {
    $("[id*='ancViewTen']").click(function (e) {

        if ($(this).text() == "Hide Last 10 Transactions") {
            document.getElementById('grid').style.display = 'none';
            $(this).text("Last 10 Transactions");
        }
        else {
            document.getElementById('grid').style.display = 'inline-block';
            $(this).text("Hide Last 10 Transactions");
        }
    });

    $("[id*='ancViewPie']").click(function (e) {

        if ($(this).text() == "Close Visualization") {
            document.getElementById('pie').style.display = 'none';
            $(this).text("Visualize Account Data");
        }
        else {
            document.getElementById('pie').style.display = 'inline-block';
            $(this).text("Close Visualization");
        }
    });

});


function showGrid() {
    alert('hi');
}