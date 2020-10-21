using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class AFD
    {
        Destados destados;
        AFN afn;
        public AFD(AFN afn)
        {
            destados = new Destados();
            this.afn = afn;
        }

        public void init()
        {
            destados.Add(cerraduraEpsilon(afn.Estados[0]));

            for(int i = 0; i < this.destados.Count(); i++)
            {
                foreach (char a in afn.alfabeto)
                {
                    Destado U = cerraduraEpsilon(mover(this.destados.ElementAt(i),a));
                    if(!destados.Contains(U))
                    {
                        destados.Add(U);
                    }
                }
            }

        }

        private List<Estado> mover(Destado T, char transicion)//se lleva a cabo con el AFN
        {
            List<Estado> resultado = new List<Estado>();

        }

        private Destado cerraduraEpsilon(Estado inicial)
        {
            Destado res;

        }
        private Destado cerraduraEpsilon(List<Estado> listaEstados)
        {
            Destado res;

        }
        private Destado cerraduraEpsilonRecursivo(List<Estado> listaEstados)
        {
            
        }
    }
}
