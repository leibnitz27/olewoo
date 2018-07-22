namespace Org.Benf.OleWoo.GuiElem
{
    partial class PnlOleText
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
            this.rtfOleText = new RichTextBoxEx();
            this.SuspendLayout();
            // 
            // rtfOleText
            // 
            this.rtfOleText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfOleText.Location = new System.Drawing.Point(0, 0);
            this.rtfOleText.Name = "rtfOleText";
            this.rtfOleText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtfOleText.Size = new System.Drawing.Size(371, 243);
            this.rtfOleText.TabIndex = 1;
            this.rtfOleText.Text = "";
            // 
            // PnlOleText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtfOleText);
            this.Name = "PnlOleText";
            this.Size = new System.Drawing.Size(371, 243);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBoxEx rtfOleText;
    }
}
