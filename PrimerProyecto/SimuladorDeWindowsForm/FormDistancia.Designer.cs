namespace SimuladorDeWindowsForm
{
    partial class FormDistancia
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
            this.dgvDistancias = new System.Windows.Forms.DataGridView();
            this.labelTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistancias)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dgvDistancias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistancias.Location = new System.Drawing.Point(34, 50);
            this.dgvDistancias.Name = "dataGridView1";
            this.dgvDistancias.Size = new System.Drawing.Size(386, 220);
            this.dgvDistancias.TabIndex = 1;
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelTitulo.Location = new System.Drawing.Point(31, 30);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(244, 17);
            this.labelTitulo.TabIndex = 2;
            this.labelTitulo.Text = "Distancias con el vuelo seleccionado:";
            // 
            // FormDistancia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 323);
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.dgvDistancias);
            this.Name = "FormDistancia";
            this.Text = "FormDistancia";
            this.Load += new System.EventHandler(this.FormDistancia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistancias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvDistancias;
        private System.Windows.Forms.Label labelTitulo;
    }
}