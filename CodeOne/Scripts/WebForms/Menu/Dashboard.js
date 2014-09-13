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
});


function showGrid() {
    alert('hi');
}