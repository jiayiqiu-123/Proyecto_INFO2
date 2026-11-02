namespace SimuladorDeWindowsForm
{
    partial class FormCambiarVelocidad
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.tablaVuelos = new System.Windows.Forms.DataGridView();
            this.columnaVuelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaVelocidadActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaNuevaVelocidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAceptar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tablaVuelos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nueva Velocidad";
            // 
            // btnCancelar
            // 
            this.btnCancelar.ForeColor = System.Drawing.Color.Red;
            this.btnCancelar.Location = new System.Drawing.Point(369, 295);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(97, 28);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // tablaVuelos
            // 
            this.tablaVuelos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaVuelos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnaVuelo,
            this.columnaVelocidadActual,
            this.columnaNuevaVelocidad});
            this.tablaVuelos.Location = new System.Drawing.Point(28, 83);
            this.tablaVuelos.Name = "tablaVuelos";
            this.tablaVuelos.RowHeadersWidth = 51;
            this.tablaVuelos.RowTemplate.Height = 24;
            this.tablaVuelos.Size = new System.Drawing.Size(438, 209);
            this.tablaVuelos.TabIndex = 5;
            // 
            // columnaVuelo
            // 
            this.columnaVuelo.HeaderText = "Vuelo";
            this.columnaVuelo.MinimumWidth = 6;
            this.columnaVuelo.Name = "columnaVuelo";
            this.columnaVuelo.Width = 125;
            // 
            // columnaVelocidadActual
            // 
            this.columnaVelocidadActual.HeaderText = "Velocidad Actual";
            this.columnaVelocidadActual.MinimumWidth = 6;
            this.columnaVelocidadActual.Name = "columnaVelocidadActual";
            this.columnaVelocidadActual.Width = 125;
            // 
            // columnaNuevaVelocidad
            // 
            this.columnaNuevaVelocidad.HeaderText = "Nueva Velocidad";
            this.columnaNuevaVelocidad.MinimumWidth = 6;
            this.columnaNuevaVelocidad.Name = "columnaNuevaVelocidad";
            this.columnaNuevaVelocidad.Width = 125;
            // 
            // btnAceptar
            // 
            this.btnAceptar.ForeColor = System.Drawing.Color.Green;
            this.btnAceptar.Location = new System.Drawing.Point(28, 295);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(335, 28);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar cambios";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FormCambiarVelocidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 338);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.tablaVuelos);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label1);
            this.Name = "FormCambiarVelocidad";
            this.Text = "Cambio de Velocidad";
            ((System.ComponentModel.ISupportInitialize)(this.tablaVuelos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView tablaVuelos;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaVuelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaVelocidadActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaNuevaVelocidad;
        private System.Windows.Forms.Button btnAceptar;
    }
}