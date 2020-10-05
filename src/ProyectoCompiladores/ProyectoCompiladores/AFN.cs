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

        protected List<Estado> estados;
        protected String op = "*+?&|";
        protected string OperadoresUnarios = "*+?";
        protected string OperadoresBinarios = "&|C";
        public string alfabeto = "";
        protected int ContadorEstados = 1;
        public AFN(String expresionPosfija)
        {
            estados = new List<Estado>();
            alfabeto = encuentraAlfabeto(expresionPosfija) + "ε";
        }

        //para determinar el alfabeto de la expresion
        public string encuentraAlfabeto(String expresionPosfija)
        {
            string res = "";
            foreach(char caracter in expresionPosfija)
            {
                if(!op.Contains(caracter))
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
                        MessageBox.Show("Estoy haciendo una operacion unaria");
                        Operando O = PilaOperandos.Pop();
                        switch (c)
                        {
                            case '*':
                                Resultado = addCerraduraDeKleen();
                                break;
                            case '+':
                                Resultado = addCerraduraPositiva();
                                break;
                            case '?':
                                Resultado = addCeroOUnaInstancia();
                                break;
                        }
                        //MessageBoxResultadoOperacion(Resultado);
                        PilaOperandos.Push(Resultado);
                    }
                    else if(OperadoresBinarios.Contains(c))
                    {
                        MessageBox.Show("Estoy haciendo una operacion binaria");
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
                    c += "\tCon sus transiciones: \n\n" ;
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
            
            return null;
        }

        public Operando addCerraduraPositiva()
        {
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
            return null;
        }

        public Operando addCeroOUnaInstancia()
        {
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
            return null;
        }

        public Operando addCerraduraDeKleen()
        {
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
            return null;
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
