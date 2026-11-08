namespace SimuladorDeWindowsForm
{
    partial class FormConflicto
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
            this.btnResolver = new System.Windows.Forms.Button();
            this.btnSinResolver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(107, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(448, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Se ha detectado un conflicto entre los vuelos. ¿Quieres resolverlo?";
            // 
            // btnResolver
            // 
            this.btnResolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnResolver.Location = new System.Drawing.Point(168, 203);
            this.btnResolver.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnResolver.Name = "btnResolver";
            this.btnResolver.Size = new System.Drawing.Size(167, 62);
            this.btnResolver.TabIndex = 1;
            this.btnResolver.Text = "Resolver Conflicto";
            this.btnResolver.UseVisualStyleBackColor = true;
            this.btnResolver.Click += new System.EventHandler(this.btnResolver_Click);
            // 
            // btnSinResolver
            // 
            this.btnSinResolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnSinResolver.Location = new System.Drawing.Point(437, 203);
            this.btnSinResolver.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSinResolver.Name = "btnSinResolver";
            this.btnSinResolver.Size = new System.Drawing.Size(167, 62);
            this.btnSinResolver.TabIndex = 2;
            this.btnSinResolver.Text = "Continuar sin Resolver";
            this.btnSinResolver.UseVisualStyleBackColor = true;
            this.btnSinResolver.Click += new System.EventHandler(this.btnSinResolver_Click);
            // 
            // FormConflicto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSinResolver);
            this.Controls.Add(this.btnResolver);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormConflicto";
            this.Text = "¡ALERTA CONFLICTO!";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResolver;
        private System.Windows.Forms.Button btnSinResolver;
    }
}