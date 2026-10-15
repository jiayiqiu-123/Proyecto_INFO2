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
            this.SuspendLayout();
            // 
            // panelEspacioAereo
            // 
            this.panelEspacioAereo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelEspacioAereo.Location = new System.Drawing.Point(331, 70);
            this.panelEspacioAereo.Name = "panelEspacioAereo";
            this.panelEspacioAereo.Size = new System.Drawing.Size(914, 628);
            this.panelEspacioAereo.TabIndex = 1;
            this.panelEspacioAereo.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnMover
            // 
            this.btnMover.Location = new System.Drawing.Point(74, 137);
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
            this.label1.Location = new System.Drawing.Point(263, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "(0，0)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 680);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "(0, 400)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1165, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "(600, 0)";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnSimulacionAutomatica
            // 
            this.btnSimulacionAutomatica.Location = new System.Drawing.Point(74, 270);
            this.btnSimulacionAutomatica.Name = "btnSimulacionAutomatica";
            this.btnSimulacionAutomatica.Size = new System.Drawing.Size(177, 85);
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
            // FormSimulacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1388, 781);
            this.Controls.Add(this.btnSimulacionAutomatica);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMover);
            this.Controls.Add(this.panelEspacioAereo);
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
    }
}