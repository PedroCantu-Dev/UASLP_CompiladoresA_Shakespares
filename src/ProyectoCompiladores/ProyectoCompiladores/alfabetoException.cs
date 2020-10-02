using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class alfabetoException : Exception
    {

        public alfabetoException()
        {

        }

        public alfabetoException(String mensaje) : base(mensaje) { }
    }
}
