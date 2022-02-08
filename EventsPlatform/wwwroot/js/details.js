$(document).ready(checkIfEventApplied($('#Id').val()));

function checkIfEventApplied(eventId) {
    let data = {
        EventId: eventId
    };

    $.ajax(
        {
            type: "Get",
            url: "/User/CheckIfEventApplied",
            data: data,
            success: function (response) {
                if (response.status == 'registered') {
                    $('#RegisterButton').css('background-color', 'green');
                    $('#RegisterButton').val('Запиши се');
                    console.log(eventId);
                } else if (response.status == 'unregistered') {
                    $('#RegisterButton').css('background-color', 'red');
                    $('#RegisterButton').val('Отпиши се');
                } else if (response.status == 'full') {
                    $('#RegisterButton').css('background-color', 'grey');
                    $('#RegisterButton').val('Пълно');
                }
            }
        });
}

function applyEvent(eventId) {
    let data = {
        EventId: eventId
    };

    $.ajax(
        {
            type: "Post",
            url: "/User/ApplyEvent",
            data: data,
            success: function (response) {
                if (response.status == 'registered') {
                    $('#RegisterButton').css('background-color', 'red');
                    $('#RegisterButton').val('Отпиши се');
                    console.log(eventId);
                } else if (response.status == 'unregistered') {
                    $('#RegisterButton').css('background-color', 'green');
                    $('#RegisterButton').val('Запиши се');
                }
            }
        });
}

