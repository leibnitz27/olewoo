namespace Org.Benf.OleWoo.GuiElem
{
    partial class PnlTextOrTabbed
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
//            this.pnlOleText = new System.Windows.Forms.TabControl();
            this.pnlOleText = new TabControlCB();
            this.txtOleText = new PnlOleText();
            this.SuspendLayout();
            // 
            // pnlOleText
            // 
            this.pnlOleText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOleText.Location = new System.Drawing.Point(0, 0);
            this.pnlOleText.Name = "pnlOleText";
            this.pnlOleText.SelectedIndex = 0;
            this.pnlOleText.Size = new System.Drawing.Size(321, 293);
            this.pnlOleText.TabIndex = 0;
            this.pnlOleText.SelectedIndexChanged += new System.EventHandler(pnlOleText_TabIndexChanged);
            // 
            // txtOleText
            // 
            this.txtOleText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOleText.Location = new System.Drawing.Point(0, 0);
            this.txtOleText.Name = "txtOleText";
            this.txtOleText.Size = new System.Drawing.Size(321, 293);
            this.txtOleText.TabIndex = 1;
            this.txtOleText.TreeNode = null;
            // 
            // PnlTextOrTabbed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtOleText);
            this.Controls.Add(this.pnlOleText);
            this.Name = "PnlTextOrTabbed";
            this.Size = new System.Drawing.Size(321, 293);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControlCB pnlOleText;
        private PnlOleText txtOleText;
    }
}
