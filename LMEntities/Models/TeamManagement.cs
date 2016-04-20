using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LMEntities.Models
{
    public partial class TeamManagement : Entity
    {
        public int Id { get; set; }
        [DisplayName("Organization Name")]
        public int OrganizationId { get; set; }
        [DisplayName("Team")]
        public int TeamId { get; set; }
        [DisplayName("Title")]
        public int TitleId { get; set; }
        [DisplayName("Team Member")]
        public int TeamMemberId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public virtual Team Team { get; set; }
        public virtual TeamMember TeamMember { get; set; }
        public virtual TeamTitle TeamTitle { get; set; }
    }
}
