var appointments = [];
$(".appointments").each(function () {
    var title = $(".title", this).text().trim();
    var appdate = $(".appdate", this).text().trim();
    var event = {
        "title": title,
        "start": appdate
    };
    appointments.push(event);
});

$("#calendar").fullCalendar({
    locale: 'au',
    events: appointments,
    dayClick: function (date, allDay, jsEvent, view) {
        var d = new Date(date);
        var m = moment(d).format("YYYY-MM-DD");
        m = encodeURIComponent(m);
        var uri = "/Appointments/Create?date=" + m;
        $(location).attr('href', uri);
    }
});
