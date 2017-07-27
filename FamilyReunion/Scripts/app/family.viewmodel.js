function FamilyViewModel(app, dataModel, familyId, name, createDate) {
    var self = this;

    self.familyId = ko.observable(familyId || "");
    self.name = ko.observable(name || "");
    self.createDate = ko.observable(createDate || "");
    self.families = ko.observableArray([]);
    self.createFamilyVisible = ko.observable(false);
    self.canJoin = ko.observable(false);

    self.init = function () {
        common.api(app.dataModel.Families, function (data) {
            self.families([]);
            $.each(data, function (key, value) {
                self.families.push(value);
            });
        });
    };
    self.createFamily = function () {
        self.createFamilyVisible(!self.createFamilyVisible());
    };
    self.add = function () {
        var item = ko.toJS(this);
        delete item.familyId;
        delete item.createDate;
        delete item.canJoin;
        delete item.families;
        delete item.createFamilyVisible;

        common.apiAdd(app.dataModel.Families, item, function () {
            self.init();
            $('#familyForm')[0].reset();
            self.createFamilyVisible(false);
        });
    };
    self.edit = function () {
        var item = ko.toJS(this);
        delete item.createDate;
        delete item.families;
        delete item.canJoin;
        delete item.createFamilyVisible

        common.apiUpdate(app.dataModel.Families, item.familyId, item, function () {
            self.init();
        });
    };
    self.delete = function () {
        var item = ko.toJS(this);

        common.apiDelete(app.dataModel.Families, item.familyId, function () {
            self.init();
        });
    };
    self.join = function () {
        var item = ko.toJS(this);

        common.apiAdd(app.dataModel.FamilyMembers, { familyId: item.familyId, memberId: '' }, function () {
            
        });
    };
    return self;
}