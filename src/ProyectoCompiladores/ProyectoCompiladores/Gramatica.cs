using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{

   

    public class Gramatica
    {
        List<String> terminales;
        List<String> NoTerminales


        public Gramatica(List<String> terminales, List<String> NoTerminales)
        {
            this.terminales = terminales;
            this.NoTerminales = NoTerminales;
        }
    }

}
