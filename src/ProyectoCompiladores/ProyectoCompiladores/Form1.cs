using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Globalization;

namespace ProyectoCompiladores
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region tab_PreparacionDelProyecto
        private void BT_SubirArchivoPrep_Click(object sender, EventArgs e)
        {
            OpenFileDialog VentanaCargaArchivo = new OpenFileDialog();
            VentanaCargaArchivo.InitialDirectory = "c:\\";
            VentanaCargaArchivo.Filter = "Archivo de Texto (*.txt)|*.txt";
            if (VentanaCargaArchivo.ShowDialog() == DialogResult.OK)
            {
                string CadenaAux = "";
                string CadenaTextBox = "";
                string RutaArchivo = VentanaCargaArchivo.FileName;
                MessageBox.Show("Ruta Archivo :" + RutaArchivo);
                StreamReader LectorArchivo = new StreamReader(RutaArchivo);
                while ((CadenaAux = LectorArchivo.ReadLine()) != null)
                {
                    CadenaAux += "\n";
                    CadenaTextBox += CadenaAux;
                }
                TB_SubirArchivo.Text = CadenaTextBox;
            }
        }

        private void BT_LimpiarTXPrep_Click(object sender, EventArgs e)
        {
            TB_SubirArchivo.Text = "";
        }
        #endregion

        #region tab_1Avance

        static String caracteresNumericos = "0123456789";
        static String caracteresAlfabeticos = "abcdefghijklmnñopqrstuvxyz";
        static String caracteresOtros = ".";
        static String alfabeto = caracteresNumericos + caracteresAlfabeticos + caracteresOtros;
        static String op_Presedecia1 = "*+?";//jerarquia 1
        static String op_Presedecia2 = "&";//jerarquia 2
        static String op_Presedecia3 = "|";//jerarquia 3
        static List<string> Operadores = new List<String>() { op_Presedecia3, op_Presedecia2, op_Presedecia1 };
 
        static String op = op_Presedecia1 + op_Presedecia2 + op_Presedecia3;

        private void BT_SubirArchivo1_Click(object sender, EventArgs e)
        {
            OpenFileDialog VentanaCargaArchivo = new OpenFileDialog();
            VentanaCargaArchivo.InitialDirectory = "c:\\";
            VentanaCargaArchivo.Filter = "Archivo de Texto (*.txt)|*.txt";
            if (VentanaCargaArchivo.ShowDialog() == DialogResult.OK)
            {
                string CadenaAux = "";
                string CadenaTextBox = "";
                string RutaArchivo = VentanaCargaArchivo.FileName;
                MessageBox.Show("Ruta Archivo :" + RutaArchivo);
                StreamReader LectorArchivo = new StreamReader(RutaArchivo);
                while ((CadenaAux = LectorArchivo.ReadLine()) != null)
                {
                    CadenaAux += "\n";
                    CadenaTextBox += CadenaAux;
                }
                inFijaTextBox.Text = CadenaTextBox;
            }
        }
        private void BT_LimpiarTX1_Click(object sender, EventArgs e)
        {
            inFijaTextBox.Text = "";
            TB_ExpresionRegularExplicita.Text = "";
            posFijaTextBox.Text = "";
        }

        private void InToPosBoton_Click(object sender, EventArgs e)
        {
            String posFija = "";//inicializa la posfija

            Stack<char> pila = new Stack<char>();
            String infija = FormateoExR(inFijaTextBox.Text);
            //infija = desglosaCorchetes(infija);

            //variables de control para el primer while
            Boolean error = false;
            int contadorInfija = 0;
            Boolean band = false;

            if (infija == "")//si la expresion entrante esta vacia marca error
            {
                error = true;
            }

            while (!error && contadorInfija < infija.Length)
            {
                //char caracter = infija.Substring(contadorInfija, 1);
                char caracter = infija[contadorInfija];

                switch (caracter)
                {
                    case ' ': //en caso de ser un espaciolo ignora
                        break;
                    case '\n'://al final de la expresion
                        while (pila.Any())
                        {
                            if (pila.Peek() != '(' && pila.Peek() != ')')
                            {
                                posFija += pila.Pop();
                            }
                            else
                            {
                                pila.Pop();
                            }
                        }
                        break;
                    case '(': //parentesis izquierdo.
                        pila.Push(caracter);
                        break;
                    case ')'://parentesis derecho.
                        while (pila.Peek() != '(')
                        {
                            posFija += pila.Pop(); ;//despliega en posFija
                        }
                        pila.Pop(); //saca el parentesis izquierdo sin desplegarlo.
                        break;
                    default:
                        if (alfabeto.Contains(caracter)) //es un operando.
                        {
                            posFija += caracter; //despliega en posFija
                        }
                        else
                        {
                            if (op.Contains(caracter))//es un operador.
                            {
                                band = true;
                                while (band)
                                {
                                    if (!pila.Any() || pila.Peek() == '(' || prioridad(caracter, pila.Peek()))
                                    {
                                        pila.Push(caracter);
                                        band = false;
                                    }
                                    else
                                    {
                                        posFija += pila.Pop();
                                    }
                                }
                            }
                            else//se introdujo algun caracter invalido.
                            {
                                error = true;
                            }
                        }
                        break;
                }
                contadorInfija++;
            }
            if (!error)
            {
                while (pila.Any())
                {
                    posFija += pila.Pop();
                }
            }

            posFijaTextBox.Text = posFija;
        }

        private string FormateoExR(string ExpresionRegular)
        {
            string ExRC = CambiaCorchetes(ExpresionRegular);
            string Resultado = CambiaConcatenaciones(ExRC);
            TB_ExpresionRegularExplicita.Text = Resultado;
            return Resultado;
        }

        private string CambiaConcatenaciones(string ExpresionRegular)
        {
            string Resultado = "";
            for(int i = 0; i < ExpresionRegular.Length - 1; i++)
            {
                if (ExpresionRegular[i] == '[')
                {
                    int IndiceFinal = ExpresionRegular.IndexOf(']', i);
                    for (int z = i; z <= IndiceFinal; z++)
                    {
                        Resultado += ExpresionRegular[z];
                    }
                    i += IndiceFinal - i;
                }
                else
                {
                    Resultado += ExpresionRegular[i];
                    if (op_Presedecia1.Contains(ExpresionRegular[i]))
                    {
                        if (alfabeto.Contains(ExpresionRegular[i + 1])) //(*&a)
                        {
                            Resultado += "&";
                        }
                        else if (ExpresionRegular[i + 1] == '[')
                        {
                            Resultado += "&";
                        }
                        else if(ExpresionRegular[i+1] == '(')
                        {
                            Resultado += "&";
                        }
                    }
                    else if (alfabeto.Contains(ExpresionRegular[i]) && ExpresionRegular[i + 1] == '(') //a(b) = a&(b
                    {
                        Resultado += "&";
                    }

                    else if (alfabeto.Contains(ExpresionRegular[i]) && alfabeto.Contains(ExpresionRegular[i + 1])) // ab = a&b
                    {
                        Resultado += "&";
                    }
                    else if (ExpresionRegular[i] == ')' && alfabeto.Contains(ExpresionRegular[i + 1])) //(a|b)&a
                    {
                        Resultado += "&";
                    }
                    else if (alfabeto.Contains(ExpresionRegular[i]) && ExpresionRegular[i + 1] == '[') //a&[bc] )&(
                    {
                        Resultado += "&";
                    }
                    else if (ExpresionRegular[i] == ')' && ExpresionRegular[i + 1] == '(')
                    {
                        Resultado += "&";
                    }
                }
            }
            Resultado += ExpresionRegular[ExpresionRegular.Length - 1];
            //MessageBox.Show("La cadena una vez hecho las concatenaciones pertinentes queda: " + Resultado);
            return Resultado;
        }
