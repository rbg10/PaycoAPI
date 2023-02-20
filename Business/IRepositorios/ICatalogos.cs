using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepositorios
{
    public interface ICatalogos
    {
        Task<List<Roles>> ObtieneRoles();

    }
}
