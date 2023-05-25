namespace Kursovaya
{
    partial class AddWorkerForm
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
            this.ChooseProject = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.NameText = new System.Windows.Forms.TextBox();
            this.AddBut = new System.Windows.Forms.Button();
            this.OutBut = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChooseProject
            // 
            this.ChooseProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChooseProject.FormattingEnabled = true;
            this.ChooseProject.Location = new System.Drawing.Point(69, 173);
            this.ChooseProject.Name = "ChooseProject";
            this.ChooseProject.Size = new System.Drawing.Size(306, 172);
            this.ChooseProject.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 80);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(64, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Добавление сотрудника";
            // 
            // NameText
            // 
            this.NameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameText.Location = new System.Drawing.Point(60, 101);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(306, 26);
            this.NameText.TabIndex = 2;
            this.NameText.Text = "Введите ФИО сотрудника";
            this.NameText.Click += new System.EventHandler(this.Name_Click);
            // 
            // AddBut
            // 
            this.AddBut.Location = new System.Drawing.Point(44, 394);
            this.AddBut.Name = "AddBut";
            this.AddBut.Size = new System.Drawing.Size(111, 45);
            this.AddBut.TabIndex = 3;
            this.AddBut.Text = "Добавить";
            this.AddBut.UseVisualStyleBackColor = true;
            this.AddBut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddBut_MouseClick);
            // 
            // OutBut
            // 
            this.OutBut.Location = new System.Drawing.Point(286, 394);
            this.OutBut.Name = "OutBut";
            this.OutBut.Size = new System.Drawing.Size(110, 45);
            this.OutBut.TabIndex = 4;
            this.OutBut.Text = "Назад";
            this.OutBut.UseVisualStyleBackColor = true;
            this.OutBut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutBut_MouseClick);
            // 
            // AddWorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 477);
            this.Controls.Add(this.OutBut);
            this.Controls.Add(this.AddBut);
            this.Controls.Add(this.NameText);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ChooseProject);
            this.Name = "AddWorkerForm";
            this.Text = "AddWorkerForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.CheckedListBox ChooseProject;
        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.TextBox NameText;
        protected System.Windows.Forms.Button AddBut;
        protected System.Windows.Forms.Button OutBut;
    }
}