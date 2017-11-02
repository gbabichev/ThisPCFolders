namespace ThisPCFolders
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.docBox = new System.Windows.Forms.CheckBox();
            this.picBox = new System.Windows.Forms.CheckBox();
            this.vidBox = new System.Windows.Forms.CheckBox();
            this.downBox = new System.Windows.Forms.CheckBox();
            this.musBox = new System.Windows.Forms.CheckBox();
            this.deskBox = new System.Windows.Forms.CheckBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_help = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // docBox
            // 
            this.docBox.AutoSize = true;
            this.docBox.Location = new System.Drawing.Point(12, 12);
            this.docBox.Name = "docBox";
            this.docBox.Size = new System.Drawing.Size(80, 17);
            this.docBox.TabIndex = 0;
            this.docBox.Text = "Documents";
            this.docBox.UseVisualStyleBackColor = true;
            // 
            // picBox
            // 
            this.picBox.AutoSize = true;
            this.picBox.Location = new System.Drawing.Point(12, 35);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(64, 17);
            this.picBox.TabIndex = 1;
            this.picBox.Text = "Pictures";
            this.picBox.UseVisualStyleBackColor = true;
            // 
            // vidBox
            // 
            this.vidBox.AutoSize = true;
            this.vidBox.Location = new System.Drawing.Point(12, 58);
            this.vidBox.Name = "vidBox";
            this.vidBox.Size = new System.Drawing.Size(58, 17);
            this.vidBox.TabIndex = 2;
            this.vidBox.Text = "Videos";
            this.vidBox.UseVisualStyleBackColor = true;
            // 
            // downBox
            // 
            this.downBox.AutoSize = true;
            this.downBox.Location = new System.Drawing.Point(12, 81);
            this.downBox.Name = "downBox";
            this.downBox.Size = new System.Drawing.Size(79, 17);
            this.downBox.TabIndex = 3;
            this.downBox.Text = "Downloads";
            this.downBox.UseVisualStyleBackColor = true;
            // 
            // musBox
            // 
            this.musBox.AutoSize = true;
            this.musBox.Location = new System.Drawing.Point(12, 104);
            this.musBox.Name = "musBox";
            this.musBox.Size = new System.Drawing.Size(54, 17);
            this.musBox.TabIndex = 4;
            this.musBox.Text = "Music";
            this.musBox.UseVisualStyleBackColor = true;
            // 
            // deskBox
            // 
            this.deskBox.AutoSize = true;
            this.deskBox.Location = new System.Drawing.Point(12, 127);
            this.deskBox.Name = "deskBox";
            this.deskBox.Size = new System.Drawing.Size(66, 17);
            this.deskBox.TabIndex = 5;
            this.deskBox.Text = "Desktop";
            this.deskBox.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(12, 150);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(80, 23);
            this.btn_Save.TabIndex = 6;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_help
            // 
            this.btn_help.Location = new System.Drawing.Point(98, 150);
            this.btn_help.Name = "btn_help";
            this.btn_help.Size = new System.Drawing.Size(62, 23);
            this.btn_help.TabIndex = 7;
            this.btn_help.Text = "?";
            this.btn_help.UseVisualStyleBackColor = true;
            this.btn_help.Click += new System.EventHandler(this.btn_help_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(169, 185);
            this.Controls.Add(this.btn_help);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.deskBox);
            this.Controls.Add(this.musBox);
            this.Controls.Add(this.downBox);
            this.Controls.Add(this.vidBox);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.docBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "ThisPC Folders";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox docBox;
        private System.Windows.Forms.CheckBox picBox;
        private System.Windows.Forms.CheckBox vidBox;
        private System.Windows.Forms.CheckBox downBox;
        private System.Windows.Forms.CheckBox musBox;
        private System.Windows.Forms.CheckBox deskBox;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_help;
    }
}

