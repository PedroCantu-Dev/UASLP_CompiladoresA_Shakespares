using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
   public  class TransicionD
    {
        public char Simbolo { get; set; }

        public String NombreDeEstadoDestino { get; set; }

        public TransicionD(char transicion, String nombreDeEstadoDestino)//es una transicion para AFD
        {
            Simbolo = transicion;
            NombreDeEstadoDestino = nombreDeEstadoDestino;
        }
    }
}
