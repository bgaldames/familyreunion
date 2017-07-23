//var memberViewModel = new MemberViewModel('', '', '', '', '', '', true);

function HomeViewModel(app, dataModel) {
    var self = this;

    self.email = ko.observable("");
    self.showMemberCreation = ko.observable("");
    self.me = ko.observable("");
    self.famlies = ko.observableArray([]);
    self.reunions = ko.observableArray([]);
    self.member = new MemberViewModel(app, dataModel);
    self.reunion = new ReunionViewModel(app, dataModel);
    self.family = new FamilyViewModel(app, dataModel);
    self.createReunionVisible = ko.observable(false);

    self.createReunion = function () {
        self.createReunionVisible(!self.createReunionVisible());
    };
    
    function loadDetails() {
        common.api(app.dataModel.Families, function (data) {
            $.each(data, function (key, value) {
                self.families.push(value);
            });
        });
        common.api(app.dataModel.Reunions, function (data) {
            self.reunions = data;
            $.each(data, function (key, value) {
                self.reunions.push(value);
            });
        });
    }

    Sammy(function () {
        this.get('#home', function () {
            common.api(app.dataModel.userInfoUrl, function (data) {
                console.log(data);
                self.me(data);
                self.email(data.email);
                self.showMemberCreation(data.member == null);

                if (data.member != null) {
                    loadDetails();
                } else {
                    self.member.email(data.email);
                }
            });
        });
        this.get('/', function () { this.app.runRoute('get', '#home') });
    });

    return self;
}


app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});
