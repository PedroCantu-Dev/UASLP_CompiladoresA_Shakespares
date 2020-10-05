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
            //MessageBox.Show("Estoy borrando una transicion desde: "  + IndexEstado + " hasta: " + IndexEstadoDestino);
            Estado ex = new Estado(-1, -1);
            foreach (Estado e in EstadosOperando)
            {
                if(e.Index== IndexEstado)
                {
                    ex = e;
                }
            }
            
            if(ex.Index != -1)
            {
                
                for (int i = 0; i < ex.Transiciones.Count; i++)
                {
                    if (ex.Transiciones[i].IdEstadoDestino == IndexEstadoDestino)
                    {
                        
                        ex.Transiciones.RemoveAt(i);
                        break;
                    }
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
            //MessageBox.Show("Buscando transicion de: " + IndexEstado + " hacia: " + IndexEstadoDestino);
            Estado EstadoInicio = new Estado(-1,-1); 
            foreach(Estado e in EstadosOperando)
            {
                if (e.Index == IndexEstado)
                {
                    EstadoInicio = e;
                }
            }
            if(EstadoInicio.Index != -1)
            {
                for (int i = 0; i < EstadoInicio.Transiciones.Count; i++)
                {
                    if ((EstadoInicio.Transiciones[i].IdEstadoDestino) == IndexEstadoDestino)
                    {
                        //MessageBox.Show("Encontré la transición de: " + (IndexEstado + 1) + " hacia: " + (IndexEstadoDestino + 1) + " con el símbolo: " + e.Transiciones[i].Simbolo);
                        return EstadoInicio.Transiciones[i].Simbolo;
                    }
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
