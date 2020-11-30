using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{

   /*
    { "John Doe", 42 },
    { "Jane Doe", 38 },
    { "Joe Doe", 12 },
    { "Jenna Doe", 12 } */
public class Gramatica
    {
        List<String> terminales = new List<string>
        {
            "if",
            "then",
            "end",
            "else",
            "repeat",
            "until",
            "identificador",
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
        List<String> NoTerminales = new List<string>
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

        Dictionary<string, string> Gramática = new Dictionary<string, string>{
            {"programa", "secuencia-sent" },
            {"secuencia-sent","secuencia-sent ; sentencia | sentencia" },
            {"sentencia","sent-if | sent-repeat | sent-assign | sent-read | sent-write"},
            {"sent-if","if exp then secuencia-sent end | if exp then secuencia-sent else secuencia-sent end"},
            {"sent-repeat","repeat secuencia-sent until exp"},
            {"sent-assign","identificador := exp"},
            {"sent-read","read identificador"},
            {"sent-write","write exp"},
            {"exp","exp-simple op-comp exp-simple | exp-simple"},
            {"op-comp","< | > | =" },
            {"exp-simple","exp-simple opsuma term | term"},
            {"opsuma","+ | -"},
            {"term","term opmult factor | factor"},
            {"opmult","* | /"},
            {"factor" ,"( exp ) | numero | identificador"},
        };

        List<Produccion> Producciones;


        public Gramatica()
        {
            initGramatica();
        }

        public void initGramatica()
        {
            foreach(KeyValuePair<string, string> EntradaD in Gramática)
            {
                string[] ArregloCadenas = EntradaD.Value.Split('|');
            }
        }
    }

}
