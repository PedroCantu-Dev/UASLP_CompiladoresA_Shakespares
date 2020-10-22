using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCompiladores
{
    public class AFN
    {
        protected Estado inicial = null;
        protected Estado final = null;

        public List<Estado> estados;
        protected String op = "*+?&|";
        protected string OperadoresUnarios = "*+?";
        protected string OperadoresBinarios = "&|C";
        public string alfabeto = "";
        protected int ContadorEstados = 1;

        public List<Estado> Estados { get { return this.estados; } }

        public AFN(String expresionPosfija)
        {
            estados = new List<Estado>();
            alfabeto = encuentraAlfabeto(expresionPosfija) + "ε";
        }

        public Estado getEstadoByIndex(int Index)
        {
            foreach(Estado e in this.estados)
            {
                if(e.Index == Index)
                {
                    return e;
                }
            }
            return null;
        }

        //para determinar el alfabeto de la expresion
        public string encuentraAlfabeto(String expresionPosfija)
        {
            string res = "";
            foreach(char caracter in expresionPosfija)
            {
                if(!op.Contains(caracter) && !res.Contains(caracter))
                {
                    res += caracter;
                }
            }
            return res;
        }

        //este metodo creara el AFN mientras hace la evaluacion de la expresion posfija.
        public Operando algoritmoDeEvaluacion(String expresionPosfija)
        {
            Stack<Operando> PilaOperandos = new Stack<Operando>();
            foreach (char c in expresionPosfija)
            {
                Operando Resultado = new Operando(new List<Estado>());
                if (alfabeto.Contains(c))
                {
                    Operando NuevoOperando = AgregaCaracter(c);
                    PilaOperandos.Push(NuevoOperando);
                    //MessageBoxResultadoOperacion(NuevoOperando);
                }
                else
                {
                    if (OperadoresUnarios.Contains(c))
                    {
                        //MessageBox.Show("Estoy haciendo una operacion unaria");
                        Operando O = PilaOperandos.Pop();
                        switch (c)
                        {
                            case '*':
                                Resultado = addCerraduraDeKleen(O);
                                break;
                            case '+':
                                Resultado = addCerraduraPositiva(O);
                                break;
                            case '?':
                                Resultado = addCeroOUnaInstancia(O);
                                break;
                        }
                        //MessageBoxResultadoOperacion(Resultado);
                        PilaOperandos.Push(Resultado);
                    }
                    else if(OperadoresBinarios.Contains(c))
                    {
                        //MessageBox.Show("Estoy haciendo una operacion binaria");
                        Operando Operando2 = PilaOperandos.Pop();
                        Operando Operando1 = PilaOperandos.Pop();
                        /*MessageBox.Show("Operando 1 tiene : " + Operando1.EstadosOperando.Count + "\n" +
                          "Operando 2 tiene: " + Operando2.EstadosOperando.Count);*/
                        switch (c)
                        {
                            case '&':
                                Resultado = addConcatenacion(Operando1, Operando2);
                                break;
                            case '|':
                                Resultado = addAlternativas(Operando1, Operando2);
                                break;
                        }
                        //MessageBoxResultadoOperacion(Resultado);
                        PilaOperandos.Push(Resultado);
                    }
                }

            }
            Operando R = PilaOperandos.Pop();
            return R;
        }

        public Operando AgregaCaracter(char C)
        {
            Estado EstadoInicial = new Estado(ContadorEstados,0);
            ContadorEstados++;
            Estado EstadoFinal = new Estado(ContadorEstados, 2);
            ContadorEstados++;
            EstadoInicial.addTransicion(C, EstadoFinal.Index);
            List<Estado> ListaEstados = new List<Estado>();
            ListaEstados.Add(EstadoInicial);
            ListaEstados.Add(EstadoFinal);
            Operando NuevoOperando = new Operando(ListaEstados);
            return NuevoOperando;
        }

        public void MessageBoxResultadoOperacion(Operando O)
        {
            string c = "";
            c += "La operación dió como resultado el AFN: \n";
            foreach( Estado e in O.EstadosOperando)
            {
                if(e.tipo != 2)
                {
                    c += "Estado: " + e.Index + "\n";
                    c += "\tTiene " + e.Transiciones.Count + " transiciones: \n\n" ;
                    foreach (Transicion t in e.Transiciones)
                    {
                        c += "\tTransición con simbolo " + t.Simbolo + " hacia: " + t.IdEstadoDestino + "\n";
                    }
                }
                else
                {
                    c += "Indice del estado de aceptacion: " + e.Index;
                }

                c += "\n";
            }
            MessageBox.Show(c);
        }

        public Operando addConcatenacion(Operando Op1, Operando Op2)
        {
            //MessageBox.Show("Agregando concatenacion desde: " + +Op1.EstadosOperando[0].Index + " hasta : " + Op2.EstadosOperando[0].Index);
            Operando NuevoOperando = new Operando(new List<Estado>()); // Creamos el nuevo operando.
            int IndiceEstadoAceptacionOp1 = Op1.EstadosOperando[Op1.CuentaEstados() - 1].Index; // Obtenemos el indice de aceptación del operando número 1.
            char SimboloTransicion = Op1.ObtenSimboloTransicion(Op1.EstadosOperando[Op1.CuentaEstados() - 2].Index, // Obtenemos el simbolo que va desde el penultimo estado del operando 1 hasta su estado de aceptación. 
                IndiceEstadoAceptacionOp1);
            /*MessageBox.Show(Op1.EstadosOperando[Op1.CuentaEstados() - 2].Index + " " + IndiceEstadoAceptacionOp1);
            MessageBox.Show("Obtuve el siguiente simbolo de transicion: " + SimboloTransicion);*/
            Op1.EliminaTransicionEstado(Op1.EstadosOperando[Op1.CuentaEstados() - 2].Index, // Eliminamos la transición del penultimo estado hacia el estado de aceptación del primer Operando
                Op1.EstadosOperando[Op1.CuentaEstados() - 1].Index);
            Op1.EstadosOperando.RemoveAt(Op1.CuentaEstados() -1); // Eliminamos el estado de aceptación del operando 1
            Op2.DecrementaIndicesEstados(); // Decrementamos los indices del estado 2 para que haya correspondencia de indices.
            int IndiceEstadoInicialOperador2 = Op2.EstadosOperando[0].Index;
            Op1.EstadosOperando[Op1.CuentaEstados() - 1].addTransicion(SimboloTransicion,IndiceEstadoInicialOperador2);
            foreach (Estado e in Op1.EstadosOperando)
            {
                NuevoOperando.EstadosOperando.Add(e);
            }
            foreach (Estado e in Op2.EstadosOperando)
            {
                NuevoOperando.EstadosOperando.Add(e);
            }
            ContadorEstados = Op2.EstadosOperando[Op2.CuentaEstados() - 1].Index + 1;
            return NuevoOperando;
        }

        public Operando addAlternativas(Operando Op1, Operando Op2)
        {
            Operando NuevoOperando = new Operando(new List<Estado>()); // Creamos el nuevo operando que se va a insertar en la pila. 
            int IndiceNuevoInicio= Op1.EstadosOperando[0].Index;
            Op1.IncrementaIndicesEstados();
            Op2.IncrementaIndicesEstados();
            Op1.EstadosOperando[Op1.CuentaEstados() - 1].cambiaTipo(1); // Cambiamos el tipo del estado de aceptación del segundo operando.
            Op2.EstadosOperando[Op2.CuentaEstados() - 1].cambiaTipo(1); // Cambiamos el tipo del estado de aceptación del segundo operando.
            ContadorEstados = Op2.EstadosOperando[Op2.CuentaEstados() - 1].Index + 1;
            Estado NuevoInicio = new Estado(IndiceNuevoInicio,0);
            Estado NuevoFinal = new Estado(ContadorEstados, 2);
            ContadorEstados++;
            NuevoInicio.addTransicion('ε', Op1.GetEstado(0).Index);
            NuevoInicio.addTransicion('ε', Op2.GetEstado(0).Index);
            Op1.GetEstado(Op1.CuentaEstados() - 1).addTransicion('ε', NuevoFinal.Index);
            Op2.GetEstado(Op2.CuentaEstados() - 1).addTransicion('ε', NuevoFinal.Index);

            NuevoOperando.EstadosOperando.Add(NuevoInicio);
            foreach(Estado e in Op1.EstadosOperando)
            {
                NuevoOperando.EstadosOperando.Add(e);
            }

            foreach (Estado e in Op2.EstadosOperando)
            {
                NuevoOperando.EstadosOperando.Add(e);
            }
            NuevoOperando.EstadosOperando.Add(NuevoFinal);
            return NuevoOperando;
        }

        public Operando addCerraduraPositiva(Operando Op1)
        {
            Operando NuevoOperando = new Operando(new List<Estado>());

            int IndexNuevo = Op1.GetEstado(0).Index;
            Op1.IncrementaIndicesEstados();
            Op1.GetEstado(0).cambiaTipo(1);
            Op1.GetEstado(Op1.CuentaEstados() - 1).cambiaTipo(1);
            Estado NuevoInicio = new Estado(IndexNuevo, 0);
            ContadorEstados = Op1.GetEstado(Op1.CuentaEstados() - 1).Index + 1;
            Estado NuevoFinal = new Estado(ContadorEstados, 2);
            ContadorEstados++;
            NuevoInicio.addTransicion('ε', Op1.GetEstado(0).Index);
            Op1.GetEstado(Op1.CuentaEstados() - 1).addTransicion('ε', Op1.GetEstado(0).Index);
            Op1.GetEstado(Op1.CuentaEstados() - 1).addTransicion('ε', NuevoFinal.Index);

            NuevoOperando.EstadosOperando.Add(NuevoInicio);
            foreach (Estado e in Op1.EstadosOperando)
            {
                NuevoOperando.EstadosOperando.Add(e);
            }
            NuevoOperando.EstadosOperando.Add(NuevoFinal);
            return NuevoOperando;
            /*
            this.sumaUnoAEstados();//los estados se recorren en indice
            
            Estado inicialAnterior = inicial;
            inicialAnterior.cambiaTipo(1);
            inicial = new Estado(0,0, alfabeto);//nuevo estado inicial
            estados.Insert(0, inicial);//agrega el nuevo estado inicial al principio de la lista
                   
            Estado finalAnterior = final;
            finalAnterior.cambiaTipo(1);
            final = new Estado(final.Index + 1, 2, alfabeto);
            estados.Add(final);//agrega el nuevo estado final a la lista

            inicial.addTransicion('ε',inicialAnterior,alfabeto);            
            finalAnterior.addTransicion('ε', final, alfabeto);
            finalAnterior.addTransicion('ε', inicialAnterior, alfabeto);*/
        }

        public Operando addCeroOUnaInstancia(Operando Op1)
        {
            Operando NuevoOperando = new Operando(new List<Estado>());

            int IndexNuevo = Op1.GetEstado(0).Index;
            Op1.IncrementaIndicesEstados();
            Op1.GetEstado(0).cambiaTipo(1);
            Op1.GetEstado(Op1.CuentaEstados() - 1).cambiaTipo(1);
            Estado NuevoInicio = new Estado(IndexNuevo, 0);
            ContadorEstados = Op1.GetEstado(Op1.CuentaEstados() - 1).Index + 1;
            Estado NuevoFinal = new Estado(ContadorEstados, 2);
            ContadorEstados++;
            NuevoInicio.addTransicion('ε', Op1.GetEstado(0).Index);
            NuevoInicio.addTransicion('ε', NuevoFinal.Index);
            Op1.GetEstado(Op1.CuentaEstados() - 1).addTransicion('ε', NuevoFinal.Index);

            NuevoOperando.EstadosOperando.Add(NuevoInicio);
            foreach (Estado e in Op1.EstadosOperando)
            {
                NuevoOperando.EstadosOperando.Add(e);
            }
            NuevoOperando.EstadosOperando.Add(NuevoFinal);
            return NuevoOperando;
            /*
            this.sumaUnoAEstados();//los estados se recorren en indice

            Estado inicialAnterior = inicial;
            inicialAnterior.cambiaTipo(1);
            inicial = new Estado(0, 0, alfabeto);//nuevo estado inicial
            estados.Insert(0, inicial);//agrega el nuevo estado inicial al principio de la lista

            Estado finalAnterior = final;
            finalAnterior.cambiaTipo(1);
            final = new Estado(final.Index + 1, 2, alfabeto);
            estados.Add(final);//agrega el nuevo estado final a la lista

            inicial.addTransicion('ε', inicialAnterior, alfabeto);
            finalAnterior.addTransicion('ε', final, alfabeto);
            inicial.addTransicion('ε', final, alfabeto);
            */
        }

        public Operando addCerraduraDeKleen(Operando Op1)
        {
            Operando NuevoOperando = new Operando(new List<Estado>());

            int IndexNuevo = Op1.GetEstado(0).Index;
            Op1.IncrementaIndicesEstados();
            Op1.GetEstado(0).cambiaTipo(1);
            Op1.GetEstado(Op1.CuentaEstados() - 1).cambiaTipo(1);
            Estado NuevoInicio = new Estado(IndexNuevo, 0);
            ContadorEstados = Op1.GetEstado(Op1.CuentaEstados() - 1).Index + 1;
            Estado NuevoFinal = new Estado(ContadorEstados, 2);
            ContadorEstados++;
            NuevoInicio.addTransicion('ε', Op1.GetEstado(0).Index);
            NuevoInicio.addTransicion('ε', NuevoFinal.Index);
            Op1.GetEstado(Op1.CuentaEstados() - 1).addTransicion('ε', Op1.GetEstado(0).Index);
            Op1.GetEstado(Op1.CuentaEstados() - 1).addTransicion('ε', NuevoFinal.Index);

            NuevoOperando.EstadosOperando.Add(NuevoInicio);
            foreach(Estado e in Op1.EstadosOperando)
            {
                NuevoOperando.EstadosOperando.Add(e);
            }
            NuevoOperando.EstadosOperando.Add(NuevoFinal);
            /*
            this.sumaUnoAEstados();//los estados se recorren en indice

            Estado inicialAnterior = inicial;
            inicialAnterior.cambiaTipo(1);
            inicial = new Estado(0, 0, alfabeto);//nuevo estado inicial
            estados.Insert(0, inicial);//agrega el nuevo estado inicial al principio de la lista

            Estado finalAnterior = final;
            finalAnterior.cambiaTipo(1);
            final = new Estado(final.Index + 1, 2, alfabeto);
            estados.Add(final);//agrega el nuevo estado final a la lista

            inicial.addTransicion('ε', inicialAnterior, alfabeto);
            finalAnterior.addTransicion('ε', final, alfabeto);
            finalAnterior.addTransicion('ε', inicialAnterior, alfabeto);
            inicial.addTransicion('ε', final, alfabeto);
            */
            return NuevoOperando;
        }
        public void sumaUnoAEstados()
        {
            foreach(Estado estado in estados)
            {
                estado.sumaUnoAlIndice();
            }
        }

    }
}
