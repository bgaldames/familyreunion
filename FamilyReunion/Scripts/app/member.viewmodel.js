function MemberViewModel(app, dataModel, memberId, memberTypeId, name, email, cellphone, dateofBirth, isEligibleForWork) {
    var self = this;

    self.memberId = ko.observable(memberId || "");
    self.memberTypeId = ko.observable(memberTypeId || "");
    self.name = ko.observable(name || "");
    self.email = ko.observable(email || "");
    self.cellPhone = ko.observable(cellphone || "");
    self.dateOfBirth = ko.observable(dateofBirth || "");
    self.isEligibleForWork = ko.observable(isEligibleForWork || true);
    self.memberTypes = ko.observableArray([]);

    common.api(app.dataModel.MemberTypes, function (data) {
        self.memberTypes([]);
        $.each(data, function (key, value) {
            self.memberTypes.push(value);
        });
    });

    self.add = function () {
        var item = ko.toJS(this);
        delete item.memberId;
        delete item.memberTypes;

        common.apiAdd(app.dataModel.Members, item, function () {
            app.Views["Home"].showMemberCreation(false);
        });
    };
    self.edit = function () {
        var item = ko.toJS(this);
        delete item.memberTypes;

        common.apiUpdate(app.dataModel.Members, item.memberId, item, function () {
        });
    };
    self.delete = function () {
        var item = ko.toJS(this);

        common.apiDelete(app.dataModel.Members, item.memberId, function () {
        });
    };
    return self;
}
