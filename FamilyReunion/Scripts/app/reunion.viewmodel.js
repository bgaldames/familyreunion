﻿function ReunionViewModel(app, dataModel) {
    var self = this;

    self.reunionId = ko.observable("");
    self.year = ko.observable("");
    self.title = ko.observable("");
    self.description = ko.observable("");
    // consider google place id
    self.location = ko.observable("");
    self.startDate = ko.observable("");
    self.endDate = ko.observable("");
    self.googlePlaceId = ko.observable("");
    self.googleFormId = ko.observable("");

    self.teams = ko.observableArray([]);
    self.reunions = ko.observableArray([]);
    self.createReunionVisible = ko.observable(false);
    self.calendarUrl = ko.observable("");

    self.set = function (reunion) {
        if (reunion != null) {
            self.reunionId(reunion.reunionId);
            self.year(reunion.year);
            self.title(reunion.title);
            self.description(reunion.description);
            self.location(reunion.location);
            self.startDate(reunion.startDate);
            self.endDate(reunion.endDate);
            self.googlePlaceId(reunion.googlePlaceId);
            self.googleFormId(reunion.googleFormId);
            self.setTeams(reunion.teams);
            self.calendarUrl("https://calendar.google.com/calendar/embed?"
                + "showTitle=0&showNav=0&showDate=0&showPrint=0&showTabs=0&showCalendars=0&showTz=0"
                + "&src=" + dataModel.familyVacationCalendarId
                + "&wkst=1&mode=WEEK"
                + "&dates=" + moment(reunion.startDate).format("YYYYMMDD") + "/" + moment(reunion.endDate).format("YYYYMMDD"));
        }
    };
    self.setTeams = function (teams) {
        self.teams([]);
        $.each(teams, function (key, value) {
            self.teams.push(value);
        });
    };
    self.init = function () {
        common.api(app.dataModel.Reunions, function (data) {
            self.reunions([]);
            $.each(data, function (key, value) {
                self.reunions.push(value);
            });
        });
    };
    self.createReunion = function () {
        self.createReunionVisible(!self.createReunionVisible());
    };
    self.add = function () {
        var item = ko.toJS(this);
        delete item.reunionId;
        delete item.teams;
        delete item.reunions;
        delete item.createReunionVisible;
        delete item.calendarUrl;

        common.apiAdd(app.dataModel.Reunions, item, function () {
            self.init();
            $('#reunionForm')[0].reset();
            self.createReunionVisible(false);
            $('#reunionModal').modal('hide');
        });
    };
    self.edit = function () {
        var item = ko.toJS(this);
        delete item.teams;
        delete item.reunions;
        delete item.createReunionVisible;
        delete item.calendarUrl;

        common.apiUpdate(app.dataModel.Reunions, item.reunionId, item, function () {
            self.init();
            $('#reunionModal').modal('hide');
        });
    };
    self.delete = function () {
        var item = ko.toJS(this);
        common.apiDelete(app.dataModel.Reunions, item.reunionId, function () {
            self.init();
        });
    };
    self.vote = function () {
        //todo: display voting form
    };
    self.join = function () {
        var item = ko.toJS(this);
        var reunionId = item.reunionId;
        var memberId = app.Views["Home"].me().memberId;;
        common.apiAdd(app.dataModel.ReunionMembers, { reunionId: reunionId, memberId: memberId }, function () {
            app.Views["Home"].init();
        });
    };
    self.leave = function () {
        var item = ko.toJS(this);
        var reunionId = item.reunionId;
        $.each(app.Views["Home"].me().reunions, function (key, value) {
            if (value.reunionId != reunionId)
                return;
            common.apiDelete(app.dataModel.ReunionMembers, value.reunionMemberId, function () {
                app.Views["Home"].init();
            });
        });
    };
    self.hasReunion = function (reunionId) {
        var result = false;
        $.each(app.Views["Home"].me().reunions, function (key, value) {
            if (value.reunionId == reunionId) {
                result = true;
                return;
            }
        });
        return result;
    };
    self.detail = function (item) {
        self.set(item);
    };
    self.assignTeams = function () {
        common.api(app.dataModel.AssignTeams, { reunionId: self.reunionId() }, function () {
            app.Views["Home"].init();
        });
    };
    return self;
}