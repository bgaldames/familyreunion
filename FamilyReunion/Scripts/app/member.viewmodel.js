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
        $.each(data, function (key, value) {
            self.memberTypes.push(value);
        });
    });

    self.add = function () {
        var data = ko.toJS(this);
        delete data.memberId;
        delete data.memberTypes;

        common.apiAdd(app.dataModel.Members, data, function () {
            app.Views["Home"].showMemberCreation(false);
        });
    };
    self.edit = function () {
        var data = ko.toJS(this);
        delete data.memberTypes;

        common.apiUpdate(app.dataModel.Members, self.memberId(), data, function () {
        });
    };
    self.delete = function () {
        common.apiDelete(app.dataModel.Members, self.memberId(), function () {
        });
    };
    return self;
}
