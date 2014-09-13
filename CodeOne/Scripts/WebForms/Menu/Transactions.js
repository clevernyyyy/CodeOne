$(document).ready(function () {

    $("[id*='expand']").click(function (e) {
        if ($("[id*='expand']").text() == "+") {
            document.getElementById('divRepCategory').style.display = 'block';
            $("[id*='expand']").text("-");
        }
        else {
            document.getElementById('divRepCategory').style.display = 'none';
            $("[id*='expand']").text("+");
        }
    });
})