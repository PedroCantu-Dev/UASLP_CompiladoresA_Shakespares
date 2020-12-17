﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ProyectoCompiladores
{

    /*
     { "John Doe", 42 },
     { "Jane Doe", 38 },
     { "Joe Doe", 12 },
     { "Jenna Doe", 12 } */
    public class Gramatica
    {

        public string ProducciónAumentada = ". programa";
        //public string ProducciónAumentada = ". P";
        public AFDL AFD;
        /*List<string> terminales = new List<string>
        {
            "a",
            "b",
            "c"
        };
        */
        public  List<String> terminales = new List<string>
         {
             ";",
             "if",
             "then",
             "end",
             "else",
             "repeat",
             "until",
             "identificador",
             ":=",
             "read",
             "write",
             "<",
             ">",
             "=",
             "+",
             "-",
             "*",
             "/",
             "(",
             ")",
             "numero"
         };
         
        /*
        List<string> NoTerminales = new List<string>
        {
            "P",
        };
       */
       public  List<String> NoTerminales = new List<string>
        {
            "programa",
            "secuencia-sent",
            "sentencia",
            "sent-if",
            "sent-repeat",
            "sent-assign",
            "sent-read",
            "sent-write",
            "exp",
            "op-comp",
            "exp-simple",
            "opsuma",
            "term",
            "opmult",
            "factor"
        };
        
        
        /*Dictionary<string, string> G = new Dictionary<string, string>
        {
            {"P", "a P a|b P b|c"}
        };

        Dictionary<string, string> Siguientes = new Dictionary<string, string>
        {
            {"P", "$ a b"}
        };*/
        Dictionary<string, string> G = new Dictionary<string, string>{
            {"programa", "secuencia-sent" },
            {"secuencia-sent","secuencia-sent ; sentencia |sentencia" },
            {"sentencia","sent-if |sent-repeat |sent-assign |sent-read |sent-write"},
            {"sent-if","if exp then secuencia-sent end |if exp then secuencia-sent else secuencia-sent end"},
            {"sent-repeat","repeat secuencia-sent until exp"},
            {"sent-assign","identificador := exp"},
            {"sent-read","read identificador"},
            {"sent-write","write exp"},
            {"exp","exp-simple op-comp exp-simple |exp-simple"},
            {"op-comp","< |> |=" },
            {"exp-simple","exp-simple opsuma term |term"},
            {"opsuma","+ |-"},
            {"term","term opmult factor |factor"},
            {"opmult","* |/"},
            {"factor" ,"( exp ) |numero |identificador"},
        };


        public Dictionary<string, string> Siguientes = new Dictionary<string, string>{
            {"programa", "$" },
            {"secuencia-sent","; end else until $" },
            {"sentencia","; end else until $" },
            {"sent-if","; end else until $"},
            {"sent-repeat","; end else until $"},
            {"sent-assign","; end else until $"},
            {"sent-read","; end else until $"},
            {"sent-write","; end else until $"},
            {"exp","; end else until $ then )"},
            {"op-comp","( numero identificador" },
            {"exp-simple","; end else until $ then ) < > = + -"},
            {"opsuma","( numero identificador"},
            {"term","; end else until $ then ) < > = + - * /"},
            {"opmult","( numero identificador"},
            {"factor" ,"; end else until $ then ) < > = + - * /"},
        };
        public bool esTerminal(String ent)
        {
            if (this.terminales.Contains(ent))
                return true;
            else
                return false;
        }

        public Gramatica()
        {
            initGramatica();
        }

        public void initGramatica()
        {

            AFD = new AFDL(G, terminales, NoTerminales,ProducciónAumentada);
            AFD.generaTablaDeAnalisisLR0(this.Siguientes);

            /*foreach (KeyValuePair<string, string> EntradaD in Gramática)
            {
                string[] ArregloCadenas = EntradaD.Value.Split('|');
                foreach (string c in ArregloCadenas)
                {
                    string Aux = c.TrimEnd();
                    MessageBox.Show("Producción:\n " + EntradaD.Key + " ----> " + Aux);
                }
            }*/
        }

        

    }

}