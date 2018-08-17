function CRate(r) {
    $("#Rating").val(r);
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).attr('class', 'starGlow');
    }
    // unselect remaining
    for (var i = r + 1; i <= 5; i++) {
        $("#Rate" + i).attr('class', 'starFade');
    }
}

function CRateOver(r) {
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).attr('class', 'starGlow');
    }
}

function CRateOut(r) {
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).attr('class', 'starFade');
    }
}

function CRateSelected() {
    var setRating = $("#Rating").val();
    for (var i = 1; i <= setRating; i++) {
        $("#Rate" + i).attr('class', 'starGlow');
    }
}

$(function () {
    SCRate();
});