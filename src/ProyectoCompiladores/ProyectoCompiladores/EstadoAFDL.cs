using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class EstadoAFDL
    {
        public List<string> ElementosEstado;
        public List<TransicionD> Transiciones;
        public int IndiceEstado;

        public EstadoAFDL(List<string> E , int indice)
        {
            ElementosEstado = E;
            Transiciones = new List<TransicionD>();
            IndiceEstado = indice;
        }


        public List<string> DevuelveCadenas(string SimboloABuscar)
        {
            List<string> Resultado = new List<string>();

            foreach(string c in ElementosEstado)
            {
                string[] Split = c.Split(' ');
                String ve = c;

                int indexPunto = -1;
                for (int i = 0; i < Split.Length; i++)
                {
                    if (Split[i] == ".")
                    {
                        indexPunto = i;
                        break;
                    }
                }
                if(indexPunto != -1 && indexPunto != Split.Length-1)
                {
                    if(Split[indexPunto + 1] == SimboloABuscar) // X está después del .
                    {
                        //Crear cadena nueva, luego mover el punto 1 lugar a la derecha.
                        string R = MoverPuntoDerecha(Split, indexPunto);
                        Resultado.Add(R);
                    }
                }
            }
            return Resultado;
        }

        public string MoverPuntoDerecha(string[] Cadena, int indicePunto)
        {
            string[] Resultado = new string[Cadena.Length];
            string R = "";
            for(int i = 0; i < Cadena.Length; i++)
            {
                Resultado[i] = Cadena[i];
            }

            Resultado[indicePunto] = Cadena[indicePunto + 1];
            Resultado[indicePunto + 1] = Cadena[indicePunto];

            foreach(string c in Resultado)
            {
                R += c + " "; 
            }
            return (R.TrimEnd());
        }

        public bool EsIgual(List<string> Candidato)
        {
            bool Res = true;
            int Contador = 0;

            if (Candidato.Count == ElementosEstado.Count)
            {
                foreach (string c in Candidato)
                {
                    if (ElementosEstado.Contains(c))
                    {
                        Contador++;
                    }
                }

                if (Contador == ElementosEstado.Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;      
        }

        public void AgregaTransicion(string Simbolo, int Id)
        {
            TransicionD NuevaTransicion = new TransicionD(Simbolo, Id);
            Transiciones.Add(NuevaTransicion);
        }

        public TransicionD getTransicion(string trans)
        {
            foreach(TransicionD tD in this.Transiciones)
            {
                if(tD.S == trans)
                {
                    return tD;
                }
            }
            return null;
        }

        public string getEstadoString()
        {
            String res = "";

            foreach(String s in this.ElementosEstado)
            {
                res += s+",";
            }
            return res;
        }
    }

    
}
