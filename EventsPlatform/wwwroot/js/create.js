$(window).on('load', function () {
    changeInputs();
});

function changeInputs() {
    var eventType = $('#group-event-type').children('select[name=EventType]').val();
    if (eventType == 0) { // Virtual
        $('#group-location-town').css('display', 'none');
        $('#group-location-town > :input').prop('disabled', true);
        $('#group-location-street').css('display', 'none');
        $('#group-location-street > :input').prop('disabled', true);
        $('#group-location-otherdetails').css('display', 'none');
        $('#group-location-otherdetails > :input').prop('disabled', true);
    } else if (eventType == 1) { // Real
        $('#group-location-town').css('display', '');
        $('#group-location-town > :input').prop('disabled', false);
        $('#group-location-street').css('display', '');
        $('#group-location-street > :input').prop('disabled', false);
        $('#group-location-otherdetails').css('display', '');
        $('#group-location-otherdetails > :input').prop('disabled', false);
    }
}