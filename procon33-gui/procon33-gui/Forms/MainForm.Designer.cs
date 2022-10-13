namespace procon33_gui.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.MatchInfoButton = new System.Windows.Forms.Button();
            this.ProblemInfoButton = new System.Windows.Forms.Button();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.PostAnswerEhuda = new System.Windows.Forms.DataGridView();
            this.AnswerButton = new System.Windows.Forms.Button();
            this.PreAnswerEhuda = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProblemInfoLabel = new System.Windows.Forms.Label();
            this.MatchInfoLabel = new System.Windows.Forms.Label();
            this.NumChunkNumericBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.GetChunkButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ChunkDataGridView = new System.Windows.Forms.DataGridView();
            this.SoundSeparateButton = new System.Windows.Forms.Button();
            this.CorrelateButton = new System.Windows.Forms.Button();
            this.FlattenCorrelateButton = new System.Windows.Forms.Button();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.PlayDataButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PostAnswerEhuda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreAnswerEhuda)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumChunkNumericBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChunkDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // MatchInfoButton
            // 
            this.MatchInfoButton.Location = new System.Drawing.Point(12, 52);
            this.MatchInfoButton.Name = "MatchInfoButton";
            this.MatchInfoButton.Size = new System.Drawing.Size(281, 34);
            this.MatchInfoButton.TabIndex = 0;
            this.MatchInfoButton.Text = "試合情報取得";
            this.MatchInfoButton.UseVisualStyleBackColor = true;
            this.MatchInfoButton.Click += new System.EventHandler(this.MatchInfoButton_Click);
            // 
            // ProblemInfoButton
            // 
            this.ProblemInfoButton.Location = new System.Drawing.Point(12, 92);
            this.ProblemInfoButton.Name = "ProblemInfoButton";
            this.ProblemInfoButton.Size = new System.Drawing.Size(281, 34);
            this.ProblemInfoButton.TabIndex = 1;
            this.ProblemInfoButton.Text = "問題情報取得";
            this.ProblemInfoButton.UseVisualStyleBackColor = true;
            this.ProblemInfoButton.Click += new System.EventHandler(this.ProblemInfoButton_Click);
            // 
            // ConfigButton
            // 
            this.ConfigButton.Location = new System.Drawing.Point(12, 12);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(281, 34);
            this.ConfigButton.TabIndex = 2;
            this.ConfigButton.Text = "設定";
            this.ConfigButton.UseVisualStyleBackColor = true;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // PostAnswerEhuda
            // 
            this.PostAnswerEhuda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PostAnswerEhuda.Location = new System.Drawing.Point(12, 367);
            this.PostAnswerEhuda.Name = "PostAnswerEhuda";
            this.PostAnswerEhuda.RowHeadersWidth = 51;
            this.PostAnswerEhuda.RowTemplate.Height = 24;
            this.PostAnswerEhuda.Size = new System.Drawing.Size(281, 320);
            this.PostAnswerEhuda.TabIndex = 3;
            // 
            // AnswerButton
            // 
            this.AnswerButton.Location = new System.Drawing.Point(299, 693);
            this.AnswerButton.Name = "AnswerButton";
            this.AnswerButton.Size = new System.Drawing.Size(281, 48);
            this.AnswerButton.TabIndex = 4;
            this.AnswerButton.Text = "選択した絵札で解答";
            this.AnswerButton.UseVisualStyleBackColor = true;
            this.AnswerButton.Click += new System.EventHandler(this.AnswerButton_Click);
            // 
            // PreAnswerEhuda
            // 
            this.PreAnswerEhuda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PreAnswerEhuda.Location = new System.Drawing.Point(299, 367);
            this.PreAnswerEhuda.Name = "PreAnswerEhuda";
            this.PreAnswerEhuda.ReadOnly = true;
            this.PreAnswerEhuda.RowHeadersWidth = 51;
            this.PreAnswerEhuda.RowTemplate.Height = 24;
            this.PreAnswerEhuda.Size = new System.Drawing.Size(281, 320);
            this.PreAnswerEhuda.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 349);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "解答されている絵札";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 349);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "これから解答される絵札";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ProblemInfoLabel);
            this.groupBox1.Controls.Add(this.MatchInfoLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 214);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "試合情報　問題情報";
            // 
            // ProblemInfoLabel
            // 
            this.ProblemInfoLabel.AutoSize = true;
            this.ProblemInfoLabel.Location = new System.Drawing.Point(238, 23);
            this.ProblemInfoLabel.Name = "ProblemInfoLabel";
            this.ProblemInfoLabel.Size = new System.Drawing.Size(196, 15);
            this.ProblemInfoLabel.TabIndex = 1;
            this.ProblemInfoLabel.Text = "まだ問題は開始されていません。";
            this.ProblemInfoLabel.Click += new System.EventHandler(this.label6_Click);
            // 
            // MatchInfoLabel
            // 
            this.MatchInfoLabel.AutoSize = true;
            this.MatchInfoLabel.Location = new System.Drawing.Point(6, 23);
            this.MatchInfoLabel.Name = "MatchInfoLabel";
            this.MatchInfoLabel.Size = new System.Drawing.Size(196, 15);
            this.MatchInfoLabel.TabIndex = 0;
            this.MatchInfoLabel.Text = "まだ試合は開始されていません。";
            // 
            // NumChunkNumericBox
            // 
            this.NumChunkNumericBox.Location = new System.Drawing.Point(1061, 20);
            this.NumChunkNumericBox.Name = "NumChunkNumericBox";
            this.NumChunkNumericBox.Size = new System.Drawing.Size(110, 22);
            this.NumChunkNumericBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(890, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "取得する分割データ数";
            // 
            // GetChunkButton
            // 
            this.GetChunkButton.Location = new System.Drawing.Point(890, 48);
            this.GetChunkButton.Name = "GetChunkButton";
            this.GetChunkButton.Size = new System.Drawing.Size(281, 34);
            this.GetChunkButton.TabIndex = 11;
            this.GetChunkButton.Text = "分割データ取得";
            this.GetChunkButton.UseVisualStyleBackColor = true;
            this.GetChunkButton.Click += new System.EventHandler(this.GetChunkButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(602, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "分割データ";
            // 
            // ChunkDataGridView
            // 
            this.ChunkDataGridView.AllowDrop = true;
            this.ChunkDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChunkDataGridView.Location = new System.Drawing.Point(605, 44);
            this.ChunkDataGridView.Name = "ChunkDataGridView";
            this.ChunkDataGridView.RowHeadersWidth = 51;
            this.ChunkDataGridView.RowTemplate.Height = 24;
            this.ChunkDataGridView.Size = new System.Drawing.Size(281, 643);
            this.ChunkDataGridView.TabIndex = 12;
            this.ChunkDataGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ChunkDataGridView_DragDrop);
            this.ChunkDataGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ChunkDataGridView_DragEnter);
            // 
            // SoundSeparateButton
            // 
            this.SoundSeparateButton.Location = new System.Drawing.Point(890, 108);
            this.SoundSeparateButton.Name = "SoundSeparateButton";
            this.SoundSeparateButton.Size = new System.Drawing.Size(281, 34);
            this.SoundSeparateButton.TabIndex = 14;
            this.SoundSeparateButton.Text = "音源分離する";
            this.SoundSeparateButton.UseVisualStyleBackColor = true;
            this.SoundSeparateButton.Click += new System.EventHandler(this.SoundSeparateButton_Click);
            // 
            // CorrelateButton
            // 
            this.CorrelateButton.Location = new System.Drawing.Point(890, 148);
            this.CorrelateButton.Name = "CorrelateButton";
            this.CorrelateButton.Size = new System.Drawing.Size(281, 34);
            this.CorrelateButton.TabIndex = 15;
            this.CorrelateButton.Text = "相互相関関数をとる";
            this.CorrelateButton.UseVisualStyleBackColor = true;
            this.CorrelateButton.Click += new System.EventHandler(this.CorrelateButton_Click);
            // 
            // FlattenCorrelateButton
            // 
            this.FlattenCorrelateButton.Location = new System.Drawing.Point(890, 188);
            this.FlattenCorrelateButton.Name = "FlattenCorrelateButton";
            this.FlattenCorrelateButton.Size = new System.Drawing.Size(281, 34);
            this.FlattenCorrelateButton.TabIndex = 16;
            this.FlattenCorrelateButton.Text = "相互相関関数をとる(flatten)";
            this.FlattenCorrelateButton.UseVisualStyleBackColor = true;
            this.FlattenCorrelateButton.Click += new System.EventHandler(this.FlattenCorrelateButton_Click);
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Location = new System.Drawing.Point(299, 111);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(76, 15);
            this.TimerLabel.TabIndex = 17;
            this.TimerLabel.Text = "TimerLabel";
            // 
            // PlayDataButton
            // 
            this.PlayDataButton.Location = new System.Drawing.Point(893, 261);
            this.PlayDataButton.Name = "PlayDataButton";
            this.PlayDataButton.Size = new System.Drawing.Size(281, 34);
            this.PlayDataButton.TabIndex = 18;
            this.PlayDataButton.Text = "分割データを再生する";
            this.PlayDataButton.UseVisualStyleBackColor = true;
            this.PlayDataButton.Click += new System.EventHandler(this.PlayDataButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 753);
            this.Controls.Add(this.PlayDataButton);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.FlattenCorrelateButton);
            this.Controls.Add(this.CorrelateButton);
            this.Controls.Add(this.SoundSeparateButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ChunkDataGridView);
            this.Controls.Add(this.GetChunkButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NumChunkNumericBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PreAnswerEhuda);
            this.Controls.Add(this.AnswerButton);
            this.Controls.Add(this.PostAnswerEhuda);
            this.Controls.Add(this.ConfigButton);
            this.Controls.Add(this.ProblemInfoButton);
            this.Controls.Add(this.MatchInfoButton);
            this.Name = "MainForm";
            this.Text = "都立(品川)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PostAnswerEhuda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreAnswerEhuda)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumChunkNumericBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChunkDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button MatchInfoButton;
        private System.Windows.Forms.Button ProblemInfoButton;
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.DataGridView PostAnswerEhuda;
        private System.Windows.Forms.Button AnswerButton;
        private System.Windows.Forms.DataGridView PreAnswerEhuda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown NumChunkNumericBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button GetChunkButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView ChunkDataGridView;
        private System.Windows.Forms.Button SoundSeparateButton;
        private System.Windows.Forms.Button CorrelateButton;
        private System.Windows.Forms.Button FlattenCorrelateButton;
        private System.Windows.Forms.Label ProblemInfoLabel;
        private System.Windows.Forms.Label MatchInfoLabel;
        private System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.Button PlayDataButton;
    }
}

