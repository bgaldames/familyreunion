//var memberViewModel = new MemberViewModel('', '', '', '', '', '', true);

function HomeViewModel(app, dataModel) {
    var self = this;

    self.email = ko.observable("");
    self.showMemberCreation = ko.observable("");
    self.me = ko.observable("");

    self.member = new MemberViewModel(app, dataModel);
    self.newMember = new MemberViewModel(app, dataModel);
    self.reunion = new ReunionViewModel(app, dataModel);
    self.family = new FamilyViewModel(app, dataModel);
    self.event = new EventViewModel(app, dataModel);

    function setFamily(family) {
        self.family.set(family);
    };

    function setReunion(reunion) {
        self.reunion.set(reunion);
    };

    function loadDetails() {
        self.family.init();
        self.reunion.init();
    }

    self.init = function () {
        common.api(app.dataModel.userInfoUrl, function (data) {
            console.log(data);
            self.email(data.email);
            self.showMemberCreation(data.member == null);

            if (data.member != null) {
                self.me(data.member);
                self.family.canJoin(data.member.families.length == 0);
                loadDetails();
                setFamily(data.family);
                setReunion(data.reunion);
                self.newMember.init();
            } else {
                self.member.email(data.email);
                self.member.init();
            }
        });
    };

    Sammy(function () {
        this.get('#home', function () {
            self.init();
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
