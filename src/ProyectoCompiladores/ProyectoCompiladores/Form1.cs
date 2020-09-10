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
    }
}
