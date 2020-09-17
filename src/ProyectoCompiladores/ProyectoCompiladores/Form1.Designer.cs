namespace ProyectoCompiladores
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TabPage tabPage2;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BT_LimpiarTX1 = new System.Windows.Forms.Button();
            this.BT_SubirArchivo1 = new System.Windows.Forms.Button();
            this.inFijaTextBox = new System.Windows.Forms.TextBox();
            this.InToPosBoton = new System.Windows.Forms.Button();
            this.posFijaTextBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BT_LimpiarTXPrep = new System.Windows.Forms.Button();
            this.TB_SubirArchivo = new System.Windows.Forms.TextBox();
            this.BT_SubirArchivoPrep = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            tabPage2 = new System.Windows.Forms.TabPage();
            tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(this.groupBox1);
            tabPage2.Location = new System.Drawing.Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(1177, 485);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "1º avance";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += new System.EventHandler(this.InToPosBoton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BT_LimpiarTX1);
            this.groupBox1.Controls.Add(this.BT_SubirArchivo1);
            this.groupBox1.Controls.Add(this.inFijaTextBox);
            this.groupBox1.Controls.Add(this.InToPosBoton);
            this.groupBox1.Controls.Add(this.posFijaTextBox);
            this.groupBox1.Location = new System.Drawing.Point(15, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 304);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Análisis Léxico";
            // 
            // BT_LimpiarTX1
            // 
            this.BT_LimpiarTX1.Location = new System.Drawing.Point(115, 82);
            this.BT_LimpiarTX1.Name = "BT_LimpiarTX1";
            this.BT_LimpiarTX1.Size = new System.Drawing.Size(109, 40);
            this.BT_LimpiarTX1.TabIndex = 7;
            this.BT_LimpiarTX1.Text = "Limpiar Texto";
            this.BT_LimpiarTX1.UseVisualStyleBackColor = true;
            this.BT_LimpiarTX1.Click += new System.EventHandler(this.BT_LimpiarTX1_Click);
            // 
            // BT_SubirArchivo1
            // 
            this.BT_SubirArchivo1.Location = new System.Drawing.Point(0, 81);
            this.BT_SubirArchivo1.Name = "BT_SubirArchivo1";
            this.BT_SubirArchivo1.Size = new System.Drawing.Size(109, 40);
            this.BT_SubirArchivo1.TabIndex = 6;
            this.BT_SubirArchivo1.Text = "Subir Archivo";
            this.BT_SubirArchivo1.UseVisualStyleBackColor = true;
            this.BT_SubirArchivo1.Click += new System.EventHandler(this.BT_SubirArchivo1_Click);
            // 
            // inFijaTextBox
            // 
            this.inFijaTextBox.Location = new System.Drawing.Point(0, 19);
            this.inFijaTextBox.Multiline = true;
            this.inFijaTextBox.Name = "inFijaTextBox";
            this.inFijaTextBox.Size = new System.Drawing.Size(462, 49);
            this.inFijaTextBox.TabIndex = 3;
            // 
            // InToPosBoton
            // 
            this.InToPosBoton.Location = new System.Drawing.Point(347, 83);
            this.InToPosBoton.Name = "InToPosBoton";
            this.InToPosBoton.Size = new System.Drawing.Size(115, 38);
            this.InToPosBoton.TabIndex = 5;
            this.InToPosBoton.Text = "Convertir a posfija";
            this.InToPosBoton.UseVisualStyleBackColor = true;
            this.InToPosBoton.Click += new System.EventHandler(this.InToPosBoton_Click);
            // 
            // posFijaTextBox
            // 
            this.posFijaTextBox.Location = new System.Drawing.Point(0, 171);
            this.posFijaTextBox.Multiline = true;
            this.posFijaTextBox.Name = "posFijaTextBox";
            this.posFijaTextBox.Size = new System.Drawing.Size(462, 53);
            this.posFijaTextBox.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textBox1.Location = new System.Drawing.Point(16, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1177, 87);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Compiladores e Intérpretes A - Equipo Shakespares\r\nPedro de Jesús Cantú Olivares\r" +
    "\nAxel López Rodríguez";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.BT_LimpiarTXPrep);
            this.tabPage1.Controls.Add(this.TB_SubirArchivo);
            this.tabPage1.Controls.Add(this.BT_SubirArchivoPrep);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1177, 485);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Preparación del Proyecto";
            // 
            // BT_LimpiarTXPrep
            // 
            this.BT_LimpiarTXPrep.Location = new System.Drawing.Point(121, 6);
            this.BT_LimpiarTXPrep.Name = "BT_LimpiarTXPrep";
            this.BT_LimpiarTXPrep.Size = new System.Drawing.Size(109, 40);
            this.BT_LimpiarTXPrep.TabIndex = 2;
            this.BT_LimpiarTXPrep.Text = "Limpiar Texto";
            this.BT_LimpiarTXPrep.UseVisualStyleBackColor = true;
            this.BT_LimpiarTXPrep.Click += new System.EventHandler(this.BT_LimpiarTXPrep_Click);
            // 
            // TB_SubirArchivo
            // 
            this.TB_SubirArchivo.Location = new System.Drawing.Point(6, 52);
            this.TB_SubirArchivo.Multiline = true;
            this.TB_SubirArchivo.Name = "TB_SubirArchivo";
            this.TB_SubirArchivo.ReadOnly = true;
            this.TB_SubirArchivo.Size = new System.Drawing.Size(224, 111);
            this.TB_SubirArchivo.TabIndex = 1;
            // 
            // BT_SubirArchivoPrep
            // 
            this.BT_SubirArchivoPrep.Location = new System.Drawing.Point(6, 6);
            this.BT_SubirArchivoPrep.Name = "BT_SubirArchivoPrep";
            this.BT_SubirArchivoPrep.Size = new System.Drawing.Size(109, 40);
            this.BT_SubirArchivoPrep.TabIndex = 0;
            this.BT_SubirArchivoPrep.Text = "Subir Archivo";
            this.BT_SubirArchivoPrep.UseVisualStyleBackColor = true;
            this.BT_SubirArchivoPrep.Click += new System.EventHandler(this.BT_SubirArchivoPrep_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 106);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1185, 511);
            this.tabControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 629);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "ProyectoCompiladores";
            tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox TB_SubirArchivo;
        private System.Windows.Forms.Button BT_SubirArchivoPrep;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button BT_LimpiarTXPrep;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox inFijaTextBox;
        private System.Windows.Forms.Button InToPosBoton;
        private System.Windows.Forms.TextBox posFijaTextBox;
        private System.Windows.Forms.Button BT_SubirArchivo1;
        private System.Windows.Forms.Button BT_LimpiarTX1;
    }
}

