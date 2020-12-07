using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class Elemento
    {
        public string CuerpoProduccion;
        public string EncabezadoProduccion;


        public Elemento(string C, string E)
        {
            this.CuerpoProduccion = C;
            this.EncabezadoProduccion = E;
        }
    }
}
