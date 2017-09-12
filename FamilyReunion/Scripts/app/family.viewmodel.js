function FamilyViewModel(app, dataModel, familyId, name, createDate) {
    var self = this;

    self.familyId = ko.observable(familyId || "");
    self.name = ko.observable(name || "");
    self.createDate = ko.observable(createDate || "");
    self.families = ko.observableArray([]);
    self.members = ko.observableArray([]);
    self.createFamilyVisible = ko.observable(false);
    self.canJoin = ko.observable(false);

    self.set = function (family) {
        if (family) {
            self.familyId(family.familyId);
            self.name(family.name);
            self.createDate(family.createDate);
            self.setMembers(family.familyMembers);
        }
    };
    self.setMembers = function (members) {
        self.members([]);
        $.each(members, function (key, value) {
            self.members.push(value);
        });
    };
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
        delete item.members;

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
        delete item.members;

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
    self.join = function (existingMemberId, successFunc) {
        var item = ko.toJS(this);
        var familyId = item.familyId;
        var memberId = existingMemberId || app.Views["Home"].me().memberId;
        common.apiAdd(app.dataModel.FamilyMembers, { familyId: familyId, memberId: memberId }, function (data) {
            app.Views["Home"].init();
            if (typeof (successFunc) == "function") {
                successFunc(data);
            }
        });
    };
    self.leave = function () {
        var item = ko.toJS(this);
        var familyId = item.familyId;
        $.each(app.Views["Home"].me().families, function (key, value) {
            if (value.familyId != familyId)
                return;
            common.apiDelete(app.dataModel.FamilyMembers, value.familyMemberId, function () {
                app.Views["Home"].init();
            });
        });
    };
    self.hasFamily = function (familyId) {
        var result = false;
        $.each(app.Views["Home"].me().families, function (key, value) {
            if (value.familyId == familyId) {
                result = true;
                return;
            }
        });
        return result;
    };
    return self;
}