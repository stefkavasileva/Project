var visitationSpan = $('#visitationValue');
var visitationButton = $('#visitationButton');

var desiredVisitationSpan = $('#desiredVisitationValue');
var desiredVisitationButton = $('#desiredPlacesVisitationButton');

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
                $(visitationButton).removeClass('secondary-color');
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
                $(visitationButton).addClass('secondary-color');
            }
        });
    }
}

function onLoadVisitation(url, lblVisited, lblVisit) {
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
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
                $(visitationButton).removeClass('secondary-color');
                $(visitationButton).addClass('btn-success');

            } else {

                $(visitationSpan).text(lblVisit);
                $(visitationSpan).attr('value', false);
            }
        }
    });
}

function checkDesiredVisitation(makeDesiredVisitationUrl, removeFromDesiredVisitationUrl, lblDesiredVisited, lblDesiredVisit) {
    var isVisited = $(desiredVisitationSpan).attr('value') === "true";

    if (!isVisited) {
        $.ajax({
            type: "POST",
            url: makeDesiredVisitationUrl,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.redirectUrl) {
                    window.location = response.redirectUrl;
                }
                $(desiredVisitationSpan).text(lblDesiredVisited);
                $(desiredVisitationSpan).attr('value', true);
                $(desiredVisitationButton).removeClass('secondary-color');
                $(desiredVisitationButton).addClass('btn-success');
            }
        });
    } else {
        $.ajax({
            type: "POST",
            url: removeFromDesiredVisitationUrl,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.redirectUrl) {
                    window.location = response.redirectUrl;
                }
                $(desiredVisitationSpan).text(lblDesiredVisit);
                $(desiredVisitationSpan).attr('value', false);
                $(desiredVisitationButton).removeClass('btn-success');
                $(desiredVisitationButton).addClass('secondary-color');
            }
        });
    }
}

function onLoadDesiredVisitation(url, lblDesiredVisited, lblDesiredVisit) {
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            var isVisited = JSON.parse(response.isVisited);

            if (lblDesiredVisited.length > 10) {
                lblDesiredVisited = 'Желано за посещение!';
            }
            if (lblDesiredVisit.length > 20) {
                lblDesiredVisit = 'Желая да посетя!'; 
            }

            if (isVisited) {

                $(desiredVisitationSpan).text(lblDesiredVisited);
                $(desiredVisitationSpan).attr('value', true);
                $(desiredVisitationButton).removeClass('secondary-color');
                $(desiredVisitationButton).addClass('btn-success');

            } else {

                $(desiredVisitationSpan).text(lblDesiredVisit);
                $(desiredVisitationSpan).attr('value', false);
            }
        }
    });
}


