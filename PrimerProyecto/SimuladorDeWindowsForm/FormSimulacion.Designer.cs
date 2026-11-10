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
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.btnCambiarVelocidad = new System.Windows.Forms.Button();
            this.GuardarFlightPlanList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelEspacioAereo
            // 
            this.panelEspacioAereo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelEspacioAereo.Location = new System.Drawing.Point(332, 70);
            this.panelEspacioAereo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelEspacioAereo.Name = "panelEspacioAereo";
            this.panelEspacioAereo.Size = new System.Drawing.Size(914, 628);
            this.panelEspacioAereo.TabIndex = 1;
            this.panelEspacioAereo.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnMover
            // 
            this.btnMover.Location = new System.Drawing.Point(74, 79);
            this.btnMover.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMover.Name = "btnMover";
            this.btnMover.Size = new System.Drawing.Size(177, 88);
            this.btnMover.TabIndex = 2;
            this.btnMover.Text = "Mover un ciclo";
            this.btnMover.UseVisualStyleBackColor = true;
            this.btnMover.Click += new System.EventHandler(this.btnMover_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "(0，0)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 680);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "(0, 550)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1190, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "(800, 0)";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnSimulacionAutomatica
            // 
            this.btnSimulacionAutomatica.Location = new System.Drawing.Point(74, 205);
            this.btnSimulacionAutomatica.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSimulacionAutomatica.Name = "btnSimulacionAutomatica";
            this.btnSimulacionAutomatica.Size = new System.Drawing.Size(177, 84);
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
            this.buttonVerTablaVuelos.Location = new System.Drawing.Point(74, 321);
            this.buttonVerTablaVuelos.Margin = new System.Windows.Forms.Padding(4);
            this.buttonVerTablaVuelos.Name = "buttonVerTablaVuelos";
            this.buttonVerTablaVuelos.Size = new System.Drawing.Size(177, 68);
            this.buttonVerTablaVuelos.TabIndex = 7;
            this.buttonVerTablaVuelos.Text = "Tabla vuelos";
            this.buttonVerTablaVuelos.UseVisualStyleBackColor = true;
            this.buttonVerTablaVuelos.Click += new System.EventHandler(this.buttonVerTablaVuelos_Click);
            // 
            // buttonPrediccion
            // 
            this.buttonPrediccion.Location = new System.Drawing.Point(74, 416);
            this.buttonPrediccion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPrediccion.Name = "buttonPrediccion";
            this.buttonPrediccion.Size = new System.Drawing.Size(177, 55);
            this.buttonPrediccion.TabIndex = 8;
            this.buttonPrediccion.Text = "Hay Conflicto?";
            this.buttonPrediccion.UseVisualStyleBackColor = true;
            this.buttonPrediccion.Click += new System.EventHandler(this.buttonPrediccion_Click);
            // 
            // btnReiniciar
            // 
            this.btnReiniciar.Location = new System.Drawing.Point(987, 35);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(197, 32);
            this.btnReiniciar.TabIndex = 9;
            this.btnReiniciar.Text = "Reiniciar Simulación";
            this.btnReiniciar.UseVisualStyleBackColor = true;
            this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);
            // 
            // btnCambiarVelocidad
            // 
            this.btnCambiarVelocidad.Location = new System.Drawing.Point(775, 36);
            this.btnCambiarVelocidad.Name = "btnCambiarVelocidad";
            this.btnCambiarVelocidad.Size = new System.Drawing.Size(192, 29);
            this.btnCambiarVelocidad.TabIndex = 10;
            this.btnCambiarVelocidad.Text = "Cambiar Velocidad";
            this.btnCambiarVelocidad.UseVisualStyleBackColor = true;
            this.btnCambiarVelocidad.Click += new System.EventHandler(this.btnCambiarVelocidad_Click);
            // 
            // GuardarFlightPlanList
            // 
            this.GuardarFlightPlanList.Location = new System.Drawing.Point(74, 511);
            this.GuardarFlightPlanList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GuardarFlightPlanList.Name = "GuardarFlightPlanList";
            this.GuardarFlightPlanList.Size = new System.Drawing.Size(177, 55);
            this.GuardarFlightPlanList.TabIndex = 11;
            this.GuardarFlightPlanList.Text = "Guardar Flight Plan List en txt";
            this.GuardarFlightPlanList.UseVisualStyleBackColor = true;
            this.GuardarFlightPlanList.Click += new System.EventHandler(this.GuardarFlightPlanList_Click);
            // 
            // FormSimulacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.GuardarFlightPlanList);
            this.Controls.Add(this.btnCambiarVelocidad);
            this.Controls.Add(this.btnReiniciar);
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
        private System.Windows.Forms.Button btnReiniciar;
        private System.Windows.Forms.Button btnCambiarVelocidad;
        private System.Windows.Forms.Button GuardarFlightPlanList;
    }
}