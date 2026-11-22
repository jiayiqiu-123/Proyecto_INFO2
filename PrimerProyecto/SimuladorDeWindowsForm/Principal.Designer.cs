namespace SimuladorDeWindowsForm
{
    partial class Principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPlanesDeVueloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parámetrosDeSeguridadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espacioAéreoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargarPlanesDeVueloDesdeFicheroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPruebas = new System.Windows.Forms.Button();
            this.intrucciones = new System.Windows.Forms.Label();
            this.título_intrucciones = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1067, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPlanesDeVueloToolStripMenuItem,
            this.parámetrosDeSeguridadToolStripMenuItem,
            this.espacioAéreoToolStripMenuItem,
            this.cargarPlanesDeVueloDesdeFicheroToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // menuPlanesDeVueloToolStripMenuItem
            // 
            this.menuPlanesDeVueloToolStripMenuItem.Name = "menuPlanesDeVueloToolStripMenuItem";
            this.menuPlanesDeVueloToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.menuPlanesDeVueloToolStripMenuItem.Text = "Menu planes de vuelo";
            this.menuPlanesDeVueloToolStripMenuItem.Click += new System.EventHandler(this.menuPlanesDeVueloToolStripMenuItem_Click);
            // 
            // parámetrosDeSeguridadToolStripMenuItem
            // 
            this.parámetrosDeSeguridadToolStripMenuItem.Name = "parámetrosDeSeguridadToolStripMenuItem";
            this.parámetrosDeSeguridadToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.parámetrosDeSeguridadToolStripMenuItem.Text = "Parámetros de seguridad";
            this.parámetrosDeSeguridadToolStripMenuItem.Click += new System.EventHandler(this.parámetrosDeSeguridadToolStripMenuItem_Click);
            // 
            // espacioAéreoToolStripMenuItem
            // 
            this.espacioAéreoToolStripMenuItem.Name = "espacioAéreoToolStripMenuItem";
            this.espacioAéreoToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.espacioAéreoToolStripMenuItem.Text = "Espacio aéreo";
            this.espacioAéreoToolStripMenuItem.Click += new System.EventHandler(this.espacioAéreoToolStripMenuItem_Click);
            // 
            // cargarPlanesDeVueloDesdeFicheroToolStripMenuItem
            // 
            this.cargarPlanesDeVueloDesdeFicheroToolStripMenuItem.Name = "cargarPlanesDeVueloDesdeFicheroToolStripMenuItem";
            this.cargarPlanesDeVueloDesdeFicheroToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.cargarPlanesDeVueloDesdeFicheroToolStripMenuItem.Text = "Cargar simulación desde fichero";
            this.cargarPlanesDeVueloDesdeFicheroToolStripMenuItem.Click += new System.EventHandler(this.cargarPlanesDeVueloDesdeFicheroToolStripMenuItem_Click);
            // 
            // btnPruebas
            // 
            this.btnPruebas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnPruebas.ForeColor = System.Drawing.Color.DeepPink;
            this.btnPruebas.Location = new System.Drawing.Point(397, 324);
            this.btnPruebas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPruebas.Name = "btnPruebas";
            this.btnPruebas.Size = new System.Drawing.Size(247, 96);
            this.btnPruebas.TabIndex = 1;
            this.btnPruebas.Text = "AUTOCOMPLETAR (solo pruebas)";
            this.btnPruebas.UseVisualStyleBackColor = true;
            this.btnPruebas.Click += new System.EventHandler(this.btnPruebas_Click);
            // 
            // intrucciones
            // 
            this.intrucciones.AutoSize = true;
            this.intrucciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.intrucciones.Location = new System.Drawing.Point(227, 157);
            this.intrucciones.Name = "intrucciones";
            this.intrucciones.Size = new System.Drawing.Size(576, 119);
            this.intrucciones.TabIndex = 2;
            this.intrucciones.Text = resources.GetString("intrucciones.Text");
            this.intrucciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // título_intrucciones
            // 
            this.título_intrucciones.AutoSize = true;
            this.título_intrucciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.título_intrucciones.Location = new System.Drawing.Point(413, 110);
            this.título_intrucciones.Name = "título_intrucciones";
            this.título_intrucciones.Size = new System.Drawing.Size(175, 24);
            this.título_intrucciones.TabIndex = 3;
            this.título_intrucciones.Text = "INSTRUCCIONES";
            this.título_intrucciones.Click += new System.EventHandler(this.label1_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.título_intrucciones);
            this.Controls.Add(this.intrucciones);
            this.Controls.Add(this.btnPruebas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Principal";
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuPlanesDeVueloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parámetrosDeSeguridadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espacioAéreoToolStripMenuItem;
        private System.Windows.Forms.Button btnPruebas;
        private System.Windows.Forms.ToolStripMenuItem cargarPlanesDeVueloDesdeFicheroToolStripMenuItem;
        private System.Windows.Forms.Label intrucciones;
        private System.Windows.Forms.Label título_intrucciones;
    }
}