using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCompiladores
{
    public class AFD
    {
        
        public  Destados destados;
        AFN afn;
        public String alfabetoAFD;

        public AFD(AFN afn)
        {
            destados = new Destados();
            this.afn = afn;
            this.alfabetoAFD = afn.alfabeto.Trim('ε');
        }

        public void init()
        {
            Destado toAdd = cerraduraEpsilon(afn.Estados[0]);
            destados.Add(toAdd);

            for(int i = 0; i < this.destados.Count(); i++)
            {
                Destado dest = this.destados.ElementAt(i);
                if (!dest.marcado)
                {
                    dest.marcar();

                    foreach (char a in alfabetoAFD)
                    {
                        List<Estado> moverRes = mover(this.destados.ElementAt(i),a);
                        if (moverRes.Count()>0)
                        {
                            Destado U = cerraduraEpsilon(moverRes);

                            if (!destados.Contains(U))
                            {
                                destados.Add(U);
                            }
                            if (!dest.ExistTransicion(U.indice))
                            {
                                this.destados.ElementAt(i).AddTransicion(U, a);
                            }
                        }
                        
                    }
                }
            }
        }


        public bool ValidaLexema(string Lexema)
        {
            //MessageBox.Show("Validando el lexema");

            bool bandera = true;
            int indiceCaracter = 0;
            int indiceDEstado = 0;
            while(bandera == true && indiceCaracter < Lexema.Length)
            {
                int Aux = destados.Lista[indiceDEstado].ExisteTransicionSimbolo(Lexema[indiceCaracter]);
                if (Aux != -1)
                {
                    indiceDEstado = Aux;
                    indiceCaracter++;
                }
                else
                {
                    return false;
                }
            }

            if(destados.Lista[indiceDEstado].tipo == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Estado> mover(Destado T, char transicion)//se lleva a cabo con el AFN
        {
            List<Estado> resultado = new List<Estado>();
            foreach(Estado e in afn.Estados)
            {
                if (T.Contains(e.Index))//si el Destado contiene el indice del estado lo checamos
                {
                    foreach (Transicion t in e.Transiciones)
                    {
                        if(t.Simbolo == transicion)
                        {
                            resultado.Add(afn.getEstadoByIndex(t.IdEstadoDestino));
                        }
                    }
                }
            }
            return resultado;
        }

        List<Estado> listaAuxiliarCeEpsilon;

        private Destado cerraduraEpsilon(Estado inicial)
        {
            listaAuxiliarCeEpsilon = new List<Estado>();
            listaAuxiliarCeEpsilon.Add(inicial);
            List<Estado> listaInterna;

                listaInterna = new List<Estado>();
                foreach (Transicion t in inicial.Transiciones)
                {
                    if (t.Simbolo == 'ε')
                    {
                        Estado estadoDestino = afn.getEstadoByIndex(t.IdEstadoDestino);
                        if (!listaAuxiliarCeEpsilon.Contains(estadoDestino))
                        {//si no contiene el estado lo agrega a la lista de estados del Dstado.
                            listaAuxiliarCeEpsilon.Add(estadoDestino);
                            listaInterna.Add(estadoDestino);
                        }
                    }
                }
                cerraduraEpsilonRecursivo(listaInterna);

            Destado dAux = destados.Exist(listaAuxiliarCeEpsilon);
            if (dAux != null)
            {
                return dAux;
            }
            else
            {
                return new Destado(listaAuxiliarCeEpsilon, asciiCounter++);
            }
            
        }
        
        private Destado cerraduraEpsilon(List<Estado> listaEstados)
        {
            listaAuxiliarCeEpsilon = new List<Estado>();
            List<Estado> listaInterna;

            foreach (Estado e in listaEstados)
            {
                if (!listaAuxiliarCeEpsilon.Contains(e))
                {//si no contiene el estado lo agrega a la lista de estados del Dstado.
                    listaAuxiliarCeEpsilon.Add(e);
                }
                    listaInterna = new List<Estado>();
                foreach (Transicion t in e.Transiciones)
                {
                    if(t.Simbolo == 'ε' )
                    {
                        Estado estadoDestino = afn.getEstadoByIndex(t.IdEstadoDestino);
                        if (!listaAuxiliarCeEpsilon.Contains(estadoDestino))
                        {//si no contiene el estado lo agrega a la lista de estados del Dstado.
                            listaAuxiliarCeEpsilon.Add(estadoDestino);
                            listaInterna.Add(estadoDestino);
                        }
                    }
                }
                cerraduraEpsilonRecursivo(listaInterna);
            }

            Destado dAux = destados.Exist(listaAuxiliarCeEpsilon);
            if (dAux != null)
            {
                return dAux;
            }
            else
            {
                return new Destado(listaAuxiliarCeEpsilon, asciiCounter++);
            }
        }

        private void cerraduraEpsilonRecursivo(List<Estado> listaEstados)
        {
            List<Estado> listaInterna;

            foreach (Estado e in listaEstados)
            {
                listaInterna = new List<Estado>();
                foreach (Transicion t in e.Transiciones)
                {
                    if (!listaAuxiliarCeEpsilon.Contains(e))
                    {//si no contiene el estado lo agrega a la lista de estados del Dstado.
                        listaAuxiliarCeEpsilon.Add(e);
                    }
                    if (t.Simbolo == 'ε')
                    {
                        Estado estadoDestino = afn.getEstadoByIndex(t.IdEstadoDestino);
                        if (!listaAuxiliarCeEpsilon.Contains(estadoDestino))
                        {//si no contiene el estado lo agrega a la lista de estados del Dstado.
                            listaAuxiliarCeEpsilon.Add(estadoDestino);
                            listaInterna.Add(estadoDestino);
                        }
                    }
                }
                cerraduraEpsilonRecursivo(listaInterna);
            }
        }


        private List<Estado> Clone(List<Estado> original)
        {
            List<Estado> res = new List<Estado>();
            foreach (Estado e in original)
            {
                res.Add(e);
            }
            return res;
        }


        //65 a 90
        int asciiCounter = 0;

        private String getNombreDestado()
        {
            String res = "";
            int counterAux = asciiCounter;

            int numCaracteres = asciiCounter / 25;

            for (int i = 0; i < numCaracteres; i++)
            {
                if(counterAux > 0)
                {
                    int numModAux = counterAux % 25;
                    if(numModAux == 0)
                    {
                        res += Convert.ToChar(25 + 65);
                    }
                    else
                    {
                        res += Convert.ToChar(65+numModAux);
                    }
                    counterAux-=numModAux;
                }                
            }
            res.Reverse();
            asciiCounter++;
            return res;
        }



    }
}
