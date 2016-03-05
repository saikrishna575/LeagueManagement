using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMUnitofWork
{
    interface IUnitofWork : IDisposable
    {
        int Complete();
    }
}
