﻿using System;
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
using Microsoft.Win32;

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
        //static String puntoCar = ".";
        static String caracteresOtros2 = "[]- ";
        public static String alfabeto = caracteresNumericos + caracteresAlfabeticos + ".";
        static String op_Presedecia1 = "*+?";//jerarquia 1
        static String op_Presedecia2 = "&";//jerarquia 2
        static String op_Presedecia3 = "|";//jerarquia 3
        static List<string> Operadores = new List<String>() { op_Presedecia3, op_Presedecia2, op_Presedecia1 };

        static String op = op_Presedecia1 + op_Presedecia2 + op_Presedecia3;

        private void BT_SubirArchivo1_Click(object sender, EventArgs e)
        {
            inFijaTextBox.Text = SubirArchivo();
        }

        private string SubirArchivo()
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
                return CadenaTextBox;
            }
            return null;
        }
        private void BT_LimpiarTX1_Click(object sender, EventArgs e)
        {
            inFijaTextBox.Text = "";
            TB_ExpresionRegularExplicita.Text = "";
            posFijaTextBox.Text = "";
        }

        private void InToPosBoton_Click(object sender, EventArgs e)
        {
            try
            {
                posFijaTextBox.Text = ConversionPosfija(inFijaTextBox.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show("Ocurrió una excepción: \n" + E.Message);
            }
        }

        private string ConversionPosfija(string Infija)
        {
            String posFija = "";//inicializa la posfija

            Stack<char> pila = new Stack<char>();
            String infija = FormateoExR(Infija);
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

            return posFija;
        }

        private string FormateoExR(string ExpresionRegular)
        {
            /*
            ExpresionRegular = limpiaExpresion(ExpresionRegular);
            string ExRC = CambiaCorchetes(ExpresionRegular);
            string Resultado = CambiaConcatenaciones(ExRC);
            */
            ExpresionRegular = limpiaExpresion(ExpresionRegular);
            String Resultado = desgloseCorchetes(ExpresionRegular);
            TB_ExpresionRegularExplicita.Text = Resultado;
            return Resultado;
        }

        private string CambiaConcatenaciones(string ExpresionRegular)
        {
            string Resultado = "";
            for (int i = 0; i < ExpresionRegular.Length - 1; i++)
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
                        else if (ExpresionRegular[i + 1] == '(')
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

        public String desgloseCorchetes(String expresion)
        {
            String resultado = "";
            Stack<int> corchetesIzquierdos = new Stack<int>();
            Stack<String> expresionesResultantes = new Stack<String>();
            List<int> indicesIzquierdos= new List<int>();
            List<int> indicesDerechos = new List<int>();

            for (int i = 0; i < expresion.Length; i++)
            {
                char caracterDeTurno = expresion.ElementAt(i);//caracter de turno dentro de la expresion

                if (caracterDeTurno == '[' || caracterDeTurno == '(')//corchete izquierdo
                {
                    corchetesIzquierdos.Push(i);
                }
                else if (alfabeto.Contains(caracterDeTurno))//es un caracter-> operando 
                {
                    expresionesResultantes.Push(caracterDeTurno.ToString());
                }
                
                else if(op_Presedecia1.Contains(caracterDeTurno))//es un operador unario
                {
                    String en = "";
                    en = expresionesResultantes.Pop() + caracterDeTurno;
                    expresionesResultantes.Push(en);
                }
                else if (op_Presedecia2.Contains(caracterDeTurno))//es un operador &
                {

                }
                else if (op_Presedecia3.Contains(caracterDeTurno))//es un operador |
                {
                    expresionesResultantes.Push(caracterDeTurno.ToString());
                }
                else if (caracterDeTurno == ']' || caracterDeTurno == ')' )//corchete derecho
                {
                    int indiceInicial = corchetesIzquierdos.Pop();

                    if (expresion.Substring(indiceInicial, i - indiceInicial).Contains('-') && !expresion.Substring(indiceInicial + 1, i - indiceInicial - 1).Contains('[') && !expresion.Substring(indiceInicial + 1, i - indiceInicial - 1).Contains(']'))//es una secuencoa de caracteres
                    {
                        String inicial = "";
                        String final = "";
                        int indiceGuion = expresion.IndexOf('-',indiceInicial);

                        for(int j = i-1; j > indiceInicial; j--)
                        {
                            char car = expresion.ElementAt(j);
                            if (alfabeto.Contains(car))
                            {
                                if (j > indiceGuion)
                                {
                                    final = expresionesResultantes.Pop() + final ;
                                }
                                else
                                {
                                    inicial = expresionesResultantes.Pop() + inicial;
                                }
                            }
                        }
                        expresionesResultantes.Push(desgloseSecuencialCorchetes(inicial,final));
                    }
                    else if(caracterDeTurno == ')') 
                    {
                        String aux = ")";
                        for (int j = i -1; j > indiceInicial; j--)
                        {
                            char car = expresion.ElementAt(j);
                            
                            if (alfabeto.Contains(car))
                            {
                                if (op_Presedecia3.Contains(expresion.ElementAt(j - 1)))
                                {
                                    aux =  expresionesResultantes.Pop() + aux;
                                }
                                else
                                {
                                    aux = "&" + expresionesResultantes.Pop() + aux;
                                }
                            }
                            else if(op_Presedecia3.Contains(car))
                            {
                                String alaDerecha = expresionesResultantes.Pop();
                                String aLaIzquierda = expresionesResultantes.Pop();
                                expresionesResultantes.Push(aLaIzquierda + alaDerecha);
                            }
                            if (indicesDerechos.Contains(j))
                            {
                                int auxIndex = indicesDerechos.IndexOf(j);
                                int nuevoJ = indicesIzquierdos.ElementAt(auxIndex);
                                indicesDerechos.RemoveAt(auxIndex);
                                indicesIzquierdos.RemoveAt(auxIndex);
                                j = nuevoJ;
                                aux = "&" + expresionesResultantes.Pop() + aux;
                            }
                        }
                        aux = "(" + aux.Substring(1,aux.Length-1);
                        expresionesResultantes.Push(aux);
                    }
                    else
                    {
                        String aux = ")";
                        for (int j = i - 1; j > indiceInicial; j--)
                        {
                            char car = expresion.ElementAt(j);

                            if (alfabeto.Contains(car))
                            {
                                aux = "|" + expresionesResultantes.Pop() + aux;
                            }
                            else if (op_Presedecia3.Contains(car))
                            {
                                String alaDerecha = expresionesResultantes.Pop();
                                String aLaIzquierda = expresionesResultantes.Pop();
                                expresionesResultantes.Push(aLaIzquierda + alaDerecha);
                            }
                            if (indicesDerechos.Contains(j))
                            {
                                int auxIndex = indicesDerechos.IndexOf(j);
                                int nuevoJ = indicesIzquierdos.ElementAt(auxIndex);
                                indicesDerechos.RemoveAt(auxIndex);
                                indicesIzquierdos.RemoveAt(auxIndex);
                                j = nuevoJ;
                                aux = "|" + expresionesResultantes.Pop() + aux;
                            }
                        }
                        aux = "(" + aux.Substring(1, aux.Length - 1);
                        expresionesResultantes.Push(aux);
                    }
                    //al final de cada insersion con corchetes se guardan sus indices en listas para las anidadas
                    indicesIzquierdos.Add(indiceInicial);
                    indicesDerechos.Add(i);
                }
                else
                {
                    //algun error
                }
                
            }
            while (expresionesResultantes.Any())
            {
                String resultanteTurno = expresionesResultantes.Pop();
                if (expresionesResultantes.Any())
                {
                    if (expresionesResultantes.Peek() == "|")
                    {
                        expresionesResultantes.Pop();
                        expresionesResultantes.Push(expresionesResultantes.Pop() + "|" + resultanteTurno);
                    }
                    else
                    {
                        //resultado = "&" + resultanteTurno + resultado;
                        expresionesResultantes.Push(expresionesResultantes.Pop() + "&" + resultanteTurno);
                    }
                }
                else
                {
                    resultado = resultanteTurno;
                }
            }
            //resultado = resultado.Substring(1, resultado.Length - 1);

            return resultado;
        }

        public String desgloseSecuencialCorchetes(string primerCaracter, string SegundoCaracter)
        {
            String res = "(";
            int aux;
            //Caracteres Alfabeticos.
            if (caracteresAlfabeticos.Contains(primerCaracter) && caracteresAlfabeticos.Contains(SegundoCaracter))
            {
                if (primerCaracter.Length == 1 && SegundoCaracter.Length == 1)
                {
                    if (caracteresAlfabeticos.IndexOf(primerCaracter) < caracteresAlfabeticos.IndexOf(SegundoCaracter))//el orden de caracteres es creciente
                    {
                        for (int z = caracteresAlfabeticos.IndexOf(primerCaracter); z <= caracteresAlfabeticos.IndexOf(SegundoCaracter); z++)
                        {
                            if (z < caracteresAlfabeticos.IndexOf(SegundoCaracter))
                                res += caracteresAlfabeticos.ElementAt(z) + "|";
                            else
                                res += caracteresAlfabeticos.ElementAt(z) + ")";
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
                else
                {
                    //MessageBox.no se puede hacer lo que quiere
                }
            }
            //Caracteres Numericos.
            else if ( Int32.TryParse(primerCaracter,out aux) && Int32.TryParse(SegundoCaracter,out aux))
            {
                    int primerCaracterDig;
                    Int32.TryParse(primerCaracter, out primerCaracterDig);
                    int segundoCaracterDig;
                    Int32.TryParse(SegundoCaracter, out segundoCaracterDig);

                    if(segundoCaracterDig < primerCaracterDig)
                    {
                        for (int j = primerCaracterDig; j >= segundoCaracterDig; j--)
                        {
                            res += j.ToString() + "|";
                        }
                    }
                    else
                    {
                        for (int j = primerCaracterDig; j <= segundoCaracterDig; j++)
                        {
                            res += j.ToString() + "|";
                        }
                    }
                    
                    res = res.Substring(0,res.Length-1)+')';
            }
            else
            {
                throw new corcheteException("los caracteres entre corchetes no coinciden en tipo");
            }
            return res;
        }

        public String desgloseListaCorchetes(int startIndex, int endIndex,String expresion)
        {
            String res = "(";
            for(int j = 0; j <= endIndex; j++)
            {
                res += expresion[j];
            }
            res += ")";
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
            else if (indiceMayor < indiceMenor)
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

        String limpiaExpresion(String expresion)
        {
            String res = "";
            foreach (char caracter in expresion)
            {
                if (!alfabeto.Contains(caracter) && !op.Contains(caracter) && !caracteresOtros2.Contains(caracter) && caracter != ')' && caracter != '(')
                {
                    throw new alfabetoException("Almenos un caracter de la Expresion no corresponde con el alfabeto");
                }
                else if(caracter != ' ')
                {
                    res += caracter;
                }
            }
            return res;
        }


        #endregion

        #region tab_2avance
        private void BT_SubirArchivo2_Click(object sender, EventArgs e)
        {
            TB_ExpresionR2.Text = SubirArchivo();
        }

        private void BT_LimpiarTexto2_Click(object sender, EventArgs e)
        {
            TB_Posfija2.Text = "";
            TB_ExpresionR2.Text = "";
            DGV_AFN.Columns.Clear();
            DGV_AFN.Rows.Clear();
            DGV_AFN.Columns.Add("Estado", "Estado");
        }

        private void BT_ConversionPosfija2_Click(object sender, EventArgs e)
        {
            try
            {
                TB_Posfija2.Text = ConversionPosfija(TB_ExpresionR2.Text);
            }
            catch (Exception E)
            {
                MessageBox.Show("Ocurrió una excepción: \n" + E.Message);
            }
        }

        private void BT_ConstruirAFN_Click(object sender, EventArgs e)
        {
            AFN AFN = new AFN(TB_Posfija2.Text);
            Operando AFNResultante = AFN.algoritmoDeEvaluacion(TB_Posfija2.Text);
            LLenaDGVAFN(AFNResultante, AFN.alfabeto);
            AFN.estados = AFNResultante.EstadosOperando;
            
        }

        public void LLenaDGVAFN(Operando AFN, string alfabeto)
        {
            DGV_AFN.Columns.Clear();
            DGV_AFN.Rows.Clear();
            //MessageBox.Show("La cantidad de caracteres en el alfabeto es de:  " + alfabeto.Length);
            DGV_AFN.Columns.Add("Estado", "Estado");
            foreach (char c in alfabeto)
            {
                DGV_AFN.Columns.Add(c.ToString(), c.ToString());
            }

            for (int i = 0; i < AFN.EstadosOperando.Count; i++)
            {
                DGV_AFN.Rows.Add();
                DGV_AFN.Rows[i].Cells[0].Value = AFN.EstadosOperando[i].Index;
                List<string> TablaTransiciones = AFN.EstadosOperando[i].ObtenTablaTransiciones(alfabeto);
                for (int j = 0; j < TablaTransiciones.Count; j++)
                {
                    string[] CadenaSplit = TablaTransiciones[j].Split('-');
                    //string Valor  = TablaTransiciones[j];

                    string Valor = "";
                    string CadenaAux = "{";
                    foreach (string c in CadenaSplit)
                    {
                        CadenaAux += c + ",";
                    }
                    Valor = CadenaAux.Remove(CadenaAux.Length - 1);
                    Valor += "}";
                    if (Valor != "{}")
                    {
                        DGV_AFN.Rows[i].Cells[j + 1].Value = Valor;

                    }
                    else
                    {
                        DGV_AFN.Rows[i].Cells[j + 1].Value = "Ø";
                    }
                }
            }
        }


        #endregion

        #region tab_3avance
        
        private void BT_SubirArchivo3_Click(object sender, EventArgs e)
        {
            expresionRegular3.Text = SubirArchivo();
        }

        private void BT_LimpiarTexto3_Click(object sender, EventArgs e)
        {
            posfija3.Text = "";
            expresionRegular3.Text = "";
            dataGrid_AFN3.Columns.Clear();
            dataGrid_AFN3.Rows.Clear();
            dataGrid_AFN3.Columns.Add("Estado","Estado");//, "Estado");

            dataGrid_AFD3.Columns.Clear();
            dataGrid_AFD3.Rows.Clear();
            dataGrid_AFD3.Columns.Add("Estado", "Estado");
        }

        private void BT_ConversionPosfija3_Click(object sender, EventArgs e)
        {
            try
            {
                posfija3.Text = ConversionPosfija(expresionRegular3.Text);

            }
            catch (Exception E)
            {
                MessageBox.Show("Ocurrió una excepción: \n" + E.Message);
            }
        }
        AFN AFN3;
        private void BT_ConstruirAFN3_Click(object sender, EventArgs e)
        {
            AFN AFN3 = new AFN(posfija3.Text);
            Operando AFNResultante = AFN3.algoritmoDeEvaluacion(posfija3.Text);
            AFN3.estados = AFNResultante.EstadosOperando;
            LLenaDGVAFN3(AFNResultante, AFN3.alfabeto);
        }

        private void button_AFD_Click(object sender, EventArgs e)
        {
            AFN AFN3 = new AFN(posfija3.Text);
            Operando AFNResultante = AFN3.algoritmoDeEvaluacion(posfija3.Text);
            AFN3.estados = AFNResultante.EstadosOperando;

            AFD afd = new AFD(AFN3);
            afd.init();
            LLenaDGVAFD3(afd);
            
        }
         
        public void LLenaDGVAFD3(AFD afd)
        {
            dataGrid_AFD3.Columns.Clear();
            dataGrid_AFD3.Rows.Clear();
            //MessageBox.Show("La cantidad de caracteres en el alfabeto es de:  " + alfabeto.Length);
            dataGrid_AFD3.Columns.Add("Estado", "Estado");
            foreach (char c in afd.alfabetoAFD)
            {
                dataGrid_AFD3.Columns.Add(c.ToString(), c.ToString());
            }

            foreach(Destado d in afd.destados.Lista)
            {
                dataGrid_AFD3.Rows.Add(d.getRowTransiciones(afd.alfabetoAFD));
            }
        }

        public void LLenaDGVAFN3(Operando AFN, string alfabeto)
        {
            dataGrid_AFN3.Columns.Clear();
            dataGrid_AFN3.Rows.Clear();
            //MessageBox.Show("La cantidad de caracteres en el alfabeto es de:  " + alfabeto.Length);
            dataGrid_AFN3.Columns.Add("Estado", "Estado");
            foreach (char c in alfabeto)
            {
                dataGrid_AFN3.Columns.Add(c.ToString(), c.ToString());
            }

            for (int i = 0; i < AFN.EstadosOperando.Count; i++)
            {
                dataGrid_AFN3.Rows.Add();
                dataGrid_AFN3.Rows[i].Cells[0].Value = AFN.EstadosOperando[i].Index;
                List<string> TablaTransiciones = AFN.EstadosOperando[i].ObtenTablaTransiciones(alfabeto);
                for (int j = 0; j < TablaTransiciones.Count; j++)
                {
                    string[] CadenaSplit = TablaTransiciones[j].Split('-');
                    //string Valor  = TablaTransiciones[j];

                    string Valor = "";
                    string CadenaAux = "{";
                    foreach (string c in CadenaSplit)
                    {
                        CadenaAux += c + ",";
                    }
                    Valor = CadenaAux.Remove(CadenaAux.Length - 1);
                    Valor += "}";
                    if (Valor != "{}")
                    {
                        dataGrid_AFN3.Rows[i].Cells[j + 1].Value = Valor;

                    }
                    else
                    {
                        dataGrid_AFN3.Rows[i].Cells[j + 1].Value = "Ø";
                    }
                }
            }
        }




        #endregion

        #region tab_4avance
        private void BT_SubirArchivoA4_Click(object sender, EventArgs e)
        {
            TB_ExpresionRegularA4.Text = SubirArchivo();
        }

        private void BT_LimpiarTextoA4_Click(object sender, EventArgs e)
        {
            TB_ExpresionRegularA4.Text = "";
            TB_PosfijaA4.Text = "";
            TB_LexemaA4.Text = "";
            DGV_AFDA4.Columns.Clear();
            DGV_AFDA4.Rows.Clear();
            DGV_AFDA4.Columns.Add("Estado", "Estado");

            DGV_AFNA4.Columns.Clear();
            DGV_AFNA4.Rows.Clear();
            DGV_AFNA4.Columns.Add("Estado", "Estado");
        }

        private void BT_PosfijaA4_Click(object sender, EventArgs e)
        {
            try
            {
                TB_PosfijaA4.Text = ConversionPosfija(TB_ExpresionRegularA4.Text);

            }
            catch (Exception E)
            {
                MessageBox.Show("Ocurrió una excepcion: \n" + E.Message);
            }
        }

        private void BT_ConstruirAFNA4_Click(object sender, EventArgs e)
        {
            AFN AFN4 = new AFN(TB_PosfijaA4.Text);
            Operando AFNResultante = AFN4.algoritmoDeEvaluacion(TB_PosfijaA4.Text);
            AFN4.estados = AFNResultante.EstadosOperando;
            LLenaDGVAFN4(AFNResultante, AFN4.alfabeto);
        }

        private void BT_ConstruirAFDA4_Click(object sender, EventArgs e)
        {
            AFN AFN4 = new AFN(TB_PosfijaA4.Text);
            Operando AFNResultante = AFN4.algoritmoDeEvaluacion(TB_PosfijaA4.Text);
            AFN4.estados = AFNResultante.EstadosOperando;

            AFD afd = new AFD(AFN4);
            afd.init();
            LLenaDGVAFD4(afd);
        }


        private void BT_ValidarLexemaA4_Click(object sender, EventArgs e)
        {

            AFN AFN4 = new AFN(TB_PosfijaA4.Text);
            Operando AFNResultante = AFN4.algoritmoDeEvaluacion(TB_PosfijaA4.Text);
            AFN4.estados = AFNResultante.EstadosOperando;

            AFD afd = new AFD(AFN4);
            afd.init();


            List<int> ListaAceptacion = AFN4.RegresaFinales();
            afd.destados.ChecaFinal(ListaAceptacion);
            bool res = afd.ValidaLexema(TB_LexemaA4.Text);
            if(res == true)
            {
                MessageBox.Show("El lexema es valido");

            }
            else
            {
                MessageBox.Show("El lexema no es valido");

            }
        }

        public void LLenaDGVAFD4(AFD afd)
        {
            DGV_AFDA4.Columns.Clear();
            DGV_AFDA4.Rows.Clear();
            //MessageBox.Show("La cantidad de caracteres en el alfabeto es de:  " + alfabeto.Length);
            DGV_AFDA4.Columns.Add("Estado", "Estado");
            foreach (char c in afd.alfabetoAFD)
            {
                DGV_AFDA4.Columns.Add(c.ToString(), c.ToString());
            }

            foreach (Destado d in afd.destados.Lista)
            {
                DGV_AFDA4.Rows.Add(d.getRowTransiciones(afd.alfabetoAFD));
            }
        }

        public void LLenaDGVAFN4(Operando AFN, string alfabeto)
        {
            DGV_AFNA4.Columns.Clear();
            DGV_AFNA4.Rows.Clear();
            //MessageBox.Show("La cantidad de caracteres en el alfabeto es de:  " + alfabeto.Length);
            DGV_AFNA4.Columns.Add("Estado", "Estado");
            foreach (char c in alfabeto)
            {
                DGV_AFNA4.Columns.Add(c.ToString(), c.ToString());
            }

            for (int i = 0; i < AFN.EstadosOperando.Count; i++)
            {
                DGV_AFNA4.Rows.Add();
                DGV_AFNA4.Rows[i].Cells[0].Value = AFN.EstadosOperando[i].Index;
                List<string> TablaTransiciones = AFN.EstadosOperando[i].ObtenTablaTransiciones(alfabeto);
                for (int j = 0; j < TablaTransiciones.Count; j++)
                {
                    string[] CadenaSplit = TablaTransiciones[j].Split('-');
                    //string Valor  = TablaTransiciones[j];

                    string Valor = "";
                    string CadenaAux = "{";
                    foreach (string c in CadenaSplit)
                    {
                        CadenaAux += c + ",";
                    }
                    Valor = CadenaAux.Remove(CadenaAux.Length - 1);
                    Valor += "}";
                    if (Valor != "{}")
                    {
                        DGV_AFNA4.Rows[i].Cells[j + 1].Value = Valor;

                    }
                    else
                    {
                        DGV_AFNA4.Rows[i].Cells[j + 1].Value = "Ø";
                    }
                }
            }
        }

        #endregion

        #region tab_5avance
        #endregion

        #region tab_6avance
        #endregion


    }//Forms END
}//namespace END
