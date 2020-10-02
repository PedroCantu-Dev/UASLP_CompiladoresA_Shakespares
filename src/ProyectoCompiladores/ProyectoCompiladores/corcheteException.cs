using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
   public class corcheteException : Exception
    {
        public corcheteException()
        {

        }

        public corcheteException(String mensaje) : base(mensaje){ }
        
    }
}
