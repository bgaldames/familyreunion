function ReunionViewModel(app, dataModel, reunionId, year, title, description, location) {
    var self = this;

    self.reunionId = ko.observable(reunionId || "");
    self.year = ko.observable(year || "");
    self.title = ko.observable(title || "");
    self.description = ko.observable(description || "");
    self.location = ko.observable(location || "");
    self.teams = ko.observableArray([]);

    self.add = function () {
        var obj = ko.toJS(this);
        delete obj.teams;

        common.apiAdd(app.dataModel.Reunions, obj);
    };
    self.edit = function () {
        var obj = ko.toJSON(this);
        delete obj.teams;

        common.apiUpdate(app.dataModel.Reunions, data);
    };
    self.delete = function () {
        common.apiDelete(app.dataModel.Reunions, self.ReunionId());
    };
    self.vote = function () {
        //todo: display voting form
    };
    return self;
}