using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class AFN
    {
        protected Estado inicial = null;
        protected Estado final = null;

        protected List<Estado> estados;
        protected String op = "*+?&|";
        protected String alfabeto = "";
        

        public AFN(String expresionPosfija)
        {
            estados = new List<Estado>();
            alfabeto = encuentraAlfabeto(expresionPosfija) + "ε";
        }

        //para determinar el alfabeto de la expresion
        public String encuentraAlfabeto(String expresionPosfija)
        {
            String res = "";
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
        public void algoritmoDeEvaluacion(String expresionPosfija)
        {

        }

        /*agrega un nuevo estado de inicio y uno de aceptacion con una transicion*/
        public void addTransicion(char transision)
        {
            if(inicial == null && final == null)
            {
                inicial = new Estado(0,0,alfabeto);
                final = new Estado(1,2,alfabeto);
            }
            else
            {
               
            }
            //Estado inicial = new Estado(,);
        }

        /*Agrega un nuevo estado final y un nuevo estado inicial*/
        public void addCerraduraPositiva()
        {
            this.sumaUnoAEstados();//los estados se recorren en indice
            
            Estado inicialAnterior = inicial;
            inicialAnterior.cambiaTipo(1);
            inicial = new Estado(0,0, alfabeto);//nuevo estado inicial
            estados.Insert(0, inicial);//agrega el nuevo estado inicial al principio de la lista
                   
            Estado finalAnterior = final;
            finalAnterior.cambiaTipo(1);
            final = new Estado(final.Index + 1, 2, alfabeto);
            estados.Add(final);//agrega el nuevo estado final a la lista

            inicial.addTransision('ε',inicialAnterior,alfabeto);            
            finalAnterior.addTransision('ε', final, alfabeto);
            finalAnterior.addTransision('ε', inicialAnterior, alfabeto);
        }

        public void addCeroOUnaInstancia()
        {
            this.sumaUnoAEstados();//los estados se recorren en indice

            Estado inicialAnterior = inicial;
            inicialAnterior.cambiaTipo(1);
            inicial = new Estado(0, 0, alfabeto);//nuevo estado inicial
            estados.Insert(0, inicial);//agrega el nuevo estado inicial al principio de la lista

            Estado finalAnterior = final;
            finalAnterior.cambiaTipo(1);
            final = new Estado(final.Index + 1, 2, alfabeto);
            estados.Add(final);//agrega el nuevo estado final a la lista

            inicial.addTransision('ε', inicialAnterior, alfabeto);
            finalAnterior.addTransision('ε', final, alfabeto);
            inicial.addTransision('ε', final, alfabeto);

        }

        public void daddCerraduraDeKleen()
        {
            this.sumaUnoAEstados();//los estados se recorren en indice

            Estado inicialAnterior = inicial;
            inicialAnterior.cambiaTipo(1);
            inicial = new Estado(0, 0, alfabeto);//nuevo estado inicial
            estados.Insert(0, inicial);//agrega el nuevo estado inicial al principio de la lista

            Estado finalAnterior = final;
            finalAnterior.cambiaTipo(1);
            final = new Estado(final.Index + 1, 2, alfabeto);
            estados.Add(final);//agrega el nuevo estado final a la lista

            inicial.addTransision('ε', inicialAnterior, alfabeto);
            finalAnterior.addTransision('ε', final, alfabeto);
            finalAnterior.addTransision('ε', inicialAnterior, alfabeto);
            inicial.addTransision('ε', final, alfabeto);
        }

        public void addAlternativas()
        {

        }


        public void addConcatenacion()
        {

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
