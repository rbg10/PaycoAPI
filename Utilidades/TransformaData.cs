using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class TransformaData
    {
        public DataTable ConvierteRolesTable(List<Roles> lista)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Rol_ID", typeof(Guid));
            tabla.Columns.Add("Nombre", typeof(string));
            foreach (Roles role in lista) {
                DataRow row = tabla.NewRow();
                row["Rol_ID"] = role.Rol_ID;
                row["Nombre"] = role.Nombre;

                tabla.Rows.Add(row);
            }

            tabla.AcceptChanges();

            return tabla;
        }

        public DataTable ConvierteClientesTable(List<Cliente> lista)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Cliente_ID", typeof(Guid));
            tabla.Columns.Add("Nombre", typeof(string));
            foreach (Cliente cliente in lista)
            {
                DataRow row = tabla.NewRow();
                row["Cliente_ID"] = cliente.Cliente_ID;
                row["Nombre"] = cliente.Nombre;

                tabla.Rows.Add(row);
            }

            tabla.AcceptChanges();

            return tabla;
        }
    }
}
