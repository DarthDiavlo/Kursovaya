namespace Kursovaya
{
    partial class registrationForm
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
            this.Password = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.check = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OutBut = new System.Windows.Forms.Button();
            this.RegistrationBut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(160, 271);
            this.Password.Multiline = true;
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(293, 30);
            this.Password.TabIndex = 1;
            this.Password.Text = "Введите пароль\r\n";
            this.Password.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Password_MouseClick);
            // 
            // Login
            // 
            this.Login.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Login.Location = new System.Drawing.Point(160, 167);
            this.Login.Multiline = true;
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(293, 30);
            this.Login.TabIndex = 0;
            this.Login.Text = "Введите логин\r\n";
            this.Login.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Login_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.check);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.OutBut);
            this.panel1.Controls.Add(this.RegistrationBut);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Password);
            this.panel1.Controls.Add(this.Login);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 524);
            this.panel1.TabIndex = 1;
            // 
            // check
            // 
            this.check.AutoSize = true;
            this.check.Location = new System.Drawing.Point(236, 135);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(148, 16);
            this.check.TabIndex = 9;
            this.check.Text = "Такой логин уже есть";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 374);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            // 
            // OutBut
            // 
            this.OutBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OutBut.Location = new System.Drawing.Point(318, 444);
            this.OutBut.Name = "OutBut";
            this.OutBut.Size = new System.Drawing.Size(173, 41);
            this.OutBut.TabIndex = 7;
            this.OutBut.Text = "Назад";
            this.OutBut.UseVisualStyleBackColor = true;
            this.OutBut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutBut_MouseClick);
            // 
            // RegistrationBut
            // 
            this.RegistrationBut.BackColor = System.Drawing.SystemColors.Highlight;
            this.RegistrationBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RegistrationBut.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.RegistrationBut.FlatAppearance.BorderSize = 0;
            this.RegistrationBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RegistrationBut.Location = new System.Drawing.Point(86, 444);
            this.RegistrationBut.Name = "RegistrationBut";
            this.RegistrationBut.Size = new System.Drawing.Size(173, 41);
            this.RegistrationBut.TabIndex = 6;
            this.RegistrationBut.Text = "Регистрация";
            this.RegistrationBut.UseVisualStyleBackColor = false;
            this.RegistrationBut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RegistrationBut_MouseClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(607, 55);
            this.label1.TabIndex = 2;
            this.label1.Text = "Регистрация";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // registrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 524);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "registrationForm";
            this.Text = "registrationForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button OutBut;
        private System.Windows.Forms.Button RegistrationBut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label check;
    }
}