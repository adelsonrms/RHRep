using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RH.Domain.Entities;
using RH.Domain.Repositories;

namespace ERP.RH.Application
{
    internal interface IEntityApplication<T>  where T : class
    {
        IEnumerable<T> ObterTodos();
    }
}
