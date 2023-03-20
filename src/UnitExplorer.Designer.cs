namespace Dota2_Script_Maker
{
    partial class UnitExplorer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitExplorer));
            this.Existing_Units = new System.Windows.Forms.ListBox();
            this.Create_New_Unit = new System.Windows.Forms.Button();
            this.Create_Duplicate = new System.Windows.Forms.Button();
            this.Edit_Unit = new System.Windows.Forms.Button();
            this.Create_Unit_Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.Duplicate_Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.Edit_Unit_Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.Delete_Unit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Current_Folder = new System.Windows.Forms.ComboBox();
            this.Refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Existing_Units
            // 
            this.Existing_Units.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Existing_Units.FormattingEnabled = true;
            this.Existing_Units.HorizontalScrollbar = true;
            this.Existing_Units.ItemHeight = 16;
            this.Existing_Units.Location = new System.Drawing.Point(90, 53);
            this.Existing_Units.Name = "Existing_Units";
            this.Existing_Units.Size = new System.Drawing.Size(401, 212);
            this.Existing_Units.TabIndex = 0;
            this.Existing_Units.DoubleClick += new System.EventHandler(this.Existing_Units_DoubleClick);
            // 
            // Create_New_Unit
            // 
            this.Create_New_Unit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Create_New_Unit.BackgroundImage")));
            this.Create_New_Unit.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Create_New_Unit.Location = new System.Drawing.Point(44, 297);
            this.Create_New_Unit.Name = "Create_New_Unit";
            this.Create_New_Unit.Size = new System.Drawing.Size(100, 55);
            this.Create_New_Unit.TabIndex = 1;
            this.Create_New_Unit.Text = "Create New Unit";
            this.Create_New_Unit.UseVisualStyleBackColor = true;
            this.Create_New_Unit.Click += new System.EventHandler(this.Create_New_Unit_Click);
            // 
            // Create_Duplicate
            // 
            this.Create_Duplicate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Create_Duplicate.BackgroundImage")));
            this.Create_Duplicate.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Create_Duplicate.Location = new System.Drawing.Point(180, 297);
            this.Create_Duplicate.Name = "Create_Duplicate";
            this.Create_Duplicate.Size = new System.Drawing.Size(100, 55);
            this.Create_Duplicate.TabIndex = 2;
            this.Create_Duplicate.Text = "Create Duplicate";
            this.Create_Duplicate.UseVisualStyleBackColor = true;
            this.Create_Duplicate.Click += new System.EventHandler(this.Create_Duplicate_Click);
            // 
            // Edit_Unit
            // 
            this.Edit_Unit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Edit_Unit.BackgroundImage")));
            this.Edit_Unit.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Edit_Unit.Location = new System.Drawing.Point(324, 297);
            this.Edit_Unit.Name = "Edit_Unit";
            this.Edit_Unit.Size = new System.Drawing.Size(100, 55);
            this.Edit_Unit.TabIndex = 3;
            this.Edit_Unit.Text = "Edit Unit";
            this.Edit_Unit.UseVisualStyleBackColor = true;
            this.Edit_Unit.Click += new System.EventHandler(this.Edit_Unit_Click);
            // 
            // Create_Unit_Tooltip
            // 
            this.Create_Unit_Tooltip.Popup += new System.Windows.Forms.PopupEventHandler(this.Create_Unit_Tooltip_Popup);
            // 
            // Delete_Unit
            // 
            this.Delete_Unit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Delete_Unit.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Delete_Unit.Location = new System.Drawing.Point(464, 297);
            this.Delete_Unit.Name = "Delete_Unit";
            this.Delete_Unit.Size = new System.Drawing.Size(83, 55);
            this.Delete_Unit.TabIndex = 4;
            this.Delete_Unit.Text = "Delete";
            this.Delete_Unit.UseVisualStyleBackColor = false;
            this.Delete_Unit.Click += new System.EventHandler(this.Delete_Unit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Current Folder:";
            // 
            // Current_Folder
            // 
            this.Current_Folder.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Current_Folder.FormattingEnabled = true;
            this.Current_Folder.Location = new System.Drawing.Point(139, 15);
            this.Current_Folder.Name = "Current_Folder";
            this.Current_Folder.Size = new System.Drawing.Size(352, 22);
            this.Current_Folder.TabIndex = 6;
            this.Current_Folder.Text = "Root";
            this.Current_Folder.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Refresh
            // 
            this.Refresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Refresh.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Refresh.Location = new System.Drawing.Point(497, 15);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(82, 27);
            this.Refresh.TabIndex = 7;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = false;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // UnitExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 364);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.Current_Folder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Delete_Unit);
            this.Controls.Add(this.Edit_Unit);
            this.Controls.Add(this.Create_Duplicate);
            this.Controls.Add(this.Create_New_Unit);
            this.Controls.Add(this.Existing_Units);
            this.Name = "UnitExplorer";
            this.Text = "UnitExplorer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UnitExplorer_FormClosed);
            this.Load += new System.EventHandler(this.UnitExplorer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Existing_Units;
        private System.Windows.Forms.Button Create_New_Unit;
        private System.Windows.Forms.Button Create_Duplicate;
        private System.Windows.Forms.Button Edit_Unit;
        private System.Windows.Forms.ToolTip Create_Unit_Tooltip;
        private System.Windows.Forms.ToolTip Duplicate_Tooltip;
        private System.Windows.Forms.ToolTip Edit_Unit_Tooltip;
        private System.Windows.Forms.Button Delete_Unit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Current_Folder;
        private System.Windows.Forms.Button Refresh;
    }
}