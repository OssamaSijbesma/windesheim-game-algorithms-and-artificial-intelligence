using System.Windows.Forms;

namespace Arce
{
    partial class Form
    {
        private DBPanel dbPanel;
        private Panel staticPanel;

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
            this.staticPanel = new System.Windows.Forms.Panel();
            this.dbPanel = new Arce.DBPanel();
            this.dbPanel.SuspendLayout();
            this.SuspendLayout();

            // Static panel
            this.staticPanel.Name = "staticPanel";
            this.staticPanel.Controls.Add(this.dbPanel);
            this.staticPanel.Location = new System.Drawing.Point(0, 0);
            this.staticPanel.Margin = new System.Windows.Forms.Padding(0);
            this.staticPanel.Size = new System.Drawing.Size(1760, 960);
            this.staticPanel.TabIndex = 0;

            // Dubbel buffered panel
            this.dbPanel.Name = "dbPanel";
            this.dbPanel.BackColor = System.Drawing.Color.Transparent;
            this.dbPanel.Location = new System.Drawing.Point(0, 0);
            this.dbPanel.Margin = new System.Windows.Forms.Padding(0);
            this.dbPanel.Size = new System.Drawing.Size(1760, 960);
            this.dbPanel.TabIndex = 0;
            this.dbPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.dbPanel1_Paint);
            this.dbPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbPanel1_MouseClick);

            // Form settings
            this.ClientSize = new System.Drawing.Size(1744, 921);
            this.Controls.Add(this.staticPanel);
            this.Name = "Form";
            this.Text = "Arce";
            this.staticPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion
    }
}

