$(function () {
    
    //LoadChart($("[id$=hfAccountNum]").val());
    //$("[id$=btnDraw]").bind("click", function () {
    //    LoadChart()
    //})
});
function LoadChart(accountNum) {
    $.ajax({
        type: "POST",
        url: "Pie.aspx/GetChart",
        data: "{nAccountNum: '" + accountNum + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $("#dvChart").html("");
            $("#dvLegend").html("");
            var data = eval(r.d);
            var el = document.createElement('canvas');
            $("#dvChart")[0].appendChild(el);

            //Fix for IE 8
            if ($.browser.msie && $.browser.version == "8.0") {
                G_vmlCanvasManager.initElement(el);
            }
            var ctx = el.getContext('2d');
            var userStrengthsChart = new Chart(ctx).Doughnut(data);

            for (var i = 0; i < data.length; i++) {
                var div = $("<div />");
                div.css("margin-bottom", "10px");
                div.html("<span style = 'display:inline-block;height:10px;width:10px;background-color:" + data[i].color + "'></span> " + data[i].text);
                $("#dvLegend").append(div);
            }
        },
        failure: function (response) {
            alert('There was an error.');
        }
    });
}