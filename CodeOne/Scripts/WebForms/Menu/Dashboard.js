$(document).ready(function () {

});

function ShowHideTransactions(e) {
    if (e.text == "Hide Last 10 Transactions") {
        e.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling.style.display='none';
        e.text = 'Last 10 Transactions';
    }
    else {
        e.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling.style.display='inline-block';
        e.text = 'Hide Last 10 Transactions';
    }
}

function ShowHideGraph(e) {
    if (e.text == "Close Visualization") {
        e.nextElementSibling.nextElementSibling.nextElementSibling.style.display = 'none';
        e.text = "Visualize Account Data";
    }
    else {
        e.nextElementSibling.nextElementSibling.nextElementSibling.style.display = 'inline-block';
        e.text = "Close Visualization";
    }
}