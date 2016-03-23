using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMEntities.Models
{
    public partial class AspNetUserRoles : Entity
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string RoleId { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetRole AspNetRole { get; set; }

    }
}
