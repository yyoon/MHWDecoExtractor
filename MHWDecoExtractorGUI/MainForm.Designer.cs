namespace MHWDecoExtractorGUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxSteamUsers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCharacters = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewDecorations = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonClipboard = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxSteamUsers
            // 
            this.comboBoxSteamUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSteamUsers.FormattingEnabled = true;
            this.comboBoxSteamUsers.Location = new System.Drawing.Point(143, 12);
            this.comboBoxSteamUsers.Name = "comboBoxSteamUsers";
            this.comboBoxSteamUsers.Size = new System.Drawing.Size(121, 23);
            this.comboBoxSteamUsers.TabIndex = 1;
            this.comboBoxSteamUsers.SelectedValueChanged += new System.EventHandler(this.ComboBoxSteamUsers_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "스팀 유저 ID";
            // 
            // comboBoxCharacters
            // 
            this.comboBoxCharacters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacters.FormattingEnabled = true;
            this.comboBoxCharacters.Location = new System.Drawing.Point(143, 41);
            this.comboBoxCharacters.Name = "comboBoxCharacters";
            this.comboBoxCharacters.Size = new System.Drawing.Size(121, 23);
            this.comboBoxCharacters.TabIndex = 3;
            this.comboBoxCharacters.SelectedValueChanged += new System.EventHandler(this.ComboBoxCharacters_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "몬헌 캐릭터";
            // 
            // listViewDecorations
            // 
            this.listViewDecorations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDecorations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewDecorations.HideSelection = false;
            this.listViewDecorations.Location = new System.Drawing.Point(12, 70);
            this.listViewDecorations.Name = "listViewDecorations";
            this.listViewDecorations.Size = new System.Drawing.Size(252, 312);
            this.listViewDecorations.TabIndex = 4;
            this.listViewDecorations.UseCompatibleStateImageBehavior = false;
            this.listViewDecorations.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "장식주";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "소지 수";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.buttonReload, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonClipboard, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 388);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(252, 50);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // buttonReload
            // 
            this.buttonReload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReload.Location = new System.Drawing.Point(0, 0);
            this.buttonReload.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(123, 50);
            this.buttonReload.TabIndex = 0;
            this.buttonReload.Text = "리로드";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.ButtonReload_Click);
            // 
            // buttonClipboard
            // 
            this.buttonClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClipboard.Location = new System.Drawing.Point(129, 0);
            this.buttonClipboard.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.buttonClipboard.Name = "buttonClipboard";
            this.buttonClipboard.Size = new System.Drawing.Size(123, 50);
            this.buttonClipboard.TabIndex = 1;
            this.buttonClipboard.Text = "클립보드에 복사";
            this.buttonClipboard.UseVisualStyleBackColor = true;
            this.buttonClipboard.Click += new System.EventHandler(this.ButtonClipboard_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.listViewDecorations);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxCharacters);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSteamUsers);
            this.MinimumSize = new System.Drawing.Size(292, 489);
            this.Name = "MainForm";
            this.Text = "보유 장식주 추출기";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSteamUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCharacters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewDecorations;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Button buttonClipboard;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}

