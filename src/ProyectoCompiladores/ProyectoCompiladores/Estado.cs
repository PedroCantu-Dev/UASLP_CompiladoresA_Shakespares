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
        int tipo;//1->Inicial, 2->Transision, 3->Aceptacion

        protected List<List<Estado>> fila;

        public Estado(int index, String alfabeto)
        {
            this.index = index;
            fila = new List<List<Estado>>(alfabeto.Length);
            
        }

        public void sumaUnoAlIndice()
        {
            index++;
        }

        public void cambiaTipo(int newTipo)
        {
            tipo = newTipo;
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        
    }
}
