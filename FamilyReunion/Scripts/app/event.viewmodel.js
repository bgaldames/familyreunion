function EventViewModel(app, dataModel) {
    var self = this;

    self.summary = ko.observable("");
    self.location = ko.observable("");
    self.description = ko.observable("");
    self.start = ko.observable("");
    self.hours = ko.observable("1");
    self.timeZone = ko.observable("America/New_York");
    self.reminders = ko.observable({
        'useDefault': false,
        'overrides': [
            { 'method': 'email', 'minutes': 2 * 60 },
            { 'method': 'popup', 'minutes': 10 }
        ]
    });
    
    self.add = function () {
        var item = ko.toJS(this);
        // https://developers.google.com/google-apps/calendar/create-events
        var event = {
            'summary': item.summary,
            'location': item.location,
            'description': item.description,
            'start': {
                'dateTime': moment(item.start).format(),
                'timeZone': item.timeZone
            },
            'end': {
                'dateTime': moment(item.start).add(item.hours, 'hours').format(),
                'timeZone': item.timeZone
            },
            'reminders': item.reminders
        };

        var request = gapi.client.calendar.events.insert({
            'calendarId': dataModel.familyVacationCalendarId,
            'resource': event
        });

        request.execute(function (event) {
            console.log('Event created: ' + event.htmlLink);
            $('#eventModal').modal('hide');
            $('#calendarFrame').attr('src', $('#calendarFrame').attr('src'));
        });
    };
    self.edit = function () {
        console.error('Action Not spported');
    };
    self.delete = function () {
        console.error('Action Not spported');
    };
    return self;
}