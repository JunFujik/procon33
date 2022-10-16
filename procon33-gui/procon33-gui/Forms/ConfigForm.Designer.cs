namespace procon33_gui.Forms
{
    partial class ConfigForm
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
            this.SystemURLTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TokenTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ScriptsPathTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PythonCommandTextBox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.PythonArgumentTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.JkspeechPathTextBox = new System.Windows.Forms.TextBox();
            this.UseHttpsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SystemURLTextBox
            // 
            this.SystemURLTextBox.Location = new System.Drawing.Point(258, 12);
            this.SystemURLTextBox.Name = "SystemURLTextBox";
            this.SystemURLTextBox.Size = new System.Drawing.Size(444, 22);
            this.SystemURLTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "競技システムホスト";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(16, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "通信用トークン";
            // 
            // TokenTextBox
            // 
            this.TokenTextBox.Location = new System.Drawing.Point(258, 56);
            this.TokenTextBox.Name = "TokenTextBox";
            this.TokenTextBox.Size = new System.Drawing.Size(444, 22);
            this.TokenTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(16, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "scriptsフォルダパス";
            // 
            // ScriptsPathTextBox
            // 
            this.ScriptsPathTextBox.Location = new System.Drawing.Point(258, 100);
            this.ScriptsPathTextBox.Name = "ScriptsPathTextBox";
            this.ScriptsPathTextBox.Size = new System.Drawing.Size(444, 22);
            this.ScriptsPathTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(16, 206);
            this.label4.Margin = new System.Windows.Forms.Padding(10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Python実行コマンド";
            // 
            // PythonCommandTextBox
            // 
            this.PythonCommandTextBox.Location = new System.Drawing.Point(258, 210);
            this.PythonCommandTextBox.Name = "PythonCommandTextBox";
            this.PythonCommandTextBox.Size = new System.Drawing.Size(444, 22);
            this.PythonCommandTextBox.TabIndex = 6;
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CloseButton.Location = new System.Drawing.Point(12, 507);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(707, 68);
            this.CloseButton.TabIndex = 9;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(16, 250);
            this.label5.Margin = new System.Windows.Forms.Padding(10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Python実行引数";
            // 
            // PythonArgumentTextBox
            // 
            this.PythonArgumentTextBox.Location = new System.Drawing.Point(258, 254);
            this.PythonArgumentTextBox.Name = "PythonArgumentTextBox";
            this.PythonArgumentTextBox.Size = new System.Drawing.Size(444, 22);
            this.PythonArgumentTextBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(16, 140);
            this.label6.Margin = new System.Windows.Forms.Padding(10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "JKspeech フォルダパス";
            // 
            // JkspeechPathTextBox
            // 
            this.JkspeechPathTextBox.Location = new System.Drawing.Point(258, 144);
            this.JkspeechPathTextBox.Name = "JkspeechPathTextBox";
            this.JkspeechPathTextBox.Size = new System.Drawing.Size(444, 22);
            this.JkspeechPathTextBox.TabIndex = 12;
            // 
            // UseHttpsCheckBox
            // 
            this.UseHttpsCheckBox.AutoSize = true;
            this.UseHttpsCheckBox.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UseHttpsCheckBox.Location = new System.Drawing.Point(20, 341);
            this.UseHttpsCheckBox.Name = "UseHttpsCheckBox";
            this.UseHttpsCheckBox.Size = new System.Drawing.Size(208, 28);
            this.UseHttpsCheckBox.TabIndex = 14;
            this.UseHttpsCheckBox.Text = "HTTPSを使用する";
            this.UseHttpsCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 587);
            this.Controls.Add(this.UseHttpsCheckBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.JkspeechPathTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PythonArgumentTextBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PythonCommandTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ScriptsPathTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TokenTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SystemURLTextBox);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SystemURLTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TokenTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ScriptsPathTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PythonCommandTextBox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PythonArgumentTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox JkspeechPathTextBox;
        private System.Windows.Forms.CheckBox UseHttpsCheckBox;
    }
}