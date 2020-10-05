using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class Transicion
    {
        public char Simbolo { get; set; }

        public int IdEstadoDestino { get; set; }
        public Transicion(char S, int id)
        {
            IdEstadoDestino = id;
            Simbolo = S;
        }
    }
}
