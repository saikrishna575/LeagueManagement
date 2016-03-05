using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LMEntities;

namespace LMRepository
{
    public class LeagueGrpRepository : Repository<Season>, ILeagueGroupRepository
    {
        public LeagueGrpRepository(SportsSiteEntities context) : base(context)
        {

        }        
           }
}
    