using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class Destado
    {
        public List<Estado> listaEstadosEnAFN;
        public Boolean marcado;
        public List<TransicionD> listaTransiciones = new List<TransicionD>();
        public String Nombre;
        public int indice;
        public bool tipo; // Inicial o Final (false || true)
        public Destado()
        {
            listaEstadosEnAFN = new List<Estado>();
            marcado = false;
        }

        public Destado(List<Estado> listaEstadosEnAFN, String Nombre, bool tipo)
        {
            this.listaEstadosEnAFN = listaEstadosEnAFN;
            marcado = false;
            this.Nombre = Nombre;
        }

        public Destado(List<Estado> listaEstadosEnAFN, int index)
        {
            this.listaEstadosEnAFN = listaEstadosEnAFN;
            marcado = false;
            indice = index;
        }

        public void marcar()
        {
            marcado = true;
        }

        public void Add(Estado e)
        {
            this.listaEstadosEnAFN.Add(e);
        }

        public void AddTransicion(Destado d, char simbolo)
        {

            listaTransiciones.Add(new TransicionD(simbolo, d.indice));
        }

        public Boolean Equals(Destado comparacion)
        {
            List<int> listaEstadosIndexThis = this.listaEstadosEnAFN_Index();
            if (comparacion.listaEstadosEnAFN.Count() == this.listaEstadosEnAFN.Count())//|| comparacion.listaEstadosEnAFN.Count() == 0)
            {
                foreach (int index in comparacion.listaEstadosEnAFN_Index())
                {
                    if (!listaEstadosIndexThis.Contains(index))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean Equals(List<Estado> listaEstados)
        {
            if (listaEstados.Count() == this.listaEstadosEnAFN.Count())//|| listaEstados.Count()==0)
            {
                foreach (Estado e in listaEstados)
                {
                    if (!this.Contains(e.Index))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ExistTransicion(int IndiceTransicion)
        {
            foreach(TransicionD t in listaTransiciones)
            {
                if(t.indiceDest == IndiceTransicion)
                {
                    return true;
                }
            }
            return false;
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
        public Boolean Contains(int Index)
        {
            foreach (Estado e in this.listaEstadosEnAFN)
            {
                if (e.Index == Index)
                {
                    return true;
                }
            }
            return false;
        }
        public Boolean Contains(Estado estado)
        {
            foreach (Estado e in this.listaEstadosEnAFN)
            {
                if (e.Index == estado.Index)
                {
                    return true;
                }
            }
            return false;
        }

        private String getStringEstadoDestinoTransicion(char transicion)
        {
            foreach (TransicionD t in this.listaTransiciones)
            {
                if (t.Simbolo == transicion)
                {
                    return t.indiceDest.ToString();
                }
            }
            return "Ø";
        }

        public String[] getRowTransiciones(String alfabeto)
        {
            List<String> res = new List<string>();
            res.Add(this.indice.ToString());
            foreach (char c in alfabeto)
            {
                res.Add(getStringEstadoDestinoTransicion(c));
            }
            return res.ToArray();
        }

        public int ExisteTransicionSimbolo(char Simbolo)
        {
            int Indice = -1;
            for(int i = 0; i < listaTransiciones.Count; i++)
            {
                if(listaTransiciones[i].Simbolo == Simbolo)
                {
                    Indice = i;
                    return Indice;
                }
            }
            return Indice;
        }
    }
}
