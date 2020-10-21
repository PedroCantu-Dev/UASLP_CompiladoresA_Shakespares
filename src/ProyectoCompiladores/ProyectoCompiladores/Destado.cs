using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class Destado
    {
        public List<Estado> listaEstadosEnAFN;
        public Boolean marcado;
        public List<TransicionD> listaTransiciones;

        public Destado()
        {
            listaEstadosEnAFN = new List<Estado>();
            marcado = false;
        }

        public Boolean Equals(Destado comparacion)
        {
            List<int> listaEstadosIndexThis = this.listaEstadosEnAFN_Index();

            foreach (int index in comparacion.listaEstadosEnAFN_Index())
            {
                if (!listaEstadosIndexThis.Contains(index))
                {
                    return false;
                }
            }
            return true;
        }

        public List<int> listaEstadosEnAFN_Index()
        {
            List<int> res = new List<int>();
            foreach (Estado e in this.listaEstadosEnAFN)
            {
                res.Add(e.Index);
            }
            return res;
        }
    }
}