/*------------------------------------------------------------------------------------------------------------------------------------------------
        public string desglosaCorchetes(string ExpresionRegular)
        {
            string Resultado = "";

            Stack<int> pilaDeIndices = new Stack<int>();//guarda los indices de los corchetes.

            for (int i = 0; i < ExpresionRegular.Length; i++)
            {
                if (ExpresionRegular[i] == '[') //corchete izquierdo.
                {
                    pilaDeIndices.Push(i);                  
                }
                else if(ExpresionRegular[i] != ']')//corchete derecho.
                {
                    String subCadenaDeCorchetes = "(";
                    if (pilaDeIndices.Any() == false)//si la pila esta vacia quiere decir que hay un signo ] de mas y retorna error.
                    {
                        throw new corcheteException("falta por lo menos un corchete izquierdo");
                    }
                    int indiceInicio = pilaDeIndices.Pop();

                    if (ExpresionRegular.IndexOf('[', indiceInicio, i - indiceInicio))
                    {
                        int indiceMedio = ExpresionRegular.IndexOf('-', indiceInicio, i - indiceInicio);
                    }
                    else
                    {

                    }

                    if(indiceMedio>0)//si encuentra un guion(-)
                    {
                        char primerCaracter = ' ';
                        char SegundoCaracter = ' ';
                        for(int z = indiceInicio; z < indiceMedio; z++)
                        {
                            switch (ExpresionRegular[z])
                            {
                                case ' ':
                                    break;
                                default:
                                    if(primerCaracter != ' ')// ya se habia asignado antes
                                    {
                                        throw new corcheteException("Mas de un caracter entre corchete y guion");
                                    }
                                    primerCaracter = ExpresionRegular[z];
                                    break;
                            }

                        }
                        for (int z = indiceMedio; z < i; z++)
                        {
                            switch (ExpresionRegular[z])
                            {
                                case ' ':
                                    break;
                                default:
                                    if (SegundoCaracter != ' ')// ya se habia asignado antes
                                    {
                                        throw new corcheteException("Mas de un caracter entre guion y corchete");
                                    }
                                    SegundoCaracter = ExpresionRegular[z];
                                    break;
                            }
                        }
                        //Caracteres Alfabeticos.
                        if(caracteresAlfabeticos.Contains(primerCaracter) && caracteresAlfabeticos.Contains(SegundoCaracter))
                        {
                            if(caracteresAlfabeticos.IndexOf(primerCaracter) < caracteresAlfabeticos.IndexOf(SegundoCaracter))//el orden de caracteres es creciente
                            {
                                for(int z = caracteresAlfabeticos.IndexOf(primerCaracter); z <= caracteresAlfabeticos.IndexOf(SegundoCaracter); z++)
                                {
                                    if (z < caracteresAlfabeticos.IndexOf(SegundoCaracter))
                                        subCadenaDeCorchetes += ExpresionRegular[z] + "|";
                                    else
                                        subCadenaDeCorchetes += ExpresionRegular[z];
                                }
                            }
                            else
                            {
                                for (int z = caracteresAlfabeticos.IndexOf(primerCaracter); z >= caracteresAlfabeticos.IndexOf(SegundoCaracter); z--)
                                {
                                    if(z > caracteresAlfabeticos.IndexOf(SegundoCaracter))
                                        subCadenaDeCorchetes += ExpresionRegular[z] + "|";
                                    else
                                        subCadenaDeCorchetes += ExpresionRegular[z];
                                }
                            }
                        }
                        //Caracteres Numericos.
                        else if(caracteresNumericos.Contains(primerCaracter) && caracteresNumericos.Contains(SegundoCaracter))
                        {
                            if (caracteresNumericos.IndexOf(primerCaracter) < caracteresNumericos.IndexOf(SegundoCaracter))//el orden de caracteres es creciente
                            {
                                for (int z = caracteresNumericos.IndexOf(primerCaracter); z <= caracteresNumericos.IndexOf(SegundoCaracter); z++)
                                {
                                    if (z < caracteresNumericos.IndexOf(SegundoCaracter))
                                        subCadenaDeCorchetes += ExpresionRegular[z] + "|";
                                    else
                                        subCadenaDeCorchetes += ExpresionRegular[z];
                                }
                            }
                            else
                            {
                                for (int z = caracteresNumericos.IndexOf(primerCaracter); z >= caracteresNumericos.IndexOf(SegundoCaracter); z--)
                                {
                                    if (z > caracteresNumericos.IndexOf(SegundoCaracter))
                                        subCadenaDeCorchetes += ExpresionRegular[z] + "|";
                                    else
                                        subCadenaDeCorchetes += ExpresionRegular[z];
                                }
                            }
                        }
                        //caracteres Combinados.
                        else
                        {
                            throw new corcheteException("los caracteres entre corchetes no coinciden en tipo");
                        }
                        subCadenaDeCorchetes += ")";
                    }
                    else//es una secuencia de caracteres a los que se les aplica el operando de alternativas
                    {
                        for(int z = indiceInicio + 1; z < i - 1; z++)
                        {
                            if(alfabeto.Contains(ExpresionRegular[z]) || ExpresionRegular[z]== ' ' )
                            {
                                if(ExpresionRegular[z] != ' ')//ignora los espacios.
                                {
                                    if(z < i-1 )
                                    {
                                        subCadenaDeCorchetes += ExpresionRegular[z] + "|";
                                    }
                                    else
                                    {
                                        subCadenaDeCorchetes += ExpresionRegular[z] + ")";
                                    }
                                }
                            }
                            else
                            {
                                throw new corcheteException("caracter invalido");
                            }
                        }    
                    }

                    //Resultado += ExpresionRegular[i];
                }//if(ExpresionRegular[i] != ']')//corchete derecho(END).
            }
            return Resultado;
        }
-----------------------------------------------------------------------------------------------------------------------*/
/********************************************************
 * **********************************************/
    public String desgloseCorchetes(String expresion)
        {
            String resultado = "";
            Stack<int> corchetesIzquierdos = new Stack<int>();
            Stack<String> expresionesResultantes = new Stack<String>();
            for(int i = 0; i < expresion.Length; i++ )
            {
                char caracterDeTurno = expresion.ElementAt(i);//caracter de urno dentro de la expresion

                if(caracterDeTurno == '[')//corchete izquierdo
                {
                    corchetesIzquierdos.Push(i);
                }
                else if(caracterDeTurno == ']')//corchete derecho
                {
                    int indiceInicial = corchetesIzquierdos.Pop();
                }
                else if(alfabeto.Contains(caracterDeTurno) )//es un caracter
                {
                    expresionesResultantes.Push(expresionesResultantes.ElementAt(i));
                }
                else //es un operando.
                {
                    String en = "";
                    en = expresionesResultantes.Pop() + caracterDeTurno;
                    expresionesResultantes.Push(en);
                }
            }
            while(expresionesResultantes.Any() == true)
            {
                resultado = expresionesResultantes.Pop()
            }

            return resultado;
        }
        
        
        
        
        
        
  
        
        
        public String desgloseSecuencialCorchetes(char primerCaracter, char SegundoCaracter)
    {
       String res = "(";
            //Caracteres Alfabeticos.
            if (caracteresAlfabeticos.Contains(primerCaracter) && caracteresAlfabeticos.Contains(SegundoCaracter))
            {
                if (caracteresAlfabeticos.IndexOf(primerCaracter) < caracteresAlfabeticos.IndexOf(SegundoCaracter))//el orden de caracteres es creciente
                {
                    for (int z = caracteresAlfabeticos.IndexOf(primerCaracter); z <= caracteresAlfabeticos.IndexOf(SegundoCaracter); z++)
                    {
                        if (z < caracteresAlfabeticos.IndexOf(SegundoCaracter))
                            res += caracteresAlfabeticos.ElementAt(z) + "|";
                        else
                            res += caracteresAlfabeticos.ElementAt(z)+ ")";
                    }
                }
                else
                {
                    for (int z = caracteresAlfabeticos.IndexOf(primerCaracter); z >= caracteresAlfabeticos.IndexOf(SegundoCaracter); z--)
                    {
                        if (z > caracteresAlfabeticos.IndexOf(SegundoCaracter))
                            res += caracteresAlfabeticos.ElementAt(z) + "|";
                        else
                            res += caracteresAlfabeticos.ElementAt(z) + ")";
                    }
                }
            }
            //Caracteres Numericos.
            else if (caracteresNumericos.Contains(primerCaracter) && caracteresNumericos.Contains(SegundoCaracter))
            {
                if (caracteresNumericos.IndexOf(primerCaracter) < caracteresNumericos.IndexOf(SegundoCaracter))//el orden de caracteres es creciente
                {
                    for (int z = caracteresNumericos.IndexOf(primerCaracter); z <= caracteresNumericos.IndexOf(SegundoCaracter); z++)
                    {
                        if (z < caracteresNumericos.IndexOf(SegundoCaracter))
                            res += caracteresNumericos.ElementAt(z) + "|";
                        else
                            res += caracteresNumericos.ElementAt(z) + ")";
                    }
                }
                else
                {
                    for (int z = caracteresNumericos.IndexOf(primerCaracter); z >= caracteresNumericos.IndexOf(SegundoCaracter); z--)
                    {
                        if (z > caracteresNumericos.IndexOf(SegundoCaracter))
                            res += caracteresNumericos.ElementAt(z) + "|";
                        else
                            res += caracteresNumericos.ElementAt(z) + ")";
                    }
                }
            }
            //caracteres Combinados.
            else
            {
                throw new corcheteException("los caracteres entre corchetes no coinciden en tipo");
            }
            return res;
        }





        public string CambiaCorchetes(string ExpresionRegular)
        {
            string Resultado = "";

            for (int i = 0; i < ExpresionRegular.Length; i++)
            {
                if (ExpresionRegular[i] == '[') //
                {

                    int IndiceFinal = ExpresionRegular.IndexOf(']', i);
                    if (ExpresionRegular.IndexOf('-', i) < IndiceFinal && ExpresionRegular.IndexOf('-', i) != -1) // En caso de que sea un rango de numeros y letras. Ejemplo[0-5][a-c]
                    {
                        int IndiceMedio = ExpresionRegular.IndexOf('-', i);
                        int NumeroDigInicial = ExpresionRegular.IndexOf('-', i) - i;
                        string NumeroInicial = "";
                        for (int z = 1; z < NumeroDigInicial; z++)
                        {
                            NumeroInicial += ExpresionRegular[i + z];
                        }

                        int ValorInicial = 0;
                        if (int.TryParse(NumeroInicial, out ValorInicial))
                        {
                            int NumeroDigitos = IndiceFinal - IndiceMedio;
                            int valorFinal;
                            string Valor = "";
                            for (int z = IndiceMedio + 1; z < IndiceFinal; z++)
                            {
                                Valor += ExpresionRegular[z];
                            }
                            valorFinal = int.Parse(Valor);
                            //MessageBox.Show("El valor inicial es de: " + ValorInicial + "\nEl valor final es de: " + valorFinal);
                            Resultado += '(';
                            for (int j = ValorInicial; j <= valorFinal - 1; j++)
                            {
                                Resultado += j;
                                Resultado += '|';
                            }
                            Resultado += valorFinal;
                            Resultado += ')';
                        }
                        else // En este caso contemplamos que sea un rango pero de cadenas.
                        {
                            string ValorFinal = "";
                            for (int z = IndiceMedio + 1; z < IndiceFinal; z++)
                            {
                                ValorFinal += ExpresionRegular[z];
                            }
                            int IndiceInicialCad = alfabeto.IndexOf(NumeroInicial);
                            int IndiceFinalCad = alfabeto.IndexOf(ValorFinal);
                            if (IndiceFinalCad != -1 && IndiceInicialCad != -1)
                            {
                                Resultado += '(';
                                for (int j = IndiceInicialCad; j <= IndiceFinalCad - 1; j++)
                                {
                                    Resultado += alfabeto[j];
                                    Resultado += '|';
                                }
                                Resultado += alfabeto[IndiceFinalCad];
                                Resultado += ')';
                            }
                        }
                    }
                    else // Si no es un rango de números. Ejemplo[0ABC]
                    {
                        Resultado += '(';
                        for (int j = i + 1; j < IndiceFinal - 1; j++)
                        {
                            Resultado += ExpresionRegular[j];
                            Resultado += '|';
                        }
                        Resultado += ExpresionRegular[IndiceFinal - 1];
                        Resultado += ')';
                    }
                    i += IndiceFinal - i;
                    //MessageBox.Show("La cadena va quedando así: " + Resultado);
                }
                else if (ExpresionRegular[i] != ']')
                {
                    Resultado += ExpresionRegular[i];
                }
            }
            //MessageBox.Show("Expresión regular explicita al desglosar los corchetes: " + Resultado);
            return Resultado;
        }



        private Boolean prioridad(char c1, char c2)
        {
            int indiceMayor = -1;
            int indiceMenor = -1;
            int i = 0;
            foreach (string c in Operadores)
            {
                
                if (c.Contains(c1))
                {
                    indiceMayor = i;
                    break;
                }
                i++;
            }

            i = 0;
            foreach (string c in Operadores)
            {
                if (c.Contains(c2))
                {
                    indiceMenor = i;
                    break;
                }
                i++;
            }

            
            if (indiceMayor > indiceMenor)
            {
                //MessageBox.Show(c1  + " : " + indiceMayor  + "\n" + " tiene mayor prioridad que: \n" + c2  + ":" + indiceMenor);
                return true;
            }
            else if(indiceMayor < indiceMenor)
            {
                //MessageBox.Show(c2  + " : " + indiceMenor + "\n"  + " tiene mayor prioridad que: " + c1 + " : " + indiceMayor);
                return false;
            }
            else
            {
                //MessageBox.Show(c1 + " : " +  indiceMayor + "\n " + "tiene la misma prioridad que: " + c2 + " : " + indiceMenor );
                return false;
            }
        }

        void compruebaAlfabeto(String expresion)
        {
            foreach(char caracter in expresion)
            {
                if(!alfabeto.Contains(caracter) && !op.Contains(caracter))
                {
                    throw new alfabetoException("Almenos un caracter de la Expresion no corresponde con el alfabeto");
                }
            }
                
        }

        
        #endregion

        #region tab_3avance
        #endregion

        #region tab_4avance
        #endregion

        #region tab_5avance
        #endregion

        #region tab_6avance
        #endregion

    }//Forms END
}//namespace END
