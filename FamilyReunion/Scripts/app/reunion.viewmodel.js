function ReunionViewModel(app, dataModel, reunionId, year, title, description, location) {
    var self = this;

    self.reunionId = ko.observable(reunionId || "");
    self.year = ko.observable(year || "");
    self.title = ko.observable(title || "");
    self.description = ko.observable(description || "");
    self.location = ko.observable(location || "");

    self.teams = ko.observableArray([]);
    self.reunions = ko.observableArray([]);
    self.createReunionVisible = ko.observable(false);

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

        common.apiAdd(app.dataModel.Reunions, item, function () {
            self.init();
            $('#reunionForm')[0].reset();
            self.createReunionVisible(false);
        });
    };
    self.edit = function () {
        var item = ko.toJS(this);
        delete item.teams;
        delete item.reunions;
        delete item.createReunionVisible;

        common.apiUpdate(app.dataModel.Reunions, item.reunionId, item, function () {
            self.init();
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
    return self;
}