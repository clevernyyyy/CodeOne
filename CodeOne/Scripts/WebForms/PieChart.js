$(function () {
    var hfs = $("[id*='_hfAccountNum']");
    var types = $("[id*='_hfGraphCat']");
    for (index = 0; index < hfs.length; ++index) {
        var hf = hfs[index];
        var type = types[index];
        var acn = hf.value;
        var cat = type.value;

        LoadChart(acn, cat, index);
    }
    //LoadChart($("[id$=hfAccountNum]").val());
    //$("[id$=btnDraw]").bind("click", function () {
    //    LoadChart()
    //})
});
function LoadChart(nID, strType, index) {
    $.ajax({
        type: "POST",
        url: "Pie.aspx/GetChart",
        data: "{nID: '" + nID + "',strType: '" + strType + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $("[id*='_dvChart_" + index + "']").html("");
            $("[id*='_dvLegend_" + index + "']").html("");
            var data = eval(r.d);
            var el = document.createElement('canvas');
            el.setAttribute("id", 'canvas' + index);
            el.setAttribute("height", '450');
            el.setAttribute("width", '450');
            $("[id*='_dvChart']")[index].appendChild(el);

            var ctx = el.getContext('2d');
            var userStrengthsChart = new Chart(ctx).Pie(data);
            //userStrengthsChart.setAttribute("width", "400");
            //userStrengthsChart.setAttribute("height", "400");
            //userStrengthsChart.resize();
            for (var i = 0; i < data.length; i++) {
                var div = $("<div />");
                div.css("margin-bottom", "10px");
                div.html("<span style = 'display:inline-block;height:10px;width:10px;background-color:" + data[i].color + "'></span> " + data[i].text);
                $("[id*='_dvLegend_" + index + "']").append(div);
            }
        },
        failure: function (response) {
            alert('There was an error.');
        }
    });
}
