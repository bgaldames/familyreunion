function FamilyViewModel(app, dataModel, familyId, name, createDate) {
    var self = this;

    self.FamilyId = ko.observable(familyId || "");
    self.Name = ko.observable(name || "");
    self.CreateDate = ko.observable(createDate || "");

    self.add = function () {
        var data = ko.toJS(this);
        common.apiAdd(app.dataModel.Families, data, function () {
        });
    };
    self.edit = function () {
        var data = ko.toJS(this);
        common.apiUpdate(app.dataModel.Families, self.FamilyId(), data, function () {
        });
    };
    self.delete = function () {
        common.apiDelete(app.dataModel.Families, self.FamilyId(), function () {
        });
    };
    return self;
}