﻿function AppDataModel() {
    var self = this;
    // Routes
    self.userInfoUrl = "/api/Me";
    self.Members = "/api/Members";
    self.Reunions = "/api/Reunions";
    self.Families = "/api/Families";
    self.MemberTypes = "/api/MemberTypes";
    self.FamilyMembers = '/api/FamilyMembers';
    self.ReunionMembers = '/api/ReunionMembers';
    self.AssignTeams = self.Reunions + '/AssignTeams';
    self.siteUrl = "/";

    self.familyVacationCalendarId = 'tt59slhpv2nl6k8dch36u9cpj0@group.calendar.google.com';

    // Route operations

    // Other private operations

    // Operations

    // Data
    self.returnUrl = self.siteUrl;

    // Data access operations
    self.setAccessToken = function (accessToken) {
        sessionStorage.setItem("accessToken", accessToken);
    };

    self.getAccessToken = function () {
        return sessionStorage.getItem("accessToken");
    };
}
