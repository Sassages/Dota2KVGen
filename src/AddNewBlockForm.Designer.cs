namespace Dota2_Script_Maker
{
    partial class AddNewBlockForm
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
            this.AcceptButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.BlockName = new System.Windows.Forms.TextBox();
            this.NestedBlockDropdown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AcceptButton
            // 
            this.AcceptButton.Location = new System.Drawing.Point(12, 108);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptButton.TabIndex = 0;
            this.AcceptButton.Text = "Accept";
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(133, 108);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // BlockName
            // 
            this.BlockName.Location = new System.Drawing.Point(12, 28);
            this.BlockName.Name = "BlockName";
            this.BlockName.Size = new System.Drawing.Size(196, 20);
            this.BlockName.TabIndex = 2;
            this.BlockName.Text = "NewBlock";
            this.BlockName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // NestedBlockDropdown
            // 
            this.NestedBlockDropdown.FormattingEnabled = true;
            this.NestedBlockDropdown.Location = new System.Drawing.Point(12, 81);
            this.NestedBlockDropdown.Name = "NestedBlockDropdown";
            this.NestedBlockDropdown.Size = new System.Drawing.Size(196, 21);
            this.NestedBlockDropdown.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Block Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nested Within Block:";
            // 
            // AddNewBlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 134);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NestedBlockDropdown);
            this.Controls.Add(this.BlockName);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AcceptButton);
            this.Name = "AddNewBlockForm";
            this.Text = "New Block";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TextBox BlockName;
        private System.Windows.Forms.ComboBox NestedBlockDropdown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}