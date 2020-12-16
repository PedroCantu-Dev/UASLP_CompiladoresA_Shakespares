using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    public class TABLA_ASLR0
    {
        Celda[,] tablaBruta;
        public TABLA_ASLR0(List<String> terminales, List<String> noTerminales, AFDL afdl )
        {
            int numReng = afdl.Estados.Count();
            int numCol = terminales.Count +noTerminales.Count;
            tablaBruta = new Celda[numReng+1,numCol+1];
            tablaBruta[0, 0] = new Celda("Estados");
            for(int j = 1; j < numReng; j++)
            {
                tablaBruta[j, 0] = new Celda("I"+j.ToString(),j);//se asignan los valores de la columna de estados.
            }
            int i = 0;
            for(i = 0; i < terminales.Count; i++ )
            {
                tablaBruta[0, i + 1] = new Celda(terminales.ElementAt(i));
            }

            for (; i < numCol; i++)
            {
                tablaBruta[0, i-(terminales.Count+1)] = new Celda(terminales.ElementAt(i));
            }

        }

        public bool insertaCeldaByIndex(int renglon, int columna, Celda insersion)
        {
            if(renglon < tablaBruta.GetLength(0) && columna < tablaBruta.GetLength(1))
            {
                this.tablaBruta[renglon, columna] = insersion;
                return true;
            }
            return false;            
        }

        public bool insertaCelda(String renglon, String columna, Celda insersion)
        {
            for(int i = 0; i < this.tablaBruta.GetLength(0); i++)
            {
                if(tablaBruta[i,0].alfa == renglon)
                {
                    for (int j = 0; j < this.tablaBruta.GetLength(1); j++)
                    {
                        if(tablaBruta[0,j].alfa == columna)
                        {
                            return insertaCeldaByIndex(i,j,insersion);
                        }
                    }
                }                
            }
            return false;
        }

        public class Celda
        {
           
            public String alfa { get; set; }
            public int numerico { get; set; }

            public Celda(String Alfa, int numerico)
            {
                this.alfa = alfa;
                this.numerico = numerico;

            }
            public Celda(String Alfa)
            {
                this.alfa = alfa;
                this.numerico = -1;

            }
        }

        
    }


    
}
