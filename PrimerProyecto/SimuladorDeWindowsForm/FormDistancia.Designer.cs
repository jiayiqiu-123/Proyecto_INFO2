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
            this.labelDistancia = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDistancia
            // 
            this.labelDistancia.AutoSize = true;
            this.labelDistancia.Location = new System.Drawing.Point(31, 34);
            this.labelDistancia.Name = "labelDistancia";
            this.labelDistancia.Size = new System.Drawing.Size(35, 13);
            this.labelDistancia.TabIndex = 0;
            this.labelDistancia.Text = "label1";
            this.labelDistancia.Click += new System.EventHandler(this.labelDistancia_Click);
            // 
            // FormDistancia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 85);
            this.Controls.Add(this.labelDistancia);
            this.Name = "FormDistancia";
            this.Text = "FormDistancia";
            this.Load += new System.EventHandler(this.FormDistancia_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelDistancia;
    }
}