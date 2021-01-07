﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ProyectoCompiladores
{
    public class AFDL
    {
        public List<EstadoAFDL> Estados;

        Dictionary<string, string> G;
        public List<string> T = new List<string>();
        public List<string> NT = new List<string>();
        public List<string> SimbolosGramaticales = new List<string>();
        public string[,] Ir_A;
        public string[,] Accion;
        int ContadorEstado = 0;
        string PAumentada;
        public AFDL(Dictionary<string, string> Gramatica, List<string> Terminales, List<string> NoTerminales, string GramaticaAumentada)
        {
            this.G = Gramatica;
            this.T = Terminales;
            this.NT = NoTerminales;

            PAumentada = GramaticaAumentada;

            foreach (string c in T)
            {
                SimbolosGramaticales.Add(c);
            }

            foreach (string c in NT)
            {
                SimbolosGramaticales.Add(c);
            }
            init();
            Ir_A = new string[Estados.Count, NT.Count];
            Accion = new string[Estados.Count, T.Count + 1];

            for(int i = 0; i < Estados.Count; i++)
            {
                for(int j = 0; j < NT.Count; j++)
                {
                    Ir_A[i, j] = "";
                }
                for(int z = 0; z < T.Count+1; z++)
                {
                    Accion[i, z] = "";
                }
            }

            
        }

        public List<string> getAllTransiciones()
        {
            List<string> res = new List<string>();

            foreach (string s in this.NT)
            {
                res.Add(s);
            }
            foreach (string s in this.T)
            {
                res.Add(s);
            }

            return res;
        }


        public void init()
        {
            Estados = new List<EstadoAFDL>();
            Elemento NuevoElemento = new Elemento(PAumentada, "programa'");
            List<Elemento> Inicial = new List<Elemento>
            {
                NuevoElemento
            };
            EstadoAFDL NuevoEstado = new EstadoAFDL(Cerradura(Inicial), ContadorEstado);
            Estados.Add(NuevoEstado);
            /*Estados.Add(new EstadoAFDL(Cerradura(new List<string>
            {
                PAumentada
            }
            ), ContadorEstado));*/
            //List<string> EstadoInicial = Cerradura()
            bool Bandera = true;
            //while (Bandera)
            //{
            //int Contador = Estados.Count;

            for (int i = 0; i < Estados.Count; i++)
            {
                /*foreach (string c in T)
                {
                    List<string> Ir_A = ir_A(i, c);

                    if (Ir_A.Count != 0 && ChecaNuevoEstado(Ir_A) == -1)
                    {
                        ContadorEstado++;
                        EstadoAFDL Nuevo = new EstadoAFDL(Ir_A, ContadorEstado);
                        Estados.Add(Nuevo);
                        Estados[i].AgregaTransicion(c, Nuevo.IndiceEstado);
                    }
                    else if (Ir_A.Count != 0 && ChecaNuevoEstado(Ir_A) != -1)
                    {
                        int indiceestado = ChecaNuevoEstado(Ir_A);
                        Estados[i].AgregaTransicion(c, Estados[indiceestado].IndiceEstado);
                    }

                }*/
                foreach (string c in SimbolosGramaticales)
                {
                    List<Elemento> Ir_A = ir_A(i, c);


                    if (Ir_A.Count != 0 && ChecaNuevoEstado(Ir_A) == -1)
                    {

                        ContadorEstado++;
                        EstadoAFDL Nuevo = new EstadoAFDL(Ir_A, ContadorEstado);
                        Estados.Add(Nuevo);
                        Estados[i].AgregaTransicion(c, Nuevo.IndiceEstado);
                    }
                    else if (Ir_A.Count != 0 && ChecaNuevoEstado(Ir_A) != -1)
                    {
                        int indiceestado = ChecaNuevoEstado(Ir_A);
                        Estados[i].AgregaTransicion(c, Estados[indiceestado].IndiceEstado);
                    }
                }
                //if (Contador == Estados.Count)
                //{
                //   Bandera = false;
                //    break;
                //}
                //}
            }


            string CadenaMostrar = "";
            foreach (EstadoAFDL E in Estados)
            {
                CadenaMostrar += "\n\nEstado:         " + E.IndiceEstado + ":";
                /*foreach(string s in E.ElementosEstado)
                {
                    CadenaMostrar += s+ ",";
                }*/
                CadenaMostrar += "Num estados " + E.ElementosEstado.Count;
                CadenaMostrar += "\n";
            }


            //MessageBox.Show(CadenaMostrar);
        }


        public void LlenaTablaAnalisisSintactico()
        {

        }
        public int ChecaNuevoEstado(List<Elemento> Candidato)
        {
            bool Nuevo = true;
            for (int i = 0; i < Estados.Count; i++)
            {
                if (Estados[i].EsIgual(Candidato))
                {
                    return i;
                }
            }
            return -1;
        }
        public List<Elemento> Cerradura(List<Elemento> ElementosEvaluar)//List<string> ElementosEvaluar)
        {
            bool Bandera = true;
            List<Elemento> J = new List<Elemento>();
            int NumeroElementos = -1;
            foreach (Elemento e in ElementosEvaluar)
            {
                J.Add(e);
            }
            /*foreach (string c in ElementosEvaluar)
            {
                J.Add(c);
            }*/
            for (int ii = 0; ii < J.Count; ii++)
            {
                string[] CadenaSplit = J[ii].CuerpoProduccion.Split(' ');
                int indexPunto = -1;
                for (int i = 0; i < CadenaSplit.Length; i++)
                {
                    if (CadenaSplit[i] == ".")
                    {
                        indexPunto = i;
                        break;
                    }
                }
                if (indexPunto != -1 && indexPunto != CadenaSplit.Length - 1)
                {
                    string CadenaEvaluar = CadenaSplit[indexPunto + 1];
                    if (NT.Contains(CadenaEvaluar))
                    {
                        List<Elemento> Producciones = DevuelveProducciones(CadenaEvaluar);
                        foreach (Elemento e in Producciones)
                        {
                            string P = e.CuerpoProduccion;
                            string Aux = ". " + P;
                            Aux.TrimEnd();
                            if (!Contiene(J, Aux))
                            {
                                Elemento NuevoElemento = new Elemento(Aux, e.EncabezadoProduccion);
                                J.Add(NuevoElemento);
                            }
                        }
                        /*
                        foreach (string P in Producciones)
                        {
                            string Aux = ". " + P;
                            if (!Contiene(J, Aux))
                            {
                                Elemento NuevoElemento = 
                            }
                            /*if (!J.Contains(Aux))
                            {
                                J.Add(Aux);
                            }
                        }*/

                    }
                }
            }
            //}
            return J;
        }

        public bool Contiene(List<Elemento> Elementos, string Cadena)
        {
            foreach (Elemento e in Elementos)
            {
                if (e.CuerpoProduccion == Cadena)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Elemento> DevuelveProducciones(string Encabezado)
        {
            string[] Split = G[Encabezado].Split('|');
            List<Elemento> Resultado = new List<Elemento>();
            foreach (string c in Split)
            {
                Elemento NuevoElemento = new Elemento(c.TrimEnd(' '), Encabezado);
                Resultado.Add(NuevoElemento);
            }
            return Resultado;
        }

        public int DevuelveNumeroProduccion(string Produccion)
        {

            return -1;
        }
        public List<Elemento> ir_A(int indiceEstado, string Simbolo)
        {
            // [A --> xB],[A ---> xC ],[A --> xD],
            EstadoAFDL Seleccionado = Estados[indiceEstado];
            List<Elemento> ProduccionesCambiadas = Seleccionado.DevuelveCadenas(Simbolo);
            List<Elemento> Resultado = new List<Elemento>();
            if (ProduccionesCambiadas.Count > 0)
            {
                Resultado = Cerradura(ProduccionesCambiadas);
            }
            return Resultado;
        }

        private String getTerminalDespuesDelPunto(String elemento, int indice)
        {
            foreach (String s in T)
            {
                int index = elemento.IndexOf(s, indice);

                if (elemento.IndexOf(s, indice) == indice + 2)
                {
                    return s;
                }
            }
            return null;
        }


        public void generaTablaDeAnalisisLR0(Dictionary<string, string> diccionarioParaB)
        {
            foreach (EstadoAFDL estado in this.Estados)
            {
                foreach (Elemento elemento in estado.ElementosEstado)
                {
                    string Aux = elemento.EncabezadoProduccion.TrimEnd(' ');
                    elemento.EncabezadoProduccion = Aux;
                    if (elemento.CuerpoProduccion.IndexOf(".") != elemento.CuerpoProduccion.Length - 1)//a)
                    {//se hacen los dirige
                        int Punto = elemento.CuerpoProduccion.IndexOf(".");
                        int Longitud = elemento.CuerpoProduccion.Length;
                        string aux = getTerminalDespuesDelPunto(elemento.CuerpoProduccion, elemento.CuerpoProduccion.IndexOf("."));
                        if (aux != null)
                        {
                            int indiceIrA = -1;
                            foreach (TransicionD t in estado.Transiciones)
                            {
                                if (t.S == aux)
                                {
                                    indiceIrA = t.indiceDest;
                                }
                            }
                            if (indiceIrA != -1)
                            {
                                int indiceSimbolo = T.IndexOf(aux);
                                Accion[estado.IndiceEstado, T.IndexOf(aux)] = "d" + indiceIrA.ToString();
                            }
                        }
                    }

                    if (!elemento.EncabezadoProduccion.Contains("'") && elemento.CuerpoProduccion.IndexOf(".") == elemento.CuerpoProduccion.Length - 1)//b)
                    {// si el punto esta al ultimo se hacen los reducir
                        String[] siguientesCadena = diccionarioParaB[elemento.EncabezadoProduccion].Split(' ');
                        int indicePunto = elemento.CuerpoProduccion.IndexOf(".");
                        String elementoSinPuntoAux = elemento.CuerpoProduccion.Remove(elemento.CuerpoProduccion.IndexOf(".")).TrimEnd(' ');

                        int indiceProd = getIndiceProduccion(elementoSinPuntoAux);
                        foreach (string s in siguientesCadena)
                        {
                            if (s == "$")
                            {
                                Accion[estado.IndiceEstado, T.Count] = "r" + indiceProd.ToString();
                            }
                            else
                            {
                                Accion[estado.IndiceEstado, T.IndexOf(s)] = "r" + indiceProd.ToString();
                            }

                        }
                    }
                    if (elemento.EncabezadoProduccion.Contains("'") && elemento.CuerpoProduccion.IndexOf(".") == elemento.CuerpoProduccion.Length - 1)//c)
                    {//estado de aceptacion [eAFDG.IndiceEstado,$] = ac

                        Accion[estado.IndiceEstado, T.Count()] = "ac";
                    }
                }
                foreach (TransicionD tD in estado.Transiciones)
                {
                    if (NT.Contains(tD.S))
                    {
                        Ir_A[estado.IndiceEstado, NT.IndexOf(tD.S)] = tD.indiceDest.ToString();
                    }
                }
            }

            /*string CadenaMessage = "";

            for(int i = 0; i < Estados.Count; i++)
            {
                for(int j = 0; j < NT.Count; j++)
                {
                    if(Ir_A[i,j] != null)
                    {
                        CadenaMessage += Ir_A[i, j] + " ";

                    }
                    else
                    {
                        CadenaMessage += "ø ";
                    }
                }
                CadenaMessage += "\n";
            }
            MessageBox.Show("Ir_A: \n" + CadenaMessage);

            CadenaMessage = "";

            for (int i = 0; i < Estados.Count; i++)
            {
                for (int j = 0; j < T.Count + 1; j++)
                {
                    if (Accion[i, j] != null)
                    {
                        CadenaMessage += Accion[i, j] + " ";

                    }
                    else
                    {
                        CadenaMessage += "ø ";
                    }
                }
                CadenaMessage += "\n";
            }
            MessageBox.Show("Accion: \n" + CadenaMessage);
            */
        }

        private int getIndiceProduccion(String produccionBus)
        {
            int aux = 0;

            foreach (KeyValuePair<string, string> EntradaD in G)
            {
                string[] ArregloCadenas = EntradaD.Value.Split('|');
                foreach (string c in ArregloCadenas)
                {
                    aux++;

                    // MessageBox.Show("Producción:\n " + EntradaD.Key + " ----> " + Aux);
                    if (produccionBus == c)
                    {
                        return aux;
                    }
                }
            }
            MessageBox.Show("No encontré esta producción: " + produccionBus);

            return 0;
        }

        public int GetCaracteresProduccion(int IndiceProduccion)
        {
            int Caracteres = -1;
            int aux = 0;
            foreach (KeyValuePair<string, string> EntradaD in G)
            {
                string[] ArregloCadenas = EntradaD.Value.Split('|');
                foreach (string c in ArregloCadenas)
                {
                    aux++;

                    // MessageBox.Show("Producción:\n " + EntradaD.Key + " ----> " + Aux);
                    if (aux == IndiceProduccion)
                    {
                        string []CadenaProduccion = c.Split();
                        return CadenaProduccion.Length;
                    }
                }
            }
            return Caracteres;
        }

        public string[] getTokensProduccion(int IndiceProduccion)
        {
            int aux = 0;
            foreach (KeyValuePair<string, string> EntradaD in G)
            {
                string[] ArregloCadenas = EntradaD.Value.Split('|');
                foreach (string c in ArregloCadenas)
                {
                    aux++;

                    // MessageBox.Show("Producción:\n " + EntradaD.Key + " ----> " + Aux);
                    if (aux == IndiceProduccion)
                    {
                        string[] CadenaProduccion = c.Split();
                        return CadenaProduccion;
                    }
                }
            }
            return null;
        }

        public string ObtenPadreProduccion(int IndiceProduccion)
        {
            string res = "";
            int aux = 0;
            foreach (KeyValuePair<string, string> EntradaD in G)
            {
                string[] ArregloCadenas = EntradaD.Value.Split('|');
                foreach (string c in ArregloCadenas)
                {
                    aux++;

                    // MessageBox.Show("Producción:\n " + EntradaD.Key + " ----> " + Aux);
                    if (aux == IndiceProduccion)
                    {
                        return EntradaD.Key;
                    }
                }
            }
            return res;
        }



















    }
}
