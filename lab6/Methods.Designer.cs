
namespace lab6
{
    partial class Methods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Methods));
            this.CheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.StartAlgorithms = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CheckedListBox
            // 
            this.CheckedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.CheckedListBox.CheckOnClick = true;
            this.CheckedListBox.FormattingEnabled = true;
            this.CheckedListBox.Items.AddRange(new object[] {
            "Гаусса",
            "Квадратного корня",
            "Прогонки",
            "Простой итерации",
            "Наискорейшего спуска",
            "Сопряженных градиентов"});
            this.CheckedListBox.Location = new System.Drawing.Point(142, 92);
            this.CheckedListBox.Name = "CheckedListBox";
            this.CheckedListBox.Size = new System.Drawing.Size(244, 140);
            this.CheckedListBox.TabIndex = 3;
            // 
            // StartAlgorithms
            // 
            this.StartAlgorithms.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartAlgorithms.Location = new System.Drawing.Point(109, 252);
            this.StartAlgorithms.Name = "StartAlgorithms";
            this.StartAlgorithms.Size = new System.Drawing.Size(303, 53);
            this.StartAlgorithms.TabIndex = 2;
            this.StartAlgorithms.Text = "Продолжить";
            this.StartAlgorithms.UseVisualStyleBackColor = true;
            this.StartAlgorithms.Click += new System.EventHandler(this.StartAlgorithms_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(147, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Выберите методы:";
            // 
            // Methods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(569, 363);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CheckedListBox);
            this.Controls.Add(this.StartAlgorithms);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Methods";
            this.Text = "Выбор методов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectAlgorithmsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CheckedListBox;
        private System.Windows.Forms.Button StartAlgorithms;
        private System.Windows.Forms.Label label1;
    }
}