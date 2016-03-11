using System;
using System.Collections.Generic;

namespace LMEntities.Models
{
    public partial class SkillSpeciality :Repository.Pattern.Ef6.Entity
    {
        public SkillSpeciality()
        {
            this.TeamMembers = new List<TeamMember>();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
