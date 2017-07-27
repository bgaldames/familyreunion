//var memberViewModel = new MemberViewModel('', '', '', '', '', '', true);

function HomeViewModel(app, dataModel) {
    var self = this;

    self.email = ko.observable("");
    self.showMemberCreation = ko.observable("");
    self.me = ko.observable("");

    self.member = new MemberViewModel(app, dataModel);
    self.reunion = new ReunionViewModel(app, dataModel);
    self.family = new FamilyViewModel(app, dataModel);

    function loadDetails() {
        self.family.init();
        self.reunion.init();
    }

    Sammy(function () {
        this.get('#home', function () {
            common.api(app.dataModel.userInfoUrl, function (data) {
                console.log(data);
                self.me(data);
                self.email(data.email);
                self.showMemberCreation(data.member == null);

                if (data.member != null) {
                    self.family.canJoin(data.member.familyMemberships.length == 0);
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
