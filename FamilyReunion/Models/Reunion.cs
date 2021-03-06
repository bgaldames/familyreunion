﻿using System;
using System.Collections.Generic;

namespace FamilyReunion.Models
{
    public class Reunion
    {
        public Reunion()
        {
            Teams = new List<Team>();
            ReunionMembers = new List<ReunionMember>();
        }
        public int ReunionId { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string GoogleFormId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<Team> Teams { get; set; }
        public ICollection<ReunionMember> ReunionMembers { get; set; }
    }
}
