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

namespace ProyectoCompiladores
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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
        /*
         * 
         * while ( no ocurra un error && no sea fin de la expresión infija )
{
 switch ( carácter )
 {
 Paréntesis izquierdo:
 Insertar en la pila;
 break;
 Paréntesis derecho:
 Extraer de la pila y desplegar en posfija hasta encontrar “paréntesis izquierdo” (no desplegarlo);
 break;
 Operando:
 Desplegar en posfija.
 break;
 Operador:
 band = true;
 while ( band )
 {
 if ( la pila está vacía ||
 el tope de la pila es un “paréntesis izquierdo” ||
 el operador tiene mayor prioridad que el tope de la pila )
 {
 Insertar el operador en la pila.
 band = false;
 }
 else
 Extraer el tope de la pila y desplegar en posfija.
}
 break;
 }
 Apuntar al siguiente carácter de la expresión infija.
}
Extraer y desplegar en posfija los elementos de la pila hasta que se vacíe.
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

         * */
        private void InToPosBoton_Click(object sender, EventArgs e)
        {
            String alfabeto = "abcdefghijklmnñopqrstuvxyz0123456789";
            String operadoresUnarios = "*"
            String operadoresBinarios = "|&"
            String infija = inFijaTextBox.Text;
            //variables de control para el primer while
            Boolean error = false;
            int contadorInfija = 0;

            while (!error && contadorInfija <= infija.Length)
            {
                String caracter = infija.Substring(contadorInfija, 1);
                switch (caracter)
                {
                    case "(":
                        //
                        break;
                    case ")":
                        //
                        break;
                    default:
                        if(alfabeto.Contains(caracter)|| )
                        {
                           
                        }
                        else//se introdujo algun caracter invalido.
                        {
                            error = true;
                        }
                        break;
                   

                }
                Boolean bandera = true
                while(bandera)
                {

                }
                    

            }
        }

    }
}
