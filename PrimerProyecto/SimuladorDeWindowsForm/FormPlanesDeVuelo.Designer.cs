namespace SimuladorDeWindowsForm
{
    partial class FormPlanesDeVuelo
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
            this.txtid1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtx1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtx1F = new System.Windows.Forms.TextBox();
            this.txty1 = new System.Windows.Forms.TextBox();
            this.txty1F = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVel1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtCompany1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtid1
            // 
            this.txtid1.Location = new System.Drawing.Point(226, 150);
            this.txtid1.Margin = new System.Windows.Forms.Padding(4);
            this.txtid1.Name = "txtid1";
            this.txtid1.Size = new System.Drawing.Size(148, 28);
            this.txtid1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Introducir los datos del plan de vuelo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(748, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 18);
            this.label3.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 154);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "Identificador";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 233);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "Coordenadas iniciales";
            // 
            // txtx1
            // 
            this.txtx1.Location = new System.Drawing.Point(226, 233);
            this.txtx1.Margin = new System.Windows.Forms.Padding(4);
            this.txtx1.Name = "txtx1";
            this.txtx1.Size = new System.Drawing.Size(66, 28);
            this.txtx1.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 274);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 18);
            this.label6.TabIndex = 14;
            this.label6.Text = "Coordenadas finales";
            // 
            // txtx1F
            // 
            this.txtx1F.Location = new System.Drawing.Point(226, 274);
            this.txtx1F.Margin = new System.Windows.Forms.Padding(4);
            this.txtx1F.Name = "txtx1F";
            this.txtx1F.Size = new System.Drawing.Size(66, 28);
            this.txtx1F.TabIndex = 13;
            // 
            // txty1
            // 
            this.txty1.Location = new System.Drawing.Point(309, 233);
            this.txty1.Margin = new System.Windows.Forms.Padding(4);
            this.txty1.Name = "txty1";
            this.txty1.Size = new System.Drawing.Size(66, 28);
            this.txty1.TabIndex = 19;
            // 
            // txty1F
            // 
            this.txty1F.Location = new System.Drawing.Point(309, 274);
            this.txty1F.Margin = new System.Windows.Forms.Padding(4);
            this.txty1F.Name = "txty1F";
            this.txty1F.Size = new System.Drawing.Size(66, 28);
            this.txty1F.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 319);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "Velocidad";
            // 
            // txtVel1
            // 
            this.txtVel1.Location = new System.Drawing.Point(226, 319);
            this.txtVel1.Margin = new System.Windows.Forms.Padding(4);
            this.txtVel1.Name = "txtVel1";
            this.txtVel1.Size = new System.Drawing.Size(148, 28);
            this.txtVel1.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(180, 414);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 32);
            this.button1.TabIndex = 33;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Agregar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(309, 414);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 32);
            this.button2.TabIndex = 34;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // txtCompany1
            // 
            this.txtCompany1.Location = new System.Drawing.Point(226, 194);
            this.txtCompany1.Name = "txtCompany1";
            this.txtCompany1.Size = new System.Drawing.Size(148, 28);
            this.txtCompany1.TabIndex = 35;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(136, 194);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 18);
            this.label11.TabIndex = 36;
            this.label11.Text = "Compañía";
            // 
            // FormPlanesDeVuelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 608);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtCompany1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtVel1);
            this.Controls.Add(this.txty1F);
            this.Controls.Add(this.txty1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtx1F);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtx1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtid1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormPlanesDeVuelo";
            this.Text = "Planes de vuelo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtid1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtx1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtx1F;
        private System.Windows.Forms.TextBox txty1;
        private System.Windows.Forms.TextBox txty1F;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtCompany1;
        private System.Windows.Forms.Label label11;
    }
}

