using Business.IRepositorios;
using Data.Repositorios;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Catalogos : ICatalogos
    {
        private readonly ICatalogosRepositorio _catalogosRepositorio;
        public Catalogos(ICatalogosRepositorio catalogosRepositorio) 
        { 
            _catalogosRepositorio= catalogosRepositorio;
        }
        public Task<List<Roles>> ObtieneRoles()
        {
            return _catalogosRepositorio.ObtieneRoles();
        }
    }
}
