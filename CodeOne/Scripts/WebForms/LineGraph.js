$(function () {
    var hf = $("[id$='_hfUserId']");
    var acn = hf.val();
    LoadChart(acn);
});
function LoadChart(nID) {
    $.ajax({
        type: "POST",
        url: "BudgetTargets.aspx/GetChart",
        data: "{nUserID: '" + nID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $("[id*='_dvChart']").html("");
            //$("[id*='_dvLegend_" + nID + "']").html("");
            var data = eval(r.d);
            var el = document.createElement('canvas');
            el.setAttribute("id", 'canvas' + index);
            el.setAttribute("height", '300');
            el.setAttribute("width", '450');
            $("[id*='_dvChart']")[index].appendChild(el);

            var ctx = el.getContext('2d');
            var userStrengthsChart = new Chart(ctx).Line(data);
            //userStrengthsChart.setAttribute("width", "400");
            //userStrengthsChart.setAttribute("height", "400");
            //userStrengthsChart.resize();
            //for (var i = 0; i < data.length; i++) {
            //    var div = $("<div />");
            //    div.css("margin-bottom", "5px");
            //    div.css("margin-left", "20px");
            //    div.html("<span style = 'display:inline-block;height:8px;width:10px;background-color:" + data[i].color + "'></span> " + data[i].text);
            //    $("[id*='_dvLegend_" + nID + "']").append(div);
            //}
        },
        failure: function (response) {
            alert('There was an error.');
        }
    });
}
