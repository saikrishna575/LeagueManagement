using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LMEntities.Models
{
    public partial class Schedule : Entity
    {
        public Schedule()
        {
            this.MatchResults = new List<MatchResult>();
        }

        public int Id { get; set; }
        [DisplayName("Season Name")]
        public int SeasonId { get; set; }
        [DisplayName("Year")]
        public int YearId { get; set; }
        [DisplayName("Home Team")]
        public int HomeTeamId { get; set; }
        [DisplayName("Visitor Team")]
        public int VisitorTeamId { get; set; }

        [DisplayName("Umpire Name")]
        public Nullable<int> UmpireId { get; set; }

        [DisplayName("Ground Name")]
        public Nullable<int> GroundId { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModfiedBy { get; set; }
        public virtual ICollection<MatchResult> MatchResults { get; set; }
        public virtual Season Season { get; set; }
        public virtual Year Year { get; set; }

        public virtual Ground Ground { get; set; }


        public virtual Team Team { get; set; }
        public virtual Team Team1 { get; set; }

    }
}
