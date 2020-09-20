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
        /*

                EVALUACIÓN DE EXPRESIONES EN NOTACIÓN POSFIJA.

                1. Inicializar una pila.
                2. Apuntar al primer carácter de la expresión posfija.
                while ( no ocurra un error && no sea fin de la expresión posfija )
                {
                    switch ( carácter )
                    {
                        Operando:
                        Insertar en la pila;
                        break;
                        Operador:
                        if ( si es “unario”)
                        {
                            Extraer el valor del tope de la pila y aplicar el operador.
                            (Se produce un error en caso de no tener valor).
                            Insertar el resultado en el nuevo tope de la pila.
                        }
                        else ( si es “binario”)
                        {
                            Extraer los 2 valores del tope de la pila y aplicar el operador.
                            (Se produce un error en caso de no tener los 2 valores).
                            Insertar el resultado en el nuevo tope de la pila.
                        }
                        break;
                    }
                    Apuntar al siguiente carácter de la expresión posfija.
                }
        */

        static String alfabeto = "abcdefghijklmnñopqrstuvxyz0123456789";
        static String op_Presedecia1 = "*+?.";//jerarquia 1
        static String op_Presedecia2 = "&";//jerarquia 2
        static String op_Presedecia3 = "|";//jerarquia 3
        static List<string> Operadores = new List<String>() { op_Presedecia3, op_Presedecia2, op_Presedecia1 };
        // static List<String> operadores = new List<String>(op_Presedecia1,op_Presedecia2,op_Presedecia3);

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
            String posFija = "";

            Stack<char> pila = new Stack<char>();


            String infija = FormateoExR(inFijaTextBox.Text);
            //infija = desglosaCorchetes(infija);

            //variables de control para el primer while
            Boolean error = false;
            int contadorInfija = 0;
            Boolean band = false;

            if (infija == "")
            {
                error = true;
            }

            while (!error && contadorInfija < infija.Length)
            {
                //char caracter = infija.Substring(contadorInfija, 1);
                char caracter = infija[contadorInfija];

                switch (caracter)
                {
                    case ' ': //en caso de ser un espacio.
                        break;
                    case '\n':
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
            string ExRC = CambiaConcatenaciones(ExpresionRegular);
            string Resultado = CambiaCorchetes(ExRC);
            TB_ExpresionRegularExplicita.Text = Resultado;
            return Resultado;
        }

        private string CambiaConcatenaciones(string ExpresionRegular)
        {
            string Resultado = "";
            for(int i = 0; i < ExpresionRegular.Length - 1; i++)
            {
                if(ExpresionRegular[i] == '[')
                {
                    int IndiceFinal = ExpresionRegular.IndexOf(']', i);
                    for(int z = i; z <= IndiceFinal; z++)
                    {
                        Resultado += ExpresionRegular[z];
                    }
                    i += IndiceFinal - i;
                }
                else
                {
                    Resultado += ExpresionRegular[i];
                    if(op_Presedecia1.Contains(ExpresionRegular[i]))
                    {
                        if(alfabeto.Contains(ExpresionRegular[i + 1]))
                        {
                            Resultado += "&";
                        } 
                    }
                    else if (alfabeto.Contains(ExpresionRegular[i]) && ExpresionRegular[i+1] == '(')
                    {
                        Resultado += "&";
                    }

                    else if(alfabeto.Contains(ExpresionRegular[i]) && alfabeto.Contains(ExpresionRegular[i + 1]))
                    {
                        Resultado += "&";
                    }
                    else if(ExpresionRegular[i] == ')' && alfabeto.Contains(ExpresionRegular[i + 1]))
                    {
                        Resultado += "&";
                    }
                    else if(alfabeto.Contains(ExpresionRegular[i]) && ExpresionRegular[i + 1] == '[')
                    {
                        Resultado += "&";
                    }
                }
            }
            Resultado += ExpresionRegular[ExpresionRegular.Length - 1];
            //MessageBox.Show("La cadena una vez hecho las concatenaciones pertinentes queda: " + Resultado);
            return Resultado;
        }

        public string CambiaCorchetes(string ExpresionRegular)
        {
            string Resultado = "";
            //MessageBox.Show("Expresion regular sin conversión: " + ExpresionRegular);
            for (int i = 0; i < ExpresionRegular.Length; i++)
            {
                
                if (ExpresionRegular[i] == '[')
                {
                    int IndiceFinal = ExpresionRegular.IndexOf(']', i);
                    if (ExpresionRegular.IndexOf('-', i) < IndiceFinal && ExpresionRegular.IndexOf('-', i) != -1) // En caso de que sea un rango de numeros. Ejemplo[0-5]
                    {
                        int IndiceMedio = ExpresionRegular.IndexOf('-', i);
                        int NumeroDigInicial = ExpresionRegular.IndexOf('-', i) - i;
                        string NumeroInicial = "";
                        for (int z = 1; z < NumeroDigInicial; z++)
                        {
                            NumeroInicial += ExpresionRegular[i + z];
                        }
                        //MessageBox.Show("Numero Inicial: " + NumeroInicial);
                        int ValorInicial = int.Parse(NumeroInicial);
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
                else
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
                return true;
            }
            else
            {
                return false;
            }
        }


        /*
        private String desglosaCorchetes(String infija)
        {
            String res;

            List<int> corchetesIniciales = new List<int>();
            List<int> corchetesFinales = new List<int>();

            Boolean salir = false;
            for (int i = s.IndexOf('['); i > -1; i = s.IndexOf('[', i + 1))
            {
                corchetesIniciales.Add(i);
            }
            for (int i = s.IndexOf(']'); i > -1; i = s.IndexOf(']', i + 1))
            {
                corchetesFinales.Add(i);
            }

            if (corchetesFinales.Count() != corchetesFinales.Count())
            {
                return "";
            }

        }
        */
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
