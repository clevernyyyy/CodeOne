$(function () {
    var hf = $("[id$='_hfLineGraphUserId']");
    var uid = hf.val();
    LoadLineChart(uid);
});
function LoadLineChart(nID) {
    $.ajax({
        type: "POST",
        url: "BudgetTargets.aspx/GetChart",
        data: "{nUserId: '" + nID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $("#dvChart").html("");
            $("#dvLegend").html("");
            var data = r.d; //eval(r.d);
            //var json = JSON.parse(data);
            var json;
            if (nID == 4) {
                json = {
                    labels: ["Restaurants/Bars", "Grocery", "Gas", "Recreation"],
                    datasets: [
                        {
                            label: "Target Spending",
                            fillColor: "rgba(146,146,146,0.2)",
                            strokeColor: "rgba(146,146,146,1)",
                            pointColor: "rgba(146,146,146,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(146,146,146,1)",
                            data: [1000, 2000, 1500, 3000]
                        },
                        {
                            label: "Actual Spending",
                            fillColor: "rgba(32,32,32,0.2)",
                            strokeColor: "rgba(32,32,32,1)",
                            pointColor: "rgba(32,32,32,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(32,32,32,1)",
                            data: [3521.01, 12346.24, 1139.07, 467.45]
                        }
                    ]
                };
            } else if (nID == 5) {
                json = {
                    labels: ["Restaurants/Bars", "Grocery", "Gas", "Recreation"],
                    datasets: [
                        {
                            label: "Target Spending",
                            fillColor: "rgba(146,146,146,0.2)",
                            strokeColor: "rgba(146,146,146,1)",
                            pointColor: "rgba(146,146,146,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(146,146,146,1)",
                            data: [2700, 5400, 3000, 9000]
                        },
                        {
                            label: "Actual Spending",
                            fillColor: "rgba(32,32,32,0.2)",
                            strokeColor: "rgba(32,32,32,1)",
                            pointColor: "rgba(32,32,32,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(32,32,32,1)",
                            data: [3639.92, 5856.91, 1201.11, 1344.10]
                        }
                    ]
                };
            } else if (nID == 6) {
                json = {
                    labels: ["Restaurants/Bars", "Grocery", "Gas", "Recreation"],
                    datasets: [
                        {
                            label: "Target Spending",
                            fillColor: "rgba(146,146,146,0.2)",
                            strokeColor: "rgba(146,146,146,1)",
                            pointColor: "rgba(146,146,146,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(146,146,146,1)",
                            data: [2000, 4000, 3000, 6000]
                        },
                        {
                            label: "Actual Spending",
                            fillColor: "rgba(32,32,32,0.2)",
                            strokeColor: "rgba(32,32,32,1)",
                            pointColor: "rgba(32,32,32,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(32,32,32,1)",
                            data: [49.48, 0, 0, 0]
                        }
                    ]
                };
            }
            var el = document.createElement('canvas');
            el.setAttribute("height", '300');
            el.setAttribute("width", '450');
            $("#dvChart")[0].appendChild(el);

            var ctx = el.getContext('2d');
            var budgetChart = new Chart(ctx).Line(json);
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
