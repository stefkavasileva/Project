var visitationSpan = $('#visitationValue');
var visitationButton = $('#visitationButton');

function checkVisitation(makeVisitedUrl, removeFromVisitedUrl, lblVisited, lblVisit) {
    var isVisited = $(visitationSpan).attr('value') === "true";

    if (!isVisited) {
        $.ajax({
            type: "POST",
            url: makeVisitedUrl,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.redirectUrl) {
                    window.location = response.redirectUrl;
                }
                $(visitationSpan).text(lblVisited);
                $(visitationSpan).attr('value', true);
                $(visitationButton).removeClass('btn-warning');
                $(visitationButton).addClass('btn-success');
            }
        });
    } else {
        $.ajax({
            type: "POST",
            url: removeFromVisitedUrl,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.redirectUrl) {
                    window.location = response.redirectUrl;
                }
                $(visitationSpan).text(lblVisit);
                $(visitationSpan).attr('value', false);
                $(visitationButton).removeClass('btn-success');
                $(visitationButton).addClass('btn-warning');
            }
        });
    }
}

function onLoad(url, lblVisited, lblVisit) {
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var isVisited = JSON.parse(response.isVisited);

            if (lblVisited.length > 10) {
                lblVisited = 'Посетено!';
            }
            if (lblVisit.length > 20) {
                lblVisit = 'Посети!';
            }

            if (isVisited) {

                $(visitationSpan).text(lblVisited);
                $(visitationSpan).attr('value', true);
                $(visitationButton).removeClass('btn-warning');
                $(visitationButton).addClass('btn-success');

            } else {

                $(visitationSpan).text(lblVisit);
                $(visitationSpan).attr('value', false);
            }
        }
    });
}

