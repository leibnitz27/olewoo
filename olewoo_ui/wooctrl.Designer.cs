using Org.Benf.OleWoo.GuiElem;

namespace Org.Benf.OleWoo
{
    sealed partial class Wooctrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wooctrl));
            this.pnlMatchesList = new System.Windows.Forms.Panel();
            this.lstNodeMatches = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnHideMatches = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.spltMainSearch = new System.Windows.Forms.SplitContainer();
            this.tvLibDisp = new System.Windows.Forms.TreeView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnRewind = new System.Windows.Forms.Button();
            this.btnSortAlpha = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pbSearch = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlDisplayContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtOleDescrPlain = new Org.Benf.OleWoo.GuiElem.PnlTextOrTabbed();
            this.pnlSearchAll = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnAddNodeTabl = new System.Windows.Forms.Button();
            this.pnlMatchesList.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltMainSearch)).BeginInit();
            this.spltMainSearch.Panel1.SuspendLayout();
            this.spltMainSearch.Panel2.SuspendLayout();
            this.spltMainSearch.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).BeginInit();
            this.pnlDisplayContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlSearchAll.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMatchesList
            // 
            this.pnlMatchesList.Controls.Add(this.lstNodeMatches);
            this.pnlMatchesList.Controls.Add(this.panel2);
            this.pnlMatchesList.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlMatchesList.Location = new System.Drawing.Point(698, 0);
            this.pnlMatchesList.Name = "pnlMatchesList";
            this.pnlMatchesList.Size = new System.Drawing.Size(102, 500);
            this.pnlMatchesList.TabIndex = 7;
            this.pnlMatchesList.Visible = false;
            // 
            // lstNodeMatches
            // 
            this.lstNodeMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNodeMatches.FormattingEnabled = true;
            this.lstNodeMatches.Location = new System.Drawing.Point(0, 28);
            this.lstNodeMatches.Name = "lstNodeMatches";
            this.lstNodeMatches.Size = new System.Drawing.Size(102, 472);
            this.lstNodeMatches.TabIndex = 1;
            this.lstNodeMatches.SelectedIndexChanged += new System.EventHandler(this.lstNodeMatches_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHideMatches);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(102, 28);
            this.panel2.TabIndex = 0;
            // 
            // btnHideMatches
            // 
            this.btnHideMatches.Location = new System.Drawing.Point(68, 3);
            this.btnHideMatches.Name = "btnHideMatches";
            this.btnHideMatches.Size = new System.Drawing.Size(31, 22);
            this.btnHideMatches.TabIndex = 0;
            this.btnHideMatches.Text = ">>";
            this.btnHideMatches.UseVisualStyleBackColor = true;
            this.btnHideMatches.Click += new System.EventHandler(this.btnHideMatches_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.spltMainSearch);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(698, 500);
            this.panel3.TabIndex = 8;
            // 
            // spltMainSearch
            // 
            this.spltMainSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltMainSearch.Location = new System.Drawing.Point(0, 0);
            this.spltMainSearch.Name = "spltMainSearch";
            // 
            // spltMainSearch.Panel1
            // 
            this.spltMainSearch.Panel1.Controls.Add(this.tvLibDisp);
            this.spltMainSearch.Panel1.Controls.Add(this.panel7);
            this.spltMainSearch.Panel1.Controls.Add(this.panel5);
            // 
            // spltMainSearch.Panel2
            // 
            this.spltMainSearch.Panel2.Controls.Add(this.pnlDisplayContainer);
            this.spltMainSearch.Size = new System.Drawing.Size(698, 500);
            this.spltMainSearch.SplitterDistance = 192;
            this.spltMainSearch.TabIndex = 6;
            // 
            // tvLibDisp
            // 
            this.tvLibDisp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLibDisp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvLibDisp.HideSelection = false;
            this.tvLibDisp.Location = new System.Drawing.Point(0, 28);
            this.tvLibDisp.Name = "tvLibDisp";
            this.tvLibDisp.Size = new System.Drawing.Size(192, 446);
            this.tvLibDisp.TabIndex = 3;
            this.tvLibDisp.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLibDisp_AfterSelect);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnRewind);
            this.panel7.Controls.Add(this.btnSortAlpha);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(192, 28);
            this.panel7.TabIndex = 2;
            // 
            // btnRewind
            // 
            this.btnRewind.Image = ((System.Drawing.Image)(resources.GetObject("btnRewind.Image")));
            this.btnRewind.Location = new System.Drawing.Point(43, 3);
            this.btnRewind.Name = "btnRewind";
            this.btnRewind.Size = new System.Drawing.Size(22, 22);
            this.btnRewind.TabIndex = 1;
            this.btnRewind.UseVisualStyleBackColor = true;
            this.btnRewind.Click += new System.EventHandler(this.btnRewind_Click);
            // 
            // btnSortAlpha
            // 
            this.btnSortAlpha.Image = ((System.Drawing.Image)(resources.GetObject("btnSortAlpha.Image")));
            this.btnSortAlpha.Location = new System.Drawing.Point(3, 3);
            this.btnSortAlpha.Name = "btnSortAlpha";
            this.btnSortAlpha.Size = new System.Drawing.Size(22, 22);
            this.btnSortAlpha.TabIndex = 0;
            this.btnSortAlpha.UseVisualStyleBackColor = true;
            this.btnSortAlpha.Click += new System.EventHandler(this.btnSortAlpha_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtSearch);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 474);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(192, 26);
            this.panel5.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(92, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.pbSearch);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(92, 26);
            this.panel6.TabIndex = 3;
            // 
            // pbSearch
            // 
            this.pbSearch.Image = ((System.Drawing.Image)(resources.GetObject("pbSearch.Image")));
            this.pbSearch.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbSearch.InitialImage")));
            this.pbSearch.Location = new System.Drawing.Point(3, 6);
            this.pbSearch.Name = "pbSearch";
            this.pbSearch.Size = new System.Drawing.Size(17, 17);
            this.pbSearch.TabIndex = 2;
            this.pbSearch.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find Symbol";
            // 
            // pnlDisplayContainer
            // 
            this.pnlDisplayContainer.Controls.Add(this.panel1);
            this.pnlDisplayContainer.Controls.Add(this.pnlSearchAll);
            this.pnlDisplayContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplayContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlDisplayContainer.Name = "pnlDisplayContainer";
            this.pnlDisplayContainer.Size = new System.Drawing.Size(502, 500);
            this.pnlDisplayContainer.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtOleDescrPlain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 474);
            this.panel1.TabIndex = 3;
            // 
            // txtOleDescrPlain
            // 
            this.txtOleDescrPlain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOleDescrPlain.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOleDescrPlain.Location = new System.Drawing.Point(0, 0);
            this.txtOleDescrPlain.Name = "txtOleDescrPlain";
            this.txtOleDescrPlain.NodeLocator = null;
            this.txtOleDescrPlain.Size = new System.Drawing.Size(502, 474);
            this.txtOleDescrPlain.TabIndex = 1;
            // 
            // pnlSearchAll
            // 
            this.pnlSearchAll.Controls.Add(this.panel4);
            this.pnlSearchAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSearchAll.Location = new System.Drawing.Point(0, 474);
            this.pnlSearchAll.Name = "pnlSearchAll";
            this.pnlSearchAll.Size = new System.Drawing.Size(502, 26);
            this.pnlSearchAll.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnAddNodeTabl);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(402, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(100, 26);
            this.panel4.TabIndex = 1;
            // 
            // btnAddNodeTabl
            // 
            this.btnAddNodeTabl.Location = new System.Drawing.Point(3, 3);
            this.btnAddNodeTabl.Name = "btnAddNodeTabl";
            this.btnAddNodeTabl.Size = new System.Drawing.Size(93, 23);
            this.btnAddNodeTabl.TabIndex = 0;
            this.btnAddNodeTabl.Text = "Add New Tab";
            this.btnAddNodeTabl.UseVisualStyleBackColor = true;
            this.btnAddNodeTabl.Click += new System.EventHandler(this.btnAddNodeTabl_Click);
            // 
            // Wooctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlMatchesList);
            this.Name = "Wooctrl";
            this.Size = new System.Drawing.Size(800, 500);
            this.pnlMatchesList.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.spltMainSearch.Panel1.ResumeLayout(false);
            this.spltMainSearch.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltMainSearch)).EndInit();
            this.spltMainSearch.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).EndInit();
            this.pnlDisplayContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlSearchAll.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMatchesList;
        private System.Windows.Forms.ListBox lstNodeMatches;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer spltMainSearch;
        private System.Windows.Forms.Panel pnlDisplayContainer;
        private System.Windows.Forms.Button btnHideMatches;
        private System.Windows.Forms.Panel panel1;
        private PnlTextOrTabbed txtOleDescrPlain;
        private System.Windows.Forms.Panel pnlSearchAll;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnAddNodeTabl;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pbSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TreeView tvLibDisp;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnSortAlpha;
        private System.Windows.Forms.Button btnRewind;



    }
}
