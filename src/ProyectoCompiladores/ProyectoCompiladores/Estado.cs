using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class Estado
    {
        int index;
        int tipo;//0->Inicial, 1->Neutro, 2->Aceptacion
        

        protected List<Estado>[] fila;

        public Estado(int index, int tipo, String alfabeto)
        {
            this.index = index;
            this.tipo = tipo;
            fila = new List<Estado>[alfabeto.Length];            
        }

        public void sumaUnoAlIndice()
        {
            index++;
        }

        public void cambiaTipo(int newTipo)
        {
            tipo = newTipo;
        }

        public void cambiaTipoEIndice(int newTipo)
        {
            tipo = newTipo;
            index++;
        }

        public void addTransision(char transision, Estado estado, String alfabeto)
        {
            int indice = alfabeto.IndexOf(transision);
            if(indice > -1)
            {
                if(fila[indice] == null)
                {
                    fila[indice] = new List<Estado>();
                }
                fila[indice].Add(estado);
            }
            else
            {
                //tira una excepcion
            }

        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        
    }
}
