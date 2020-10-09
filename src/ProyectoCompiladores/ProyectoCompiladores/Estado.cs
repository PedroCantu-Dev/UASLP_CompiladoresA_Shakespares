using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCompiladores
{
    public class Estado
    {
        int index { get; set; }
        public int tipo { get; set; }//0->Inicial, 1->Neutro, 2->Aceptacion
        //protected List<Estado>[] fila;
        public List<Transicion> Transiciones;
        public Estado(int index, int tipo)
        {
            this.index = index;
            this.tipo = tipo;
            Transiciones = new List<Transicion>();
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

        public void addTransicion(char Simbolo, int IdEstado)
        {
            //MessageBox.Show("Tengo : " + Transiciones.Count + " transiciones y estoy agregando una nueva hacia: " + IdEstado + " con el simbolo: " + Simbolo);
            Transicion NuevaTransicion = new Transicion(Simbolo, IdEstado);
            Transiciones.Add(NuevaTransicion);
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public List<string> ObtenTablaTransiciones(string Alfabeto)
        {
            List<string> TablaTransiciones = new List<string>();
            foreach (char c in Alfabeto)
            {
                string Cadena = "";
                for(int i = 0; i < Transiciones.Count; i++)
                {
                    if(Transiciones[i].Simbolo == c)
                    {
                        Cadena += Transiciones[i].IdEstadoDestino;
                        Cadena += "-";
                    }
                }
                if(Cadena.Length > 1)
                {
                    string Aux = Cadena.Remove(Cadena.Length - 1);
                    TablaTransiciones.Add(Aux);
                }
                else
                {
                    TablaTransiciones.Add(Cadena);
                }   
            }
            return TablaTransiciones;
        }        
    }
}
