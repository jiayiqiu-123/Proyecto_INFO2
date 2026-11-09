namespace SimuladorDeWindowsForm
{
    partial class FormUser
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
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonSignIn = new System.Windows.Forms.Button();
            this.buttonSignUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxUsername.Location = new System.Drawing.Point(165, 138);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(484, 22);
            this.textBoxUsername.TabIndex = 0;
            this.textBoxUsername.Text = "Introduce Username";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(162, 119);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(70, 16);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxPassword.Location = new System.Drawing.Point(165, 193);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(484, 22);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.Text = "Introduce Password";
            // 
            // buttonSignIn
            // 
            this.buttonSignIn.Location = new System.Drawing.Point(165, 251);
            this.buttonSignIn.Name = "buttonSignIn";
            this.buttonSignIn.Size = new System.Drawing.Size(175, 48);
            this.buttonSignIn.TabIndex = 4;
            this.buttonSignIn.Text = "Sign in";
            this.buttonSignIn.UseVisualStyleBackColor = true;
            // 
            // buttonSignUp
            // 
            this.buttonSignUp.Location = new System.Drawing.Point(474, 251);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(175, 48);
            this.buttonSignUp.TabIndex = 5;
            this.buttonSignUp.Text = "Sign up";
            this.buttonSignUp.UseVisualStyleBackColor = true;
            // 
            // FormUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSignUp);
            this.Controls.Add(this.buttonSignIn);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.textBoxUsername);
            this.Name = "FormUser";
            this.Text = "FormUser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonSignIn;
        private System.Windows.Forms.Button buttonSignUp;
    }
}