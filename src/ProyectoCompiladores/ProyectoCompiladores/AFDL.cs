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
        List<string> T = new List<string>();
        List<string> NT = new List<string>();
        List<string> SimbolosGramaticales = new List<string>();

        int ContadorEstado = 1;
        string PAumentada;
        public AFDL(Dictionary<string, string> Gramatica, List<string> Terminales, List<string> NoTerminales, string GramaticaAumentada)
        {
            this.G = Gramatica;
            this.T = Terminales;
            this.NT = NoTerminales;

            PAumentada = GramaticaAumentada;

            foreach (string c in NT)
            {
                SimbolosGramaticales.Add(c);
            }

            foreach (string c in T)
            {
                SimbolosGramaticales.Add(c);
            }
            init();
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

            Estados.Add(new EstadoAFDL(Cerradura(new List<string>
            {
                PAumentada
            }
            ), ContadorEstado));
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



        public int ChecaNuevoEstado(List<string> Candidato)
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
        public List<string> Cerradura(List<string> ElementosEvaluar)
        {
            bool Bandera = true;
            List<string> J = new List<string>();
            int NumeroElementos = -1;
            foreach (string c in ElementosEvaluar)
            {
                J.Add(c);
            }
            for (int ii = 0; ii < J.Count; ii++)
            {
                string[] CadenaSplit = J[ii].Split(' ');
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
                        List<string> Producciones = DevuelveProducciones(CadenaEvaluar);
                        foreach (string P in Producciones)
                        {
                            string Aux = ". " + P;
                            if (!J.Contains(Aux))
                            {
                                J.Add(Aux);
                            }
                        }

                    }
                }
            }
            //}
            return J;
        }


        public List<string> DevuelveProducciones(string Encabezado)
        {
            string[] Split = G[Encabezado].Split('|');
            List<string> Resultado = new List<string>();
            foreach (string c in Split)
            {
                Resultado.Add(c.TrimEnd());
            }
            return Resultado;
        }

        public List<string> ir_A(int indiceEstado, string Simbolo)
        {
            // [A --> xB],[A ---> xC ],[A --> xD],
            EstadoAFDL Seleccionado = Estados[indiceEstado];
            List<string> ProduccionesCambiadas = Seleccionado.DevuelveCadenas(Simbolo);
            List<string> Resultado = new List<string>();
            if (ProduccionesCambiadas.Count > 0)
            {
                Resultado = Cerradura(ProduccionesCambiadas);
            }
            return Resultado;
        }
    }
}