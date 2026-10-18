namespace SimuladorDeWindowsForm
{
    partial class FormSimulacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelEspacioAereo = new System.Windows.Forms.Panel();
            this.btnMover = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSimulacionAutomatica = new System.Windows.Forms.Button();
            this.timerSimulacion = new System.Windows.Forms.Timer(this.components);
            this.buttonVerTablaVuelos = new System.Windows.Forms.Button();
            this.buttonPrediccion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelEspacioAereo
            // 
            this.panelEspacioAereo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelEspacioAereo.Location = new System.Drawing.Point(295, 62);
            this.panelEspacioAereo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelEspacioAereo.Name = "panelEspacioAereo";
            this.panelEspacioAereo.Size = new System.Drawing.Size(801, 551);
            this.panelEspacioAereo.TabIndex = 1;
            this.panelEspacioAereo.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnMover
            // 
            this.btnMover.Location = new System.Drawing.Point(66, 70);
            this.btnMover.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMover.Name = "btnMover";
            this.btnMover.Size = new System.Drawing.Size(157, 78);
            this.btnMover.TabIndex = 2;
            this.btnMover.Text = "Mover un ciclo";
            this.btnMover.UseVisualStyleBackColor = true;
            this.btnMover.Click += new System.EventHandler(this.btnMover_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(233, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "(0，0)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 604);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "(0, 550)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1036, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "(800, 0)";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnSimulacionAutomatica
            // 
            this.btnSimulacionAutomatica.Location = new System.Drawing.Point(66, 182);
            this.btnSimulacionAutomatica.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSimulacionAutomatica.Name = "btnSimulacionAutomatica";
            this.btnSimulacionAutomatica.Size = new System.Drawing.Size(157, 75);
            this.btnSimulacionAutomatica.TabIndex = 6;
            this.btnSimulacionAutomatica.Text = "Iniciar simulación automática";
            this.btnSimulacionAutomatica.UseVisualStyleBackColor = true;
            this.btnSimulacionAutomatica.Click += new System.EventHandler(this.btnIniciarAutomatico_Click);
            // 
            // timerSimulacion
            // 
            this.timerSimulacion.Interval = 1000;
            this.timerSimulacion.Tick += new System.EventHandler(this.timerSimulacion_Tick);
            // 
            // buttonVerTablaVuelos
            // 
            this.buttonVerTablaVuelos.Location = new System.Drawing.Point(66, 285);
            this.buttonVerTablaVuelos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonVerTablaVuelos.Name = "buttonVerTablaVuelos";
            this.buttonVerTablaVuelos.Size = new System.Drawing.Size(157, 60);
            this.buttonVerTablaVuelos.TabIndex = 7;
            this.buttonVerTablaVuelos.Text = "Tabla vuelos";
            this.buttonVerTablaVuelos.UseVisualStyleBackColor = true;
            this.buttonVerTablaVuelos.Click += new System.EventHandler(this.buttonVerTablaVuelos_Click);
            // 
            // buttonPrediccion
            // 
            this.buttonPrediccion.Location = new System.Drawing.Point(66, 370);
            this.buttonPrediccion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPrediccion.Name = "buttonPrediccion";
            this.buttonPrediccion.Size = new System.Drawing.Size(157, 49);
            this.buttonPrediccion.TabIndex = 8;
            this.buttonPrediccion.Text = "Hay Conflicto?";
            this.buttonPrediccion.UseVisualStyleBackColor = true;
            this.buttonPrediccion.Click += new System.EventHandler(this.buttonPrediccion_Click);
            // 
            // FormSimulacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 666);
            this.Controls.Add(this.buttonPrediccion);
            this.Controls.Add(this.buttonVerTablaVuelos);
            this.Controls.Add(this.btnSimulacionAutomatica);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMover);
            this.Controls.Add(this.panelEspacioAereo);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormSimulacion";
            this.Text = "FormSimulacion";
            this.Load += new System.EventHandler(this.FormSimulacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelEspacioAereo;
        private System.Windows.Forms.Button btnMover;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSimulacionAutomatica;
        private System.Windows.Forms.Timer timerSimulacion;
        private System.Windows.Forms.Button buttonVerTablaVuelos;
        private System.Windows.Forms.Button buttonPrediccion;
    }
}