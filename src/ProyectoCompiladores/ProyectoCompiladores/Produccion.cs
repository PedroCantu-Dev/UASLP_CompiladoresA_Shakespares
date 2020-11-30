using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    class Produccion
    {
        string Encabezado;
        List<string> CuerpoProduccion;


        public Produccion(string Encabezado, List<string> Cuerpo)
        {
            this.Encabezado = Encabezado;
            this.CuerpoProduccion = Cuerpo;
        }
    }
}
