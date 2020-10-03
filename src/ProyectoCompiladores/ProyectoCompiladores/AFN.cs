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
        public void addEstado()
        {
            if(inicial == null && final == null)
            {
                inicial = new Estado(0,alfabeto);
                final = new Estado(1,alfabeto);
            }
            else
            {
                 inicial = new Estado(final.)
            }
            //Estado inicial = new Estado(,);
        }

        /*Agrega un nuevo estado final y un nuevo estado inicial*/
        public void addCerraduraPositiva()
        {

        }

        public void addCeroOUnaInstancia()
        {

        }

        public void daddCerraduraDeKleen()
        {

        }

        public void addAlternativas()
        {

        }


        public void addConcatenacion()
        {

        }
    }
}
