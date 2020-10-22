using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class Destados
    {
        public List<Destado> Lista;

        public Destados()
        {
            Lista = new List<Destado>();
        }

       public Boolean Contains(Destado destadoEntrante)
       {
            foreach(Destado de in this.Lista)
            {
                if(de.Equals(destadoEntrante))
                {
                    return true;
                }
            }
            return false;
       }

        public void Add(Destado destado)
        {
            this.Lista.Add(destado);
        }

        public int Count()
        {
            return this.Lista.Count();
        }

        public Destado ElementAt(int i)
        {
            return this.Lista.ElementAt(i);
        }

       /* public Boolean Exist(List<Estado> lista)
        {
            foreach(Destado d in Lista)
            {
                if(d.Equals(lista))
                {
                    return true;
                }
            }
            return false;
        }*/

        public Destado Exist(List<Estado> lista)
        {
            foreach (Destado d in Lista)
            {
                if (d.Equals(lista))
                {
                    return d;
                }
            }
            return null;
        }

    }
}
