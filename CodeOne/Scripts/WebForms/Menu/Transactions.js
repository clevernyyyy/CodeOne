$(document).ready(function () {

    $('#divRepCategory').draggable({
        containment: '#categories',
        cursor: 'move',
        snap: '#categories'
    });

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