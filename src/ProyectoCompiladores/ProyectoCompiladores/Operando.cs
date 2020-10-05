using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCompiladores
{
    public class Operando
    {

        public List<Estado> EstadosOperando;

        public Operando(List<Estado> E)
        {
            EstadosOperando = E;
        }


        public Estado GetEstado(int Index)
        {
            return EstadosOperando[Index];
        }

        public void IncrementaIndicesEstados()
        {
            foreach (Estado c in EstadosOperando)
            {
                c.Index++;
                foreach (Transicion t in c.Transiciones)
                {
                    t.IdEstadoDestino++;
                }
            }
        }

        public void EliminaTransicionEstado(int IndexEstado, int IndexEstadoDestino)
        {
            IndexEstado--;
            Estado e = EstadosOperando.ElementAt(IndexEstado);
            for (int i = 0; i < e.Transiciones.Count; i++)
            {
                if (e.Transiciones[i].IdEstadoDestino == IndexEstadoDestino)
                {
                    e.Transiciones.RemoveAt(i);
                    break;
                }
            }
        }
        public void DecrementaIndicesEstados()
        {
            foreach (Estado c in EstadosOperando)
            {
                c.Index--;
                foreach (Transicion t in c.Transiciones)
                {
                    t.IdEstadoDestino--;
                }
            }
        }

        public char ObtenSimboloTransicion(int IndexEstado, int IndexEstadoDestino)
        {
            IndexEstado--;
            //MessageBox.Show("Buscando transicion de: " + IndexEstado + " hacia: " + IndexEstadoDestino);
            Estado e = EstadosOperando.ElementAt(IndexEstado);
            for (int i = 0; i < e.Transiciones.Count; i++)
            {
                if ((e.Transiciones[i].IdEstadoDestino) == IndexEstadoDestino)
                {
                    //MessageBox.Show("Encontré la transición de: " + (IndexEstado + 1) + " hacia: " + (IndexEstadoDestino + 1) + " con el símbolo: " + e.Transiciones[i].Simbolo);
                    return e.Transiciones[i].Simbolo;
                }
            }
            return '/';
        }
        public int CuentaEstados()
        {
            return EstadosOperando.Count;
        }
    }
}
