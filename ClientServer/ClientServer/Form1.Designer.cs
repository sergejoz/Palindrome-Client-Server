namespace ClientServer
{
    partial class ClientForm
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
            this.ClientCommandBox = new System.Windows.Forms.TextBox();
            this.ServerAnswerBox = new System.Windows.Forms.TextBox();
            this.ClientSendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ClientCommandBox
            // 
            this.ClientCommandBox.Location = new System.Drawing.Point(12, 37);
            this.ClientCommandBox.Multiline = true;
            this.ClientCommandBox.Name = "ClientCommandBox";
            this.ClientCommandBox.Size = new System.Drawing.Size(535, 84);
            this.ClientCommandBox.TabIndex = 0;
            this.ClientCommandBox.TextChanged += new System.EventHandler(this.ClientCommandBox_TextChanged);
            // 
            // ServerAnswerBox
            // 
            this.ServerAnswerBox.Location = new System.Drawing.Point(12, 168);
            this.ServerAnswerBox.Multiline = true;
            this.ServerAnswerBox.Name = "ServerAnswerBox";
            this.ServerAnswerBox.ReadOnly = true;
            this.ServerAnswerBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ServerAnswerBox.Size = new System.Drawing.Size(535, 279);
            this.ServerAnswerBox.TabIndex = 1;
            // 
            // ClientSendButton
            // 
            this.ClientSendButton.Enabled = false;
            this.ClientSendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientSendButton.Location = new System.Drawing.Point(413, 127);
            this.ClientSendButton.Name = "ClientSendButton";
            this.ClientSendButton.Size = new System.Drawing.Size(134, 35);
            this.ClientSendButton.TabIndex = 2;
            this.ClientSendButton.Text = "Отправить";
            this.ClientSendButton.UseVisualStyleBackColor = true;
            this.ClientSendButton.Click += new System.EventHandler(this.ClientSendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ответ сервера:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(10, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "Создать файлы";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(203, 468);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 31);
            this.textBox1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Введите путь до директории:";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 457);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClientSendButton);
            this.Controls.Add(this.ServerAnswerBox);
            this.Controls.Add(this.ClientCommandBox);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ClientCommandBox;
        private System.Windows.Forms.TextBox ServerAnswerBox;
        private System.Windows.Forms.Button ClientSendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}

